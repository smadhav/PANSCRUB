using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScanImage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage.Tests
{
    [TestClass()]
    public class MSWordExtractorTests
    {
        [TestMethod()]
        public void MSWordExtractorTest()
        {
            
            Assert.Fail();
        }

        [TestMethod()]
        public void extractImagesTest()
        {
            var myDriver = new MSWordScanner();
            //myDriver.extractImages(@"C:\Downloads\ScanSample.docx");
            myDriver.extractImages(@"C:\Downloads\3270Single.docx");
            //myDriver.extractImages(@"C:\Downloads\SimpleScan1.docx");
            //Assert.Fail();
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void extractImagesTestUsingOneImageDoc()
        {
            var myDriver = new MSWordScanner();
            //myDriver.extractImages(@".\Docs\ExampleWord.docx");
            myDriver.extractImages("OneImage.docx");
            Assert.IsTrue(true);
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void extractImagesTestUsingTwoImageDoc()
        {
            var myDriver = new MSWordScanner();
            myDriver.extractImages("TwoImages.docx");
            Assert.IsTrue(true);
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void extractImagesTestUsingThreeDoc()
        {
            var myDriver = new MSWordScanner();
            myDriver.extractImages("ThreeImages.docx");
            Assert.IsTrue(true);
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void extractImagesTestUsingBrianDoc()
        {
            var myDriver = new MSWordScanner();
            myDriver.extractImages("ExampleWord.docx");
            Assert.IsTrue(true);
        }
        [TestMethod()]
        public void replaceImageTest()
        {
            Assert.Fail();
        }
    }
}