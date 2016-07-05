using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScanImage;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage.Tests
{
    [TestClass()]
    public class ImagePreProcessorTests
    {
        [TestMethod()]
        public void AdjustDPITest()
        {


            using (Bitmap img = (Bitmap)Image.FromFile(@".\images\screenshot.gif"))
            {
                ImagePreProcessor.AdjustDPI(img);
                Assert.IsTrue(img.VerticalResolution >= 300);
                //Bitmap bitImg = new Bitmap(img);
                img.Save(@"C:\Downloads\screenshot_DPI.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                //img.Save(@"./newImages/screenshot_DPI.bmp");
            }
            


        }

        [TestMethod()]
        public void SetGrayscaleTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveNoiseTest()
        {
            Assert.Fail();
        }

        
    }
}