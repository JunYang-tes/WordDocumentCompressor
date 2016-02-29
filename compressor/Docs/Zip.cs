using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace Compressor.Docs
{
   abstract class ZipPackage:Document {

        string programPath = utils.getPath("7z.exe");
        string fileList = utils.getPath("zip_file_list"); 
       protected string tmpdir = utils.getPath("zip_tmp");
        
       

        public override string UnPackage()
        {
            utils.assertExist("7z.exe");
            ProcessStartInfo info = new ProcessStartInfo(programPath,string.Format(" x -y -o\"{0}\" \"{1}\"",tmpdir,FilePath));
            info.CreateNoWindow = true;
            info.RedirectStandardOutput = true;
            info.RedirectStandardError = true;
            info.UseShellExecute = false;


            prepare();
            Process p = Process.Start(info);
            Debug.WriteLine(p.StandardError.ReadToEnd());
            Debug.WriteLine(p.StandardOutput.ReadToEnd());
            p.WaitForExit();
            if (p.ExitCode != 0) { 
                //somthing wrong
            }
            return tmpdir;
        }
        private void prepare(){
            if (System.IO.Directory.Exists(tmpdir)) { 
                
                utils.deleteDir(tmpdir);
            }
        }
        private void prepare_package() {
            if (!System.IO.Directory.Exists(tmpdir))
            {
                throw new Exceptions.UnKnowException("Something WRONG");
            }
            System.IO.StreamWriter sw=System.IO.File.CreateText(fileList);
            foreach (string s in System.IO.Directory.GetDirectories(tmpdir)) {
                sw.WriteLine("\""+s+"\"");
            }
            foreach (string s in System.IO.Directory.GetFiles(tmpdir)) {
                sw.WriteLine("\"" + s + "\"");
            }
            sw.Close();
        }

        public override string Package()
        {
            string newName=System.IO.Path.GetDirectoryName(FilePath)+"\\"+
                System.IO.Path.GetFileNameWithoutExtension(FilePath)+"-resize"+
                System.IO.Path.GetExtension(FilePath);
            utils.assertExist("7z.exe");
            ProcessStartInfo info = new ProcessStartInfo(programPath, string.Format(" a -tzip \"{0}\" @\"{1}\"",newName,fileList));
            info.CreateNoWindow = true;
            info.UseShellExecute = false;
            info.RedirectStandardOutput = true;
            prepare_package();
            Process p=Process.Start(info);
            Debug.WriteLine(p.StandardOutput.ReadToEnd());
            p.WaitForExit();
            
            if(p.ExitCode!=0){
                throw new Exceptions.UnKnowException("Something wrong during rebuilding");
            }
            return newName;
        }
    }


}
