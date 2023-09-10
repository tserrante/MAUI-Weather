
namespace WeatherApp.Services
{
    public static class FileService
    {
        private static string savePath = string.Concat(FileSystem.Current.AppDataDirectory, "\\WeatherImages\\");
        private static string fileNamePrepend = "img";
        private static string fileNameAppend = "_2x.png";

        public static string SaveIconToAppData(byte[] imageArray, string iconId)
        {
            string fullFilePath = string.Concat(savePath,
                                                fileNamePrepend,
                                                iconId, 
                                                fileNameAppend);
            try
            {
                if (!Directory.Exists(savePath))
                {
                    Directory.CreateDirectory(savePath);
                }

                File.WriteAllBytes(fullFilePath, imageArray);

                return fullFilePath;
            }
            catch( Exception ex )
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        public static bool isIconDownloaded(string iconId)
        {
            string fullFilePath = string.Concat(savePath,
                                    fileNamePrepend,
                                    iconId,
                                    fileNameAppend);

            if(File.Exists(fullFilePath))
                return true;

            return false;

        }

        public static string GetIconPath(string iconId)
        {
            string fullFilePath = string.Concat(savePath,
                                    fileNamePrepend,
                                    iconId,
                                    fileNameAppend);

            if (File.Exists(fullFilePath))
                return fullFilePath;

            return null;
        }
    }

}
