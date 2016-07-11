using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    public class DocScanData
    {
        public string docFileName;
        public bool isScanCompleted;
        public int errorNumber;
        public int numberOfImg;
        public List<ImgScanData> imgData;

        public DocScanData(string fileName)
        {
            this.docFileName = fileName;
            this.isScanCompleted = false;
            this.errorNumber = 0;
            this.numberOfImg = 0;
            imgData = new List<ImgScanData>();

        }
        public override string ToString()
        {
            if (docFileName == null ||
                isScanCompleted == false ||
                errorNumber != 0
                )
            {
                return "";
            }
            else
            {
                var retStr = "FileName:" + docFileName + "\n";
                retStr = "Number of images found:" + numberOfImg;
                int i =0;
                foreach(ImgScanData item in imgData)
                {
                    i++;
                    if(item.isScanCompleted && item.scanData.Count >0) { 
                        retStr = retStr + "\n" + "Image #:" + i +
                            item.ToString();
                    }
                }

                return retStr;
            }
            
        }
    }
}
