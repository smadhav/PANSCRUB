using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    public class ImgScanData
    {
        public Size imgSize;
        public string imgName;
        public ImageFormat imgFormat;
        public bool isScanCompleted;
        public List<ScanData> scanData;


        public override string ToString()
        {
            var retStr = "\n";
            if (isScanCompleted == false )
            {
                return retStr;
            }
            else
            {
                if (scanData == null || scanData.Count == 0)
                {
                    retStr = "\n Image does not have pattern";
                }
                else
                {
                    foreach (ScanData item in scanData)
                    {
                        retStr = retStr + "\n\t\t" +
                            "Text found: " + item.foundTxt +
                                " Confidence: " + item.wConfidence;
                    }
                }
                return retStr;
            }

        }
    }
}
