using System;
using System.Collections.Generic;
using System.Text;

namespace Compressor
{
    public abstract class Document
    {
        static Dictionary<string, Type> documents = init();
        static Dictionary<string, Type> init() {
            Dictionary<string, Type> docs = new Dictionary<string, Type>();
            Dictionary<Attribute,Type> ret=scanner.scanNoInstance(typeof(Document), typeof(Attributes.DocExtension));
            foreach (KeyValuePair<Attribute, Type> kv in ret) {
                docs[((Attributes.DocExtension)kv.Key).Extension.ToUpper()] = kv.Value;
            }
            return docs;
        }


        /// <summary>
        /// Get a marched Document object,return null if there is no
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static Document GetDocument(string file) {
            string ex = System.IO.Path.GetExtension(file).ToUpper();
            if (documents.ContainsKey(ex))
            {
                Document doc = Activator.CreateInstance(documents[ex]) as Document;
                doc.setFilePath(file);
                return doc;
            }
            else
                throw new Exceptions.Unsupport("This (" + ex + ") type of file is not supported now");
            
        }
        public string FilePath { get;private set; }
        public Document()
        {
            
        }
        void setFilePath(string path) {
            FilePath = path;
        }
        public abstract string GetImagesDir();
        public abstract string UnPackage();
        public abstract string Package();
    }
}
