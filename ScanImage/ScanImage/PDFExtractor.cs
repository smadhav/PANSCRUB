using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using iTextSharp.text.pdf;
//using System.Windows.Forms;

namespace ScanImage
{
    using System.IO;
    using MSWord = Microsoft.Office.Interop.Word;

    public class PDFExtractor : MSExtractor
    {

        iTextSharp.text.pdf.RandomAccessFileOrArray RAFObj = null;
        iTextSharp.text.pdf.PdfReader PDFReaderObj = null;
        iTextSharp.text.pdf.PdfObject PDFObj = null;
        iTextSharp.text.pdf.PdfStream PDFStremObj = null;

        /*
        private MSWord.Document doc { get; set; }
        private MSWord.Application myWordApp { get; set; }
        object missing = System.Reflection.Missing.Value;
        object yes = true;
        object no = false;
        private TesseractScan scanEng = new TesseractScan();
        string imageName = @"C:\Downloads\scan\image.jpg";
        string newImage = @"C:\Downloads\new\image.jpg"; */
        public PDFExtractor()
        {
            //doc = myDoc;

        }
        public void extractImages(string fileName)
        {
            //var fileName = @"C:\Downloads\word2010_1.docx";
            //var ranges = new List<MSWord.Range>();
            /*
            PDFReaderObj = openPDF(fileName);
            try
            {
                for (int i = 0; i <= PDFReaderObj.XrefSize - 1; i++)
                {
                    PDFObj = PDFReaderObj.GetPdfObject(i);

                    if ((PDFObj != null) && PDFObj.IsStream())
                    {
                        PDFStremObj = (iTextSharp.text.pdf.PdfStream)PDFObj;
                        iTextSharp.text.pdf.PdfObject subtype = PDFStremObj.Get(iTextSharp.text.pdf.PdfName.SUBTYPE);

                        if ((subtype != null) && subtype.ToString() == iTextSharp.text.pdf.PdfName.IMAGE.ToString())
                        {
                            try
                            {

                                iTextSharp.text.pdf.parser.PdfImageObject PdfImageObj =
                         new iTextSharp.text.pdf.parser.PdfImageObject((iTextSharp.text.pdf.PRStream)PDFStremObj);

                                System.Drawing.Image ImgPDF = PdfImageObj.GetDrawingImage();


                                ImgPDF.Save(imageName);

                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
                PDFReaderObj.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ImgList;
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
                            scanEng.maskSensitive(imageName, scanData);
                            s.Range.InlineShapes.AddPicture(newImage);
                            File.Delete(newImage);
                        }
                        File.Delete(imageName);
                        //ranges.Add(s.Range);
                        //s.Delete();
                    }
                }*/

                /*foreach( MSWord.Range r in ranges)
                {

                    r.InlineShapes.AddPicture(@"C:\Downloads\slide_12.jpg", ref missing, ref missing, ref missing);
                }*/
            /*}
            catch (Exception ie)
            {
                Console.WriteLine("Extract encountered Exception:" + ie);
            }
            finally
            {
                myWordApp.Quit(ref yes, ref missing, ref missing);
            }

            */

        }

        public void replaceImage(Object WordDOc, string loc)
        {

        }
        /*
        private PdfReader openPDF(object docFile)
        {
            PdfReader thisReader = null;
            try
            {
                RAFObj = new iTextSharp.text.pdf.RandomAccessFileOrArray(PDFSourcePath);
                thisReader = new iTextSharp.text.pdf.PdfReader(RAFObj, null);

            }
            catch(Exception ie)
            {
                Console.Out.WriteLine("Document could not read");
            }
            return thisReader;


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