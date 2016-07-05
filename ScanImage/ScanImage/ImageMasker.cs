using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    class ImageMasker
    {
        const int startPos = 6;
        const int endingPos = 12;
        public static void maskSensitive(string fileName, List<ScanData> locations)
        {
            Color maskColor = Color.Red;
            try
            {
                Bitmap currentImage = (Bitmap)Image.FromFile(fileName, false);
                foreach (ScanData item in locations)
                {
                    for (int i = item.bbox[0]; i <= item.bbox[2]; i++)
                    {
                        for (int j = item.bbox[1]; j <= item.bbox[3]; j++)
                        {
                            currentImage.SetPixel(i, j, maskColor);

                        }
                    }
                }
                currentImage.Save(@"C:\Downloads\new\image.jpg");
            }
            catch (Exception ie)
            {
                Console.WriteLine("Exception during Masking:" + ie);
            }
        }
        public static void maskPartOfSensitive(Bitmap bmpImage, List<ScanData> locations)
        {
            
            try
            {
                foreach (ScanData item in locations)
                {
                    //Idea is to find the total length of string
                    //And start masking on the 6th position to the 12th position
                    int strLen = item.foundTxt.Length;
                    int unitLen = (item.bbox[2] - item.bbox[0]) / strLen;
                    int x1 = item.bbox[0] + unitLen * (startPos - 1);
                    int y1 = item.bbox[1];
                    int x2 = item.bbox[0] + unitLen * endingPos;
                    int y2 = item.bbox[3];
                    MaskArea(bmpImage, x1, y1, x2, y2);
                }
            }
            catch (Exception ie)
            {
                Console.WriteLine("Exception during Masking:" + ie);
            }
        }

        public void maskPartOfSensitive(string fileName, List<ScanData> locations)
        {
            try
            {
                Bitmap currentImage = (Bitmap)Image.FromFile(fileName, false);
                maskPartOfSensitive(currentImage, locations);
                /*foreach (ScanData item in locations)
                {
                    //Idea is to find the total length of string
                    //And start masking on the 6th position to the 12th position
                    int strLen = item.foundTxt.Length;
                    int unitLen = (item.bbox[2] - item.bbox[0]) / strLen;
                    for (int i = (item.bbox[0] + unitLen * (startPos - 1)); i <= (item.bbox[0] + unitLen * endingPos); i++)
                    {
                        for (int j = item.bbox[1]; j <= item.bbox[3]; j++)
                        {
                            currentImage.SetPixel(i, j, maskColor);

                        }
                    }
                }*/

                currentImage.Save(@"C:\Downloads\new\image.jpg");
            }
            catch (Exception ie)
            {
                Console.WriteLine("Exception during Masking:" + ie);
            }
        }
        private static void MaskArea(Bitmap bmpImage, int x1, int y1, int x2, int y2)
        {
            //TODO  Add logic to check mask box is within the image sizes
            
                for (int i = x1; i <= x2; i++)
                {
                    for (int j = y1; j <= y2; j++)
                    {
                        bmpImage.SetPixel(i, j, Color.Red);

                    }
                }
           
        }
    }
}
