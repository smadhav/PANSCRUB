using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    public class ScanData
    {
        public bool isSensitive=false;
        public string foundTxt="";
        public int wConfidence = 0;
        public int[] bbox = new int[4];
        //public List<string> hOCRdata;

    }
}
