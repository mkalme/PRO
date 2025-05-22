using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace PRO
{
    class read
    {
        public static String start(String filePath, String typeOfFile)
        {
            String text = "";

            try
            {
                text = check(filePath, typeOfFile);
            }
            catch {
            }

            return convert.convertFrom(text);
        }

        public static String check(String filePath, String typeOfFile) {
            String text = "";

            string[] Files = Directory.GetFiles(filePath);

            for (int i = 0; i < Files.Length; i++)
            {
                if (convert.convertFrom(Path.GetFileName(Files[i])).Equals(typeOfFile)) {
                    text = Read(Files[i]);
                    i = Files.Length;
                    break;
                }   
            }

            return text;
        }

        public static String Read(String filePath) {
            String text = "";

            using (StreamReader sr = File.OpenText(filePath))
            {
                string s = String.Empty;
                while ((s = sr.ReadLine()) != null)
                {
                    text += s;
                }
            }

            return text;
        }
    }
}
