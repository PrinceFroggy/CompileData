using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace RusherData
{
    public class Program3
    {
        public static void Main(string[] args)
        {
            string filepath = Directory.GetCurrentDirectory();
            DirectoryInfo d = new DirectoryInfo(filepath);

            List<string> result = new List<string>();

            foreach (var file in d.GetFiles("*.xml"))
            {
                string test = getValuesOneFile(file.ToString());

                result.Add(test);
                Console.WriteLine(test);
            }

            File.WriteAllLines(filepath + @"\LadderData.txt", result);
        }

        public static string getValuesOneFile(string fileName)
        {
            string finalString = "";

            XElement xmlDocument = XElement.Load((fileName));

            XElement infoImgDir = GetImgDir(xmlDocument, "info");

            int index = fileName.IndexOf(".img");
            string nametest = fileName;

            finalString += "[" + nametest.Substring(0, index) + "]\n";

            try
            {
                finalString += "total=" + GetChildrenCount(GetImgDir(xmlDocument, "ladderRope")) + "\n";

                IEnumerable<XElement> ladders = GetImgDir(xmlDocument, "ladderRope").Elements();

                foreach (XElement currentLadders in ladders)
                {
                    finalString += GetInt(currentLadders, "x") + " ";
                    finalString += GetInt(currentLadders, "y1") + " ";
                    finalString += GetInt(currentLadders, "y2") + "\n";
                }
            }
            catch
            {
                finalString += "total=0\n";
            }

            return finalString;
        }

        public static XElement GetImgDir(XElement file, string imgDirName)
        {
            return file.Descendants("imgdir").FirstOrDefault(x => (string)x.Attribute("name") == imgDirName);

        }

        public static int GetInt(XElement data, string attribName)
        {
            var element = data.Descendants("int").FirstOrDefault(x => (string)x.Attribute("name") == attribName);

            if (element == null)
                return 0;

            var value = (int?)element.Attribute("value");

            if (value == null)
                return 0;

            return value.Value;
        }

        public static string GetString(XElement data, string attribName)
        {
            var element = data.Descendants("string").FirstOrDefault(x => (string)x.Attribute("name") == attribName);

            if (element == null)
                return "";

            var value = (string)element.Attribute("value");

            if (value == null)
                return "";

            return value;
        }

        public static int GetChildrenCount(XElement data)
        {
            return data.Elements().Count();
        }
    }
}