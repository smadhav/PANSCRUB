using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.Office;

namespace ScanImage
{
    /*using System.IO;
    using MSExcel = Microsoft.Office.Interop.Excel;

    public class MSExcelExtractor : MSExtractor
    {

        private MSExcel.Workbook wrkBook { get; set; }
        private MSExcel.Application myExcelApp { get; set; }
        object missing = System.Reflection.Missing.Value;
        object yes = true;
        object no = false;
        private TesseractScan scanEng = new TesseractScan();
        string imageName = @"C:\Downloads\scan\image.jpg";
        string newImage = @"C:\Downloads\new\image.jpg";
        public MSExcelExtractor()
        {
            //doc = myDoc;

        }
        public void extractImages(string fileName)
        {
            //var fileName = @"C:\Downloads\word2010_1.docx";
            //var ranges = new List<MSExcel.Range>();
            myExcelApp = new MSExcel.Application();
            wrkBook = openMSExcel(fileName);
            try
            {
                foreach (MSExcel.Worksheet s in wrkBook.Sheets)
                {
                    foreach (MSExcel.Worksheet.Shapes 
                }
                {
                    if (s.Type == MSExcel.WdInlineShapeType.wdInlineShapePicture)
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
                            scanEng.maskSensitive(imageName, scanData);
                            s.Range.InlineShapes.AddPicture(newImage);
                            File.Delete(newImage);
                        }
                        File.Delete(imageName);
                        //ranges.Add(s.Range);
                        //s.Delete();
                    }
                }
                
            }
            catch (Exception ie)
            {
                Console.WriteLine("Extract encountered Exception:" + ie);
            }
            finally
            {
                myWordApp.Quit(ref yes, missing, missing);
            }



        }

        public void replaceImage(Object WordDOc, string loc)
        {

        }

        private MSExcel.Workbook openMSExcel(string docFile)
        {

            MSExcel.Workbook thisDoc = myExcelApp.Workbooks.Open(docFile,  missing,  no, missing,
              missing, missing, missing, missing, missing,
              yes, missing, missing, missing, missing, missing);

            if (thisDoc == null)
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
        }*/

        /*private ScanData scanImage()
        {

        }*/



    
}
