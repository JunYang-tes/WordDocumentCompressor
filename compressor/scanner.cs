using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Diagnostics;

namespace Compressor
{
    class scanner
    {
        public static Dictionary<Attribute, Object> scan(Type superType,Type attributeType) {
            

            Dictionary<Attribute, Object> dict = new Dictionary<Attribute, object>();
            Type []ts=Assembly.GetAssembly(typeof(scanner)).GetTypes();
            foreach (Type t in ts) {
                if (t.IsSubclassOf(superType)) {
                    Object []bs=t.GetCustomAttributes(attributeType, false);
                    if (bs != null && bs.Length != 0) {
                        dict[(Attribute)bs[0]] = Activator.CreateInstance(t);
                    }
                }
            }
            return dict;
        }

        public static Dictionary<Attribute, Type> scanNoInstance(Type superType, Type attributeType)
        {
            Dictionary<Attribute, Type> dict = new Dictionary<Attribute, Type>();
            Type[] ts = Assembly.GetAssembly(typeof(scanner)).GetTypes();
            foreach (Type t in ts)
            {
                if (t.IsSubclassOf(superType))
                {
                    Object[] bs = t.GetCustomAttributes(attributeType, false);
                    if (bs != null && bs.Length != 0)
                    {
                        dict[(Attribute)bs[0]] = t;
                    }
                }
            }
            return dict;
        }

    }
    class utils {
        public static string getPath(string file) {
            return System.IO.Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)+"\\"+file;
        }
        public static void assertExist(string file) {
            if (!System.IO.File.Exists(getPath(file))) {
                throw new Exceptions.ComponentNotFound(file);
            }
        }
        public static void debugout(ProcessStartInfo info) {
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            
        }

        internal static void deleteDir(string tmpdir)
        {
            foreach(string file in System.IO.Directory.GetFiles(tmpdir)){
                System.IO.File.Delete(file);
            }
            foreach (string dir in System.IO.Directory.GetDirectories(tmpdir)) {
                deleteDir(dir);
            }
            System.IO.Directory.Delete(tmpdir);
        }
        static void delete() { 
        
        }
    }
}
