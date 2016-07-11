using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace ScanImage
{
    public class TesseractScan
    {
        public ImgScanData ScanForAccountNumber(string fileName)
        {
            ImgScanData resultData = null;
            try
            {
                //using (var engine = new TesseractEngine(@"C:\Downloads\tessdata\tessdata-master\tessdata", "eng", EngineMode.Default))
                //using (var img = Pix.LoadFromFile(fileName))
                    using (Bitmap img = (Bitmap)Image.FromFile(fileName))
                    {
                        resultData = ScanForAccountNumber(img);
                        
                    }
            }
            
            catch(Exception ie)
            {
                Console.WriteLine("Caught exception:" + ie);
            }
            return resultData;
        }

        //Overload method to accept BMP image in memory
        public ImgScanData ScanForAccountNumber(Bitmap bmpImage)
        {
            var imgData = new ImgScanData();
            imgData.imgSize = bmpImage.Size;
            imgData.imgFormat = bmpImage.RawFormat;
            imgData.isScanCompleted = false;
            imgData.scanData = null;
            try
            {
                using (var engine = new TesseractEngine(@".\tessdata", "eng", EngineMode.Default))
                {
                    engine.SetVariable("tessedit_char_whitelist", "0123456789");
                    using (var page = engine.Process(bmpImage))
                        {
                            imgData.scanData = searchForAccount(page.GetHOCRText(1, true));
                            //Console.WriteLine("HOCR text:" + page.GetHOCRText(1, true));
                        }
                }
                imgData.isScanCompleted = true;
            }
            catch (Exception ie)
            {
                Console.WriteLine("Caught exception:" + ie);
                imgData.isScanCompleted = false;
            }
            return imgData;
        }
        
        public  List<ScanData> searchForAccount(string hocrData)
        {
            var foundData = new List<ScanData>();
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(hocrData);
            if(htmlDoc.ParseErrors!= null && htmlDoc.ParseErrors.Count() > 0)
            {
                Console.WriteLine("Parse Error:" + htmlDoc.ParseErrors.ToString());
            }
            else
            {
                if (htmlDoc.DocumentNode != null)
                {
                    //Get a collection of nodes with ID word_*
                    HtmlNodeCollection wordNodes = htmlDoc.DocumentNode.SelectNodes("//*[@id[starts-with(.,'word_')]]");
                    if (wordNodes == null)
                    {
                        Console.WriteLine("Empty collection of words found");
                    }
                    else
                    {
                        foreach (HtmlNode wordType in wordNodes)
                        {
                            
                            //Console.WriteLine("Is a matching number:" + matchCardNumber(wordType.InnerText));
                            //if current word match with pattern, mask that area
                            if (matchCardNumber(wordType.InnerText))
                            {
                                //Console.WriteLine("Text:" + wordType.OuterHtml);
                                //TODO This is klude way, this need to be rewritten
                                var outerStr = wordType.OuterHtml;
                                string[] splitter = new string[] { "title=" };
                                char[] sep = { ' ', ';' };
                                var tokens = outerStr.Split(splitter, 5, StringSplitOptions.RemoveEmptyEntries)[1].Split(sep);
                                /*HtmlDocument subDoc = new HtmlDocument();
                                subDoc.LoadHtml(wordType.OuterHtml);
                                List<string> corItems = subDoc.DocumentNode.SelectNodes("tokenize(@title,' ')");
                                */
                                var tmpData = new ScanData();
                                tmpData.foundTxt = wordType.InnerText;
                                tmpData.isSensitive = true;
                                tmpData.bbox[0] = Int32.Parse(tokens[1]);
                                tmpData.bbox[1] = Int32.Parse(tokens[2]);
                                tmpData.bbox[2] = Int32.Parse(tokens[3]);
                                tmpData.bbox[3] = Int32.Parse(tokens[4]);
                                if(tokens.Count() >=8 && tokens[6].Equals("x_wconf"))
                                {
                                    tmpData.wConfidence = Convert.ToInt32(tokens[7].Replace("'"," "));
                                }


                                foundData.Add(tmpData);
                                //var title = wordType.Attributes["tittle"].Value;
                               // Console.WriteLine(" Title" + title);


                            }

                        }
                    }
                   
                }
            }

            return foundData;
        }

        private bool matchCardNumber(string testStr)
        {
            if (testStr != null && Regex.IsMatch(testStr,
                @"^4[0-9]{12}(?:[0-9]{3})?$"))
                //@"^1[0]{2}\.[0]"))
            {
                //^4[0-9]{12}(?:[0-9]{3})?$))

                return true;
            }
            else return false;
        }

        

        
    }
}
