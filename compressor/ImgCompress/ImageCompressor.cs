using System;
using System.Collections.Generic;
using System.Text;
using Compressor.Attributes;
using System.Diagnostics;
using Compressor.Events;

namespace Compressor.ImgCompress
{
    public class ImageCompressor
    {
        string[] supported = { ".JPG",".PNG",".JPEG",".GIF",".BMP"};
        public event EventHandler<Events.ProgressReportor> PregressReport;
        protected virtual void onPregressReport(ProgressReportor e)
        {
            if (PregressReport != null)
            {
                PregressReport(this, e);
            }
        }

        static Dictionary<string, Compressor> compressors = init();

        static Dictionary<string, Compressor> init()
        {
            Dictionary<string, Compressor> dict = new Dictionary<string, Compressor>();
            Dictionary<Attribute, Object> ret = scanner.scan(typeof(Compressor), typeof(Attributes.ImageCompressorInfo));
            foreach (KeyValuePair<Attribute, Object> kv in ret)
            {
                dict[((ImageCompressorInfo)kv.Key).Extension.ToUpper()] = kv.Value as Compressor;
            }
            return dict;
        }

        public CompressInfo execute(string path,CompressOptions option)
        {
            int before = 0;
            int after = 0;
            string[] files = System.IO.Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                report("Compressing Image", files.Length, i);
                string ex = System.IO.Path.GetExtension(files[i]).ToUpper();
                if (!isImage(ex))
                {
                    continue;
                }
                Compressor com = getCompressor(ex);
                if (com == null)
                    continue;
                System.IO.File.Copy(files[i], files[i] + ".old", true);
                CompressInfo info = com.Compress(files[i],option);
                before += info.Before;
                after += info.After;
                if (info.After > info.Before)
                {
                    System.IO.File.Delete(files[i]);
                    
                    System.IO.File.Move(files[i] + ".old", files[i]);
                 
                }
                 
                System.IO.File.Delete(files[i] + ".old");
            }
            report("Compressed Images", files.Length, files.Length);
            return new CompressInfo() { Before = before, After = after, Precentage = after / (before + 0.0) };
        }
        private Compressor getCompressor(string ex)
        {
            ex = ex.ToUpper();
            if (compressors.ContainsKey(ex)) {
                return compressors[ex];
            }
            return compressors["*"];
        }
        private void report(string text, int total, int done)
        {
            ProgressReportor e = new ProgressReportor(text, total, done);
            onPregressReport(e);
        }
        private bool isImage(string ex) {
            foreach (string s in supported) {
                if (s.Equals(ex)) {
                    return true;
                }
            }
            return false;
        }
    }

    public struct CompressInfo
    {
        public int Before { get; set; }
        public int After { get; set; }
        public double Precentage { get; set; }

    }


}
