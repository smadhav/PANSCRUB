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

    public class MSWordExtractor : MSExtractor
    {

        private MSWord.Document doc { get; set; }
        private MSWord.Application  myWordApp { get; set; }
        object missing = System.Reflection.Missing.Value;
        object yes = true;
        object no = false;
        private TesseractScan scanEng = new TesseractScan();
        string imageName = @"C:\Downloads\scan\image.jpg";
        string newImage = @"C:\Downloads\new\image.jpg";
        public MSWordExtractor()
        {
            //doc = myDoc;
            
        }
        public void extractImages(string fileName)
        {
            //var fileName = @"C:\Downloads\word2010_1.docx";
            //var ranges = new List<MSWord.Range>();
            myWordApp = new MSWord.Application();
            MSWord.Document myDoc = openMSWord(fileName);
            try {
                foreach (MSWord.InlineShape s in myDoc.InlineShapes)
                {
                    if (s.Type == MSWord.WdInlineShapeType.wdInlineShapePicture)
                    {
                        byte[] img = (byte[])s.Range.EnhMetaFileBits;
                        Bitmap bmp = new Bitmap(new MemoryStream(img));
                        bmp.Save(imageName);
                        // Copying through clipboard is reducing image quality
                        //s.Select();
                        //myWordApp.Selection.Copy();
                        //writeImageFromClipB();
                        var scanData = scanEng.ScanForAccountNumber(imageName);
                        if (scanData.Count > 0)
                        {
                            scanEng.maskPartOfSensitive(imageName, scanData);
                            s.Range.InlineShapes.AddPicture(newImage);
                            File.Delete(newImage);
                        }
                        File.Delete(imageName);
                        //ranges.Add(s.Range);
                        //s.Delete();
                    }
                }
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
                myWordApp.Quit(ref yes, ref missing, ref missing);
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
