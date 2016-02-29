using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Attributes
{
    class ImageCompressorInfo : Attribute {
        public string Extension { get; set; }
    }
    class DocExtension : Attribute {
        public string Extension { get; set; }
    }
}
