using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;
using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Drawing;

namespace ScanImage
{
    public class TesseractScan
    {
        //TODO  this need to come from a property file
        object  scanRegEx = "";

        public List<ScanData> ScanForAccountNumber(string fileName)
        {
            List<ScanData> resultData = null;
            try
            {
                //using (var engine = new TesseractEngine(@"C:\Downloads\tessdata\tessdata-master\tessdata", "eng", EngineMode.Default))
                using (var engine = new TesseractEngine(@".\tessdata", "eng", EngineMode.Default))
                {
                    //using (var img = Pix.LoadFromFile(fileName))
                    using (Bitmap img = (Bitmap)Image.FromFile(fileName))
                    {
                        using (var page = engine.Process(img))
                        {

                            resultData = searchForAccount(page.GetHOCRText(1, true));
                            Console.WriteLine("HOCR text:" + page.GetHOCRText(1, true));

                        }
                    }
                }
            }
            
            catch(Exception ie)
            {
                Console.WriteLine("Caught exception:" + ie);
            }
            return resultData;
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
                    Console.WriteLine("Testing execution");
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
                            Console.WriteLine("Word Found:" + wordType.InnerText);
                            //Console.WriteLine("Is a matching number:" + matchCardNumber(wordType.InnerText));
                            //if current word match with pattern, mask that area
                            if (matchCardNumber(wordType.InnerText))
                            {
                                Console.WriteLine("Text:" + wordType.OuterHtml);
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


                                foundData.Add(tmpData);
                                Console.WriteLine("Cordinates:" + "X:");
                                   
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

        public void maskSensitive(string fileName, List<ScanData> locations)
        {
            Color maskColor = Color.Red;
            try
            {
                Bitmap currentImage = (Bitmap) Image.FromFile(fileName, false);
                foreach(ScanData item in locations)
                {
                    for (int i=item.bbox[0]; i<= item.bbox[2]; i++)
                    {
                        for(int j=item.bbox[1]; j<=item.bbox[3]; j++)
                        {
                            currentImage.SetPixel(i, j, maskColor);

                        }
                    }
                }
                currentImage.Save(@"C:\Downloads\new\image.jpg");
            }
            catch(Exception ie)
            {
                Console.WriteLine("Exception during Masking:" + ie);
            }
        }

        public void maskPartOfSensitive(string fileName, List<ScanData> locations)
        {
            int startPos = 6;
            int endingPos = 12;

            Color maskColor = Color.Red;
            try
            {
                Bitmap currentImage = (Bitmap)Image.FromFile(fileName, false);
                foreach (ScanData item in locations)
                {
                    //Idea is to find the total length of string
                    //And start masking on the 6th position to the 12th position
                    int strLen = item.foundTxt.Length;
                    int unitLen = (item.bbox[2] - item.bbox[0]) / strLen;
                    for (int i = (item.bbox[0]+ unitLen*(startPos-1)) ; i <= (item.bbox[0]+ unitLen* endingPos) ; i++)
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
    }
}
