using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScanImage
{
    class ImgScanData
    {
        Size imgSize;
        string imgName;
        ImageFormat imgFormat;
        List<ScanData> scanData;
    }
}
