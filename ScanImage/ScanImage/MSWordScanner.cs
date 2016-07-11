using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace ScanImage
{
    using System.IO;
    using MSWord = Microsoft.Office.Interop.Word;

    public class MSWordScanner : MSScanner
    {

        object missing = System.Reflection.Missing.Value;
        object yes = true;
        object no = false;
        //string imageName = @"C:\Downloads\scan\image.jpg";
        string newImage = @"C:\Downloads\new\image.jpg";
        //string docLoc = @"C:\Downloads\scan\";
        //string newLoc = @"C:\Downloads\new\";
        public MSWordScanner()
        {
            //doc = myDoc;
            
        }
        
        public DocScanData ScanForPattern(string fileName)
        {
            var docScanData = new DocScanData(fileName);
            MSWord.Application myWordApp = new MSWord.Application();
            MSWord.Document myDoc = null;
            TesseractScan tessScanner = new TesseractScan();
            try
            {
                myDoc = openMSWord(myWordApp,fileName);
                foreach (MSWord.InlineShape s in myDoc.InlineShapes)
                {
                    if (s.Type == MSWord.WdInlineShapeType.wdInlineShapePicture)
                    {
                        //TODO need to research if bye[] or memory stream can be directly used
                        byte[] img = (byte[])s.Range.EnhMetaFileBits;
                        Bitmap bmp = new Bitmap(new MemoryStream(img));
                        ImgScanData tessData = tessScanner.ScanForAccountNumber(bmp);
                        if(tessData != null)
                        {
                            docScanData.numberOfImg++;
                            docScanData.imgData.Add(tessData);
                        }
                      
                    }
                }
                docScanData.isScanCompleted = true;
            }
            catch (Exception ie)
            {
                docScanData.isScanCompleted = true;
                Console.WriteLine("Extract encountered Exception:" + ie);
            }
            finally
            {
                //Quit Word app without saving doc
                myWordApp.Quit(ref no, ref missing, ref missing);

            }

            return docScanData;

        }
        public DocScanData ScanAndFixPattern(string fileName)
        {
            var docScanData = new DocScanData(fileName);
            MSWord.Application myWordApp = new MSWord.Application();
            MSWord.Document myDoc = null;
            TesseractScan tessScanner = new TesseractScan();
            try
            {
                myDoc = openMSWord(myWordApp,fileName);
                foreach (MSWord.InlineShape s in myDoc.InlineShapes)
                {
                    if (s.Type == MSWord.WdInlineShapeType.wdInlineShapePicture)
                    {
                        //TODO need to research if bye[] or memory stream can be directly used
                        byte[] img = (byte[])s.Range.EnhMetaFileBits;
                        Bitmap bmp = new Bitmap(new MemoryStream(img));
                        var tessData = tessScanner.ScanForAccountNumber(bmp);
                        if (tessData != null)
                        {
                            docScanData.numberOfImg++;
                            docScanData.imgData.Add(tessData);
                        }
                        if (tessData.isScanCompleted && tessData.scanData.Count > 0)
                        {
                            ImageMasker.maskPartOfSensitive(bmp, tessData.scanData);
                            //TODO Could not find a way to directly add to Word.
                            //So saving to disk and addding. This is very expensive and need to be changed
                            bmp.Save(newImage);
                            MSWord.Range thisRange = s.Range;
                            s.Delete();
                            thisRange.InlineShapes.AddPicture(newImage);
                            File.Delete(newImage);
                        }
                        
                    }
                }
                docScanData.isScanCompleted = true;
                //SaveMSWord(fileName, myDoc);
                //myDoc.Close();
            }
            catch (Exception ie)
            {
                docScanData.isScanCompleted = false;
                Console.WriteLine("Extract encountered Exception:" + ie);
            }
            finally
            {
                //Quit application after saving document
                myWordApp.Quit(ref yes, ref missing, ref missing);
                //TODO make sure all resources are cleaned up
            }

            return docScanData;
        }

        private MSWord.Document openMSWord(MSWord.Application thisApp,object docFile)
        {
            
            MSWord.Document thisDoc = thisApp.Documents.Open(ref docFile, ref missing, ref no, ref missing,
              ref missing, ref missing, ref missing, ref missing, ref missing,
              ref missing, ref missing, ref yes, ref missing, ref missing, ref missing, ref missing);

            if(thisDoc == null)
            {
                Console.Out.WriteLine("Document could not read");
            }
            return thisDoc;

        }
        private void SaveMSWord( object docFile, MSWord.Document msDoc)
        {
            msDoc.SaveAs2(docFile, ref missing, ref no, ref missing,
              ref missing, ref missing, ref missing, ref missing, ref missing,
              ref missing, ref missing, ref yes, ref missing, ref missing, ref missing, ref missing);
        }

        private void writeImageFromClipB()
        {
            if (Clipboard.GetDataObject() != null)
            {
                IDataObject data = Clipboard.GetDataObject();

                if (data.GetDataPresent(DataFormats.Bitmap))
                {
                    Image image = (Image)data.GetData(DataFormats.Bitmap, true);

                    //image.Save(imageName, System.Drawing.Imaging.ImageFormat.Bmp);
                    image.Save(@"C:\Downloads\scan\image.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    //image.Save(@"C:\Downloads\scan\image.gif", System.Drawing.Imaging.ImageFormat.Gif);
                }
                else
                {
                    MessageBox.Show("The Data In Clipboard is not as image format");
                }
            }
            else
            {
                MessageBox.Show("The Clipboard was empty");
            }
        }

        /*private ScanData scanImage()
        {

        }*/
         
       
       
    }
}
