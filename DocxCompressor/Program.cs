using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace DocxCompressor
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String []args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args.Length == 0)
                //Application.Run(new Advance());
                Application.Run(new Form1());
            else
            {
                Application.Run(new Form1(args[0]));
            }
        }
    }
}
