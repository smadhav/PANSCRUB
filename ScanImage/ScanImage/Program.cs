using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName;
            string opName;
            if (args.Length != 2)
            {
                Console.WriteLine("Usage ScanImage filename scan[mask]");
                return;
            }
            
                fileName = args[0];
                opName = args[1];
           
           
            var myEx = new MSWordScanner();
            DocScanData result = null;
            if (opName.Equals("scan") ){
                result = myEx.ScanForPattern(fileName);
            }
            else
            { if (opName.Equals("mask"))
                {
                    result = myEx.ScanAndFixPattern(fileName);
                }

            }
            if(result != null)
            {
                Console.WriteLine("Scan detail\n" + result.ToString());
            }
        }


    }
}
