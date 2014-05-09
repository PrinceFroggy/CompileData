using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;

namespace RusherData
{
    public class Program1
    {
        public static void Main(string[] args)
        {
            var file = XElement.Load(Directory.GetCurrentDirectory() + "\\Map.img.xml");
            var secondLevelImgdirs = file.Elements("imgdir");

            List<string> result = new List<string>();

            foreach (var imgDir2 in secondLevelImgdirs)
            {
                var name = (string)imgDir2.Attribute("name");
                var thirdLevelImgdirs = imgDir2.Elements("imgdir");
                foreach (var imgDir3 in thirdLevelImgdirs)
                {
                    var id = (string)imgDir3.Attribute("name");
                    var streetName = GetString(imgDir3, "streetName");
                    var mapName = GetString(imgDir3, "mapName");

                    string test = "ID: " + id + " NAME: " + name + ": " + streetName + " : " + mapName;
                    result.Add(test);
                    Console.WriteLine(test);
                }
            }

            File.WriteAllLines(Directory.GetCurrentDirectory() + @"\MapData.txt", result);
        }

        public static string GetString(XElement e, string attribName)
        {
            var element = e.Elements("string").FirstOrDefault(x => attribName == (string)x.Attribute("name"));

            if (element == null)
                return "";

            var value = (string)element.Attribute("value");

            if (value == null)
                return "";

            return value;
        }
    }
}