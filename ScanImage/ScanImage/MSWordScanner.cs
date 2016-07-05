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

        private MSWord.Document doc { get; set; }
        private MSWord.Application  myWordApp { get; set; }
        object missing = System.Reflection.Missing.Value;
        object yes = true;
        object no = false;
        private TesseractScan scanEng = new TesseractScan();
        string imageName = @"C:\Downloads\scan\image.jpg";
        string newImage = @"C:\Downloads\new\image.jpg";
        string docLoc = @"C:\Downloads\scan\";
        string newLoc = @"C:\Downloads\new\";
        public MSWordScanner()
        {
            //doc = myDoc;
            
        }
        public void extractImages(string fileName)
        {
            //var fileName = @"C:\Downloads\word2010_1.docx";
            //var ranges = new List<MSWord.Range>();
            var resultLst = new List<ScanData>(0);
            myWordApp = new MSWord.Application();
            //MSWord.Document myDoc = openMSWord(Path.GetFullPath(fileName));
            MSWord.Document myDoc = openMSWord(docLoc+fileName);
            try {
                foreach (MSWord.InlineShape s in myDoc.InlineShapes)
                {
                    if (s.Type == MSWord.WdInlineShapeType.wdInlineShapePicture)
                    {
                        
                        //TODO need to research if bye[] or memory stream can be directly used
                        byte[] img = (byte[])s.Range.EnhMetaFileBits;
                        Bitmap bmp = new Bitmap(new MemoryStream(img));
                        //bmp.Save(imageName);
                        // Copying through clipboard is reducing image quality
                        //s.Select();
                        //myWordApp.Selection.Copy();
                        //writeImageFromClipB();
                        var scanData = scanEng.ScanForAccountNumber(bmp);
                        //var scanData = scanEng.ScanForAccountNumber(imageName);
                        if (scanData.Count > 0)
                        {
                            ImageMasker.maskPartOfSensitive(bmp, scanData);
                            //TODO Could not find a way to directly add to Word.
                            //So saving to disk and addding. This is very expensive and need to be changed
                            bmp.Save(newImage);
                            MSWord.Range thisRange = s.Range;
                            s.Delete();
                            thisRange.InlineShapes.AddPicture(newImage);
                            //s.Range.InlineShapes.AddPicture(newImage);
                            File.Delete(newImage);
                        }
                        //File.Delete(imageName);
                        //ranges.Add(s.Range);
                        //s.Delete();
                        
                    }
                }
                object newName = newLoc + fileName;
                SaveMSWord(newName, myDoc);
                /*foreach( MSWord.Range r in ranges)
                {

                    r.InlineShapes.AddPicture(@"C:\Downloads\slide_12.jpg", ref missing, ref missing, ref missing);
                }*/
            }
            catch(Exception ie)
            {
                Console.WriteLine("Extract encountered Exception:" + ie);
            }
            finally
            {
                //myWordApp.Quit(ref yes, ref missing, ref missing);
                myWordApp.Quit(ref no, ref missing, ref missing);

            }
            
            

        }

        public void replaceImage(Object WordDOc, string loc)
        {
            
        }

        private MSWord.Document openMSWord(object docFile)
        {
            
            MSWord.Document thisDoc = myWordApp.Documents.Open(ref docFile, ref missing, ref no, ref missing,
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
