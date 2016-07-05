using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    public class ImagePreProcessor
    {
        public static void AdjustDPI(Bitmap bmpImage)
        {
            //It is the understanding any DPI(resolution) below 300
            // is not good for Tesseract. So change resolution accordingly
            //Check if resolution is below 300
            float hDPI = bmpImage.HorizontalResolution;
            float vDPI = bmpImage.VerticalResolution;


            if (hDPI < 300 || vDPI < 300)
            {
                int dpiFactor = (int)((300 / (hDPI <= vDPI ? hDPI : vDPI)) + 1.0f);
                hDPI = hDPI * dpiFactor;
                vDPI = vDPI * dpiFactor;
                bmpImage.SetResolution(hDPI, vDPI);
            }
        }

        public static Bitmap SetGrayscale(Bitmap img)
        {

            Bitmap temp = (Bitmap)img;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    byte gray = (byte)(.299 * c.R + .587 * c.G + .114 * c.B);

                    bmap.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            //TODo check if this cloning is needed.
            // Also check if manipulation can be done on incoming Bitmap
            return (Bitmap)bmap.Clone();

        }
        public static Bitmap RemoveNoise(Bitmap bmap)
        {

            for (var x = 0; x < bmap.Width; x++)
            {
                for (var y = 0; y < bmap.Height; y++)
                {
                    var pixel = bmap.GetPixel(x, y);
                    if (pixel.R < 162 && pixel.G < 162 && pixel.B < 162)
                        bmap.SetPixel(x, y, Color.Black);
                }
            }

            for (var x = 0; x < bmap.Width; x++)
            {
                for (var y = 0; y < bmap.Height; y++)
                {
                    var pixel = bmap.GetPixel(x, y);
                    if (pixel.R > 162 && pixel.G > 162 && pixel.B > 162)
                        bmap.SetPixel(x, y, Color.White);
                }
            }

            return bmap;
        }
    }
    
}
