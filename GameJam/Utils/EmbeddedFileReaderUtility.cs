using System.Drawing;
using System.IO;
using System.Reflection;

namespace GameJam.Utils
{
    public static class EmbeddedFileReaderUtility
    {
        public static string ReadFile(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "GameJam." + path;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public static Image ReadImage(string path)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "GameJam." + path;

            return Bitmap.FromStream(assembly.GetManifestResourceStream(resourceName));
        }
    }
}