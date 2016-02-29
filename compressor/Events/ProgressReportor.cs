using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Events
{
    public  class ProgressReportor :EventArgs
    {
        public String Text { get; private set; }
        public int Totle { get; set; }
        public int Done { get; set; }

        public ProgressReportor(String text,int totle,int done) {
            Text = text;
            Totle = totle;
            Done = done;
        }
    }
}
