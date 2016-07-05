using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWord = Microsoft.Office.Interop.Word;

namespace ScanImage
{
    interface MSScanner
    {
         void extractImages(string fileName);
        void replaceImage(Object MSDocument, string loc);

    }
}
