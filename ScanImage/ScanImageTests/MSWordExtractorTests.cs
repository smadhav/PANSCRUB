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
    public class MSWordScannerTests
    {
        [TestMethod()]
        public void MSWordExtractorTest()
        {
            
            Assert.Fail();
        }

        
        //Using Sample Document with one image
        [TestMethod()]
        public void scanImagesTestUsingOneImageDoc()
        {
            var myDriver = new MSWordScanner();
            //myDriver.extractImages(@".\Docs\ExampleWord.docx");
            DocScanData result =  myDriver.ScanForPattern(@"C:\Downloads\new\OneImage.docx");
            Assert.AreEqual(3, result.numberOfImg);
            Assert.AreEqual(0, result.errorNumber);
            Assert.IsTrue(result.isScanCompleted);
            Assert.AreEqual(3, result.imgData.Count);
            Assert.AreEqual(2, result.imgData[2].scanData.Count);
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void scanImagesTestUsingTwoImageDoc()
        {
            var myDriver = new MSWordScanner();
            DocScanData result = myDriver.ScanForPattern(@"C:\Downloads\new\TwoImages.docx");
            Assert.AreEqual(2, result.numberOfImg);
            Assert.AreEqual(0, result.errorNumber);
            Assert.IsTrue(result.isScanCompleted);
            Assert.AreEqual(2, result.imgData.Count);
            Assert.AreEqual(2, result.imgData[0].scanData.Count);
            Assert.AreEqual(1, result.imgData[1].scanData.Count);
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void scanImagesTestUsingThreeDoc()
        {
            var myDriver = new MSWordScanner();
            DocScanData result = myDriver.ScanForPattern(@"C:\Downloads\new\ThreeImages.docx");
            Assert.AreEqual(2, result.numberOfImg);
            Assert.AreEqual(0, result.errorNumber);
            Assert.IsTrue(result.isScanCompleted);
            Assert.AreEqual(2, result.imgData.Count);
            Assert.AreEqual(2, result.imgData[0].scanData.Count);
            Assert.AreEqual(1, result.imgData[1].scanData.Count);
        }
        //Using Sample Document with one image
        [TestMethod()]
        public void scanImagesTestUsingBrianDoc()
        {
            var myDriver = new MSWordScanner();
            myDriver.ScanForPattern(@"C:\Downloads\new\ExampleWord.docx");
            Assert.IsTrue(true);
        }
        
    }
}