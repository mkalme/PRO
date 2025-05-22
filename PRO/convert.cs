using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PRO
{
    class convert
    {
        private static String[] array = {"n$S=", "LPqg", "'wXh", "X8Ex", "A@kE", "tX9~", "$&AC", "^dCn", "ldYK", "P@Tb", "+!hA", "f95J", "cMS5", "+Tv6", "iT90", "1Q(D", "Wixs", "[vJ!", "i);s", "ow8i", "(@^2", "1%d9", "kFay", "%FQX", "vFgz", "@AEi", "#QJ)", "2$Fo", "=O&p", "pu4K", "%b)8", "8MzE", "WayK", "3;Kh", "xsn^", "cczg", ";n^U", "A_f!", "3=v[", "7-QT", "S%IY", "'w^!", "$6uz", "hSfB", "dB8I", "mH%u", "wx9s", "CmQl", "DX,]", ";Ca&", "2]Q#", "gh8^", "UVut", "s)f-", "fQ_,", "_h_6", "j)Y1", "I('C", "^282", "s#02", "bYAj", "tZv;", "p{]h", "bTGH", "z=8^", "0(6M", "PU7c", "pU8{", "COI%", "Js@0", "1B@J", "Kl_)", "in4W", "iP#Y", "Ep;B", "F6Hj", "f_H3", ",Bz(", "{(Ol", "ik#4", "s-w!", "D^4o", "MmaV", "Y{ix", "v#bi", "tMzJ", "q+Yb", "!5nW", "n'K8", "fGv(", "LUe%", "_FM~", "@Ssf", ")8hI", "1P08"};
        public static String characters = " !\"#$%&'()*+,-./0123456789:;<=>?@ABCDEFGHIJKLMNOPQRSTUVWXYZ[\\]^_`abcdefghijklmnopqrstuvwxyz{|}~";

        //private static String charactersCon = "!#$%&'()+,-0123456789;=@ABCDEFGHIJKLMNOPQRSTUVWXYZ[]^_abcdefghijklmnopqrstuvwxyz{~";
        private static String salt = "`}";
        private static Random ran = new Random();

        /*public static void test() {
            int numOfChar = 4;
            String text = "";
            Random ran = new Random();

            for (int i = 0; i < characters.Length; i++) {
                text += "\"";
                text += charactersCon[ran.Next(charactersCon.Length)];
                text += charactersCon[ran.Next(charactersCon.Length)];
                text += charactersCon[ran.Next(charactersCon.Length)];
                text += charactersCon[ran.Next(charactersCon.Length)];
                text += "\", ";
            }
            Debug.WriteLine(text);
        }*/

        public static String convertTo(String textToConvert) {
            String text = "";

            for (int i = 0; i < textToConvert.Length; i++) {
                text += salting(array[index(textToConvert[i])]);
            }

            return text;
        }

        public static String convertFrom(String textFrom)
        {
            String text = "";

            for (int i = 0; i < salt.Length; i++) {
                textFrom = textFrom.Replace(salt[i].ToString(), "");
            }

            for (int i = 0; i < textFrom.Length; i += 4) {
                String substring = textFrom.Substring(i, 4);
                text += characters[indexFrom(substring)];
            }

            return text;
        }
        public static String salting(String text)
        {
            int ifInsert = ran.Next(2);
            int locationChar = ran.Next(4);

            text = (ifInsert == 1 ? text.Insert(locationChar, salt[ran.Next(salt.Length)].ToString()) : text);

            return text;
        }

        public static int indexFrom(String text)
        {
            int index = 0;

            for (int i = 0; i < array.Length; i++)
            {
                if (text.Equals(array[i]))
                {
                    index = i;
                    i = array.Length;
                    break;
                }
            }

            return index;
        }

        public static int index(char ch) {
            int index = 0;

            for (int i = 0; i < characters.Length; i++) {
                if (ch.Equals(characters[i])) {
                    index = i;
                    i = characters.Length;
                    break;
                }
            }

            return index;
        }
    }
}
