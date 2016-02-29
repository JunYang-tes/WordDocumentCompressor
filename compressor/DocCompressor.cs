using System;
using System.Collections.Generic;
using System.Text;
using Compressor.Events;
using Compressor.ImgCompress;
using System.IO;

namespace Compressor
{
    public class DocCompressor
    {

        public event EventHandler<Events.ProgressReportor> PregressReport;
        public event EventHandler<Events.ProgressReportor> SubPregressReport;

        protected ImageCompressor imgCompressor;
        public DocCompressor()
        {
            imgCompressor = new ImageCompressor();
            imgCompressor.PregressReport += new EventHandler<ProgressReportor>(imgCompressor_PregressReport);
        }

        void imgCompressor_PregressReport(object sender, ProgressReportor e)
        {
            onSubPregressReport(e);
        }
        protected virtual void onPregressReport(ProgressReportor e)
        {
            if (PregressReport != null)
            {
                PregressReport(this, e);
            }
        }
        protected virtual void onSubPregressReport(ProgressReportor e)
        {
            if (SubPregressReport != null)
            {
                SubPregressReport(this, e);
            }
        }

        protected void report(string text, int total, int done)
        {
            ProgressReportor e = new ProgressReportor(text, total, done);
            onPregressReport(e);
        }

        /// <summary>
        /// Compress a file then return infomation
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public Result execute(string file, CompressOptions options)
        {
            Result ret = new Result();


            if (!System.IO.File.Exists(file))
            {
                throw new ArgumentException("No such file:" + file);
            }

            ret.OldSize = new FileInfo(file).Length;

            Document doc = Document.GetDocument(file);

            ProgressReportor e = new ProgressReportor("Analysing", 3, 0);
            onPregressReport(e);
            doc.UnPackage();
            e = new ProgressReportor("Analyse Done!", 3, 1);
            onPregressReport(e);
            e = new ProgressReportor("Compressing ", 3, 1);
            onPregressReport(e);


            string imagedir = doc.GetImagesDir();
            CompressInfo info = imgCompressor.execute(imagedir, options);
            e = new ProgressReportor("Compress Done!", 3, 2);
            onPregressReport(e);



            e = new ProgressReportor("Rebuilding ", 3, 2);
            onPregressReport(e);
            string newFile = doc.Package();

            e = new ProgressReportor("Rebuild Done ", 3, 3);
            onPregressReport(e);

            ret.OutFile = newFile;
            ret.NewSize = new FileInfo(file).Length;
            ret.Rate = ret.NewSize / (ret.OldSize + 0.0F);
            return ret;
        }
    }

    public struct Result
    {
        public String OutFile { get; set; }
        public long OldSize { get; set; }
        public long NewSize { get; set; }
        public float Rate { get; set; }
    }

    public class CompressibleInfo
    {
        List<ImageInfo> Images { get; set; }
        public CompressibleInfo() {
            Images = new List<ImageInfo>();
        }
        public void AddImageInfo(ImageInfo imgInfo) {
            Images.Add(imgInfo);
        }
    }
    public class ImageInfo {
        public String FullPath { get; set; }
        public string ID { get; set; }
        public int Length { get; set; }
    }
}
