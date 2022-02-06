using System;
using System.Collections.Generic;
using System.IO;

namespace DirbWin
{
    internal class FileIO
    {
        private string path = string.Empty;

        public FileIO(string path)
        {
            this.path = path;
        }

        private List<string> files = new List<string>();

        public List<string> GetList()
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException("path");
            }
            else
            {
                StreamReader reader = new StreamReader(path);
                string line;

                while ((line = reader.ReadLine()) != null)
                {
                    files.Add(line);
                }

                reader.Close();
            }

            return files;
        }
    }
}