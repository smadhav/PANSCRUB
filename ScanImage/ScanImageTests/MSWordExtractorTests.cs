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
            var myDriver = new MSWordExtractor();
            //myDriver.extractImages(@"C:\Downloads\ScanSample.docx");
            myDriver.extractImages(@"C:\Downloads\3270Single.docx");
            //myDriver.extractImages(@"C:\Downloads\SimpleScan1.docx");
            //Assert.Fail();
        }

        [TestMethod()]
        public void replaceImageTest()
        {
            Assert.Fail();
        }
    }
}