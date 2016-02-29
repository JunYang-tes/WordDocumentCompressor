using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Exceptions
{
    public class Unsupport :Exception
    {
        public Unsupport(String message) : base(message) { 
        
        }
    }
}
