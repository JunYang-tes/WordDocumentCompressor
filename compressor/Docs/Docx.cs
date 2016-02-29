using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Docs
{
    [Attributes.DocExtension(Extension=".DOCX")]
    class Docx:ZipPackage
    {
        public override string GetImagesDir()
        {
            return tmpdir + @"\word\media";
        }
    }
}
