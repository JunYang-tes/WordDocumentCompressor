using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Docs
{
    //[Attributes.DocExtension(Extension=".WSP")]
    class Wps:ZipPackage
    {
        public override string GetImagesDir()
        {
            throw new NotImplementedException();
        }
    }
}
