using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Services
{
    public class FileService
    {
        private static string savePath = string.Concat(FileSystem.Current.AppDataDirectory, "\\WeatherImages\\");
        private static string fileNamePrepend = "img";
        private static string fileNameAppend = "_2x.png";

        public static string SaveIconToAppData(byte[] imageArray, string iconName)
        {
            string fullFilePath = string.Concat(savePath,
                                                fileNamePrepend, 
                                                iconName, 
                                                fileNameAppend);
            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                System.IO.File.WriteAllBytes(fullFilePath, imageArray);

                return fullFilePath;
            }
            catch( Exception ex )
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
