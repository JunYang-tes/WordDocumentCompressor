using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor
{
    public class AdvanceCompressor : DocCompressor
    {
        protected Document document;
        public CompressibleInfo GetCompressibleInfo(string file)
        {
            CompressibleInfo info = new CompressibleInfo();
            if (!System.IO.File.Exists(file))
            {
                throw new ArgumentException("No such file:" + file);
            }
            document = Document.GetDocument(file);
            report("Analysing", 2, 0);
            document.UnPackage();
            report("Analysing Image", 2, 1);
            getImagesInfo(document.GetImagesDir(), info);
            report("Done", 2, 2);
            return info;
        }
        public Result execute( CompressOptions options)
        {
            if (document == null) {
                throw new ArgumentException("Must invoke GetCompressibleInfo() before this ");
            }
            Result ret = new Result();
            report("Compressing", 2, 0);
            imgCompressor.execute(document.GetImagesDir(), options);
            report("Compress Done", 2, 1);
            report("Rebuilding", 2, 1);
            document.Package();
            return ret;
        }
        private void getImagesInfo(string dir,CompressibleInfo c)
        {
            foreach (var path in System.IO.Directory.GetFiles(dir))
            {
                ImageInfo img = new ImageInfo()
                {
                    ID = System.IO.Path.GetFileName(path),
                    FullPath = path,
                    Length = (int)new System.IO.FileInfo(path).Length
                };
                c.AddImageInfo(img);
            }
        }
    }
}
