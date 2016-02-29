using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Exceptions
{
    public class UnKnowException:Exception
    {
        public UnKnowException(string message) : base(message) { 
        
        }
    }
}
