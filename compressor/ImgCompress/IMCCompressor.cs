using System;
using System.Collections.Generic;
using System.Text;
using Compressor.Attributes;

namespace Compressor.ImgCompress
{
    
    class IMConvertCompressor:ExternalCompressor
    {
        protected override string getArgument(string file)
        {
            String imageName = System.IO.Path.GetFileNameWithoutExtension(file);
            imageName = imageName.Substring(imageName.LastIndexOf("\\"));
            Strategy strategy = base.Option.GetStrategy(imageName);
            if (strategy == null)
                return string.Format("-strip -quality {0}% \"{1}\" \"{2}\"", this.Option.Quality, file, file + ".jpg");
            else {
                return format(strategy);
            }
        }
        /// <summary>
        /// 通过策略，格式化convert 参数
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        private string format(Strategy strategy) {
            if (strategy.Exclude)
            {
                return null;
            }
            else {
                StringBuilder sb = new StringBuilder();
                sb.Append(" -strip ");
                if (strategy.EnableResize) {
                    sb.Append(" -resize ");
                    if (strategy.Size != null)
                    {
                        sb.Append(" " + strategy.Size.ToString() + " ");
                    }
                    else {
                        sb.Append(" " + strategy.Resize + "% ");
                    }
                }
                if (strategy.EnableQuality) {
                    sb.Append(" -quality ");
                    sb.Append(strategy.Quality + "%");
                }
                return "";
            }
        }

        protected override string getProgram()
        {
            return "convert.exe";
        }
        protected override void aftercompress(string file)
        {
            System.IO.File.Delete(file);
            System.IO.File.Move(file + ".jpg", file);
        }
    }
}
