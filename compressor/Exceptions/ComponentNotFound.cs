using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor.Exceptions
{
    public class ComponentNotFound :Exception
    {
        public String Name { get; private set; }
        public ComponentNotFound(string name) {
            Name = name;
        
        }
    }
}
