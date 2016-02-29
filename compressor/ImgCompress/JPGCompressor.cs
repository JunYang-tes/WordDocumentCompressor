using System;
using System.Collections.Generic;
using System.Text;
using Compressor.Attributes;

namespace Compressor.ImgCompress
{
    [ImageCompressorInfo(Extension = ".JPG")]
    class JPGCompressor :IMConvertCompressor
    {
        //protected override string getArgument(string file)
        //{
        //    return string.Format("-optimize \"{0}\" \"{1}\"", file, file);
        //}

        //protected override string getProgram()
        //{
        //    return "jpegtran.exe";
        //}
    }
    [ImageCompressorInfo(Extension = ".JPEG")]
    class JPGECompressor : JPGCompressor
    {

    }
    [ImageCompressorInfo(Extension = ".PNG")]
    class PNGCompressor : IMConvertCompressor { 
    
    }
}
