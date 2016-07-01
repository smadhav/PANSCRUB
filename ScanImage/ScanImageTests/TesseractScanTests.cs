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
    public class TesseractScanTests
    {
        [TestMethod()]
        public void ScanForAccountNumberTest()
        {
            var tScan = new TesseractScan();
            /*string fileStr = @"./images/3270_screenshot.jpg";
            ScanData resultData = tScan.ScanForAccountNumber(fileStr);
            Assert.IsNotNull(resultData); */
            // file type gif
            //string fileStr = @"./images/CPD31.gif";
            //string fileStr = @"./images/CPD31.bmp";
            //string fileStr = @"./images/FIG1.jpg";
            //string fileStr = @"./images/FIG1.bmp";
            //string fileStr = @"./images/3270_Sample1.gif";
            string fileStr = @"C:\Downloads\sample1FromWord.jpg";
            List<ScanData> resultData = tScan.ScanForAccountNumber(fileStr);
            Assert.IsNotNull(resultData);

        }

        [TestMethod()]
        public void searchForAccountTest()
        {
            var tScan = new TesseractScan();
            string htmlStr = "<div class='ocr_page' id='page_1' title='image 'syllabus-page1.jpg'; bbox 0 0 2531 3272; ppageno 0'> <div class='ocr_carea' id='block_1_4' title='bbox 265 1183 2147 1778'><p class='ocr_par' dir='ltr' id='par_1_8' title='bbox 274 1305 655 1342'><span class='ocr_line' id='line_1_14' title='bbox 274 1305 655 1342; baseline -0.005 0; x_size 46.378059; x_descenders 10.378059; x_ascenders 12'><span class='ocrx_word' id='word_1_78' title='bbox 274 1307 386 1342; x_wconf 90' lang='eng' dir='ltr'>needs</span><span class='ocrx_word' id='word_1_79' title='bbox 402 1318 459 1342; x_wconf 90' lang='eng' dir='ltr'>are</span><span class='ocrx_word' id='word_1_80' title='bbox 474 1305 655 1341; x_wconf 86' lang='eng' dir='ltr'>different:</span></span> </p></div>  </div>";
            tScan.searchForAccount(htmlStr);
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void maskSensitiveTest()
        {
            var tScan = new TesseractScan();
            string fileStr = @"./images/a14.jpg";
            tScan.maskSensitive(fileStr, 
                tScan.ScanForAccountNumber(fileStr));
            Assert.Fail();
        }
    }
}