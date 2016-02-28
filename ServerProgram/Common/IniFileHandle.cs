using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Runtime.InteropServices;


namespace DevExpress.ProductsDemo.Win.Common
{
    [Serializable]
    public class IniFileHandle
    {
                /********************생성자*******************/
        public IniFileHandle(string INIPath)
        {
            path = INIPath;
            System.IO.FileInfo cFileInfo = new System.IO.FileInfo(INIPath);
            if (!cFileInfo.Exists) FileCreate(cFileInfo);
        }
        public IniFileHandle()
        {

        }

        /********************소멸자*******************/
        ~IniFileHandle()
        {  }

        /********************정  의*******************/
        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,            string key, string val, string filePath);        
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,                 string key, string def, StringBuilder retVal,            int size, string filePath);
        
        /********************변  수*******************/
        public string path;
        
        /********************메소드*******************/
        public void FileWrite(string Value)
        {
            FileWrite(Value, path.ToString());
        }

        public void FileWrite(string Value, string FullName)
        {

            try
            {
                using (System.IO.StreamWriter w = System.IO.File.AppendText(FullName))
                {
                    w.WriteLine(Value);

                    w.Flush();
                    w.Close();
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.ToString());
            }

        }


        /// <summary>
        /// Write Data to the INI File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// Section name
        /// <PARAM name="Key"></PARAM>
        /// Key Name
        /// <PARAM name="Value"></PARAM>
        /// Value Name
        public void IniWriteValue(string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, this.path);
        }

        /// <summary>
        /// Read Data Value From the Ini File
        /// </summary>
        /// <PARAM name="Section"></PARAM>
        /// <PARAM name="Key"></PARAM>
        /// <PARAM name="Path"></PARAM>
        /// <returns></returns>
        public string IniReadValue(string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, this.path);
            return temp.ToString();

        }
        public string IniReadValue(string Section, string Key, string Default)
        {
            try
            {
                StringBuilder temp = new StringBuilder(255);
                int i = GetPrivateProfileString(Section, Key, "", temp,
                                                255, this.path);

                return (temp.Length == 0 ? Default.ToString() : temp.ToString());
            }
            catch
            {
                return Default;
            }


        }
        private bool FileCreate(System.IO.FileInfo cFileInfo)
        {
        reFileCreate:
            bool bCreateFile = false;
            if (!cFileInfo.Exists)
            {
                try
                {

                    System.IO.FileStream fstr = cFileInfo.Create();
                    fstr.Close();
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    if (CreatePath(cFileInfo.DirectoryName))
                    {
                        goto reFileCreate;
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return bCreateFile;
        }

        public  bool CreatePath(string FullPathDirectory)
        {
            System.Collections.ArrayList strFullPath = GetPathDirectory(FullPathDirectory);
            //throw new System.NotImplementedException();
            bool bCreatePath = false;
            foreach (string path in strFullPath)
            {
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                    bCreatePath = true;
                }
            }
            return bCreatePath;
        }

        private System.Collections.ArrayList GetPathDirectory(string FullPathDirectory)
        {
            int start = 4;

            System.Collections.ArrayList path = new System.Collections.ArrayList();

            if (FullPathDirectory != null)
            {
                while (start < FullPathDirectory.Length)
                {
                    start = FullPathDirectory.IndexOf('\\', start) + 2;
                    try
                    {
                        path.Add(FullPathDirectory.Substring(0, start - 2));
                    }
                    catch (System.ArgumentOutOfRangeException)
                    {
                        path.Add(FullPathDirectory);
                        return path;
                    }
                }
            }
            return path;
            //throw new System.NotImplementedException();
        }




        
    }
}
