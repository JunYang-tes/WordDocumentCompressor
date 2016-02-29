using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Compressor.ImgCompress
{
    abstract class Compressor
    {
        public CompressOptions Option { get; set; }
        public CompressInfo Compress(string path,CompressOptions option)
        {
            this.Option = option;
            CompressInfo info = new CompressInfo();
            info.Before = (int)new System.IO.FileInfo(path).Length;
            compressImpl(path);
            info.After = (int)new System.IO.FileInfo(path).Length;
            return info;
        }
        abstract protected void compressImpl(string file);


    }

    abstract class ExternalCompressor : Compressor
    {
        /// <summary>
        /// 子类实现该方法，用来返回特定的外部程序的参数，如果返回NULL，则本次不调用该外部程序
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected abstract string getArgument(string file);
        protected abstract string getProgram();
        protected override void compressImpl(string file)
        {
            utils.assertExist(getProgram());
            string args = getArgument(file);
            if (args == null)
                return;

            ProcessStartInfo startInfo = new ProcessStartInfo(utils.getPath(getProgram()));

            startInfo.Arguments = args;
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardOutput = true;



            Process p = Process.Start(startInfo);

            Debug.WriteLine(p.StandardOutput.ReadToEnd());
            Debug.WriteLine(p.StandardError.ReadToEnd());

            p.WaitForExit();
            aftercompress(file);
        }
        protected virtual void aftercompress(string file) { }
    }


}
