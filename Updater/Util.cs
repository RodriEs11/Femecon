using System;
using System.IO;
using System.IO.Compression;


namespace Updater
{

    public static class ZipArchiveExtensions
    {
        public static void descomprimirZip(this ZipArchive archive, string destinationDirectoryName, bool overwrite)
        {
            if (!overwrite)
            {
                archive.ExtractToDirectory(destinationDirectoryName);
                return;
            }

            DirectoryInfo di = Directory.CreateDirectory(destinationDirectoryName);
            string destinationDirectoryFullPath = di.FullName;

            foreach (ZipArchiveEntry file in archive.Entries)
            {
                string completeFileName = Path.GetFullPath(Path.Combine(destinationDirectoryFullPath, file.FullName));

                if (!completeFileName.StartsWith(destinationDirectoryFullPath, StringComparison.OrdinalIgnoreCase))
                {
                    throw new IOException("Trying to extract file outside of destination directory. See this link for more info: https://snyk.io/research/zip-slip-vulnerability");
                }

                if (file.Name == "")
                {// Assuming Empty for Directory
                    Directory.CreateDirectory(Path.GetDirectoryName(completeFileName));
                    continue;
                }
                file.ExtractToFile(completeFileName, true);
            }
        }
    }

    class Util
    {


        public static void borrarArchivosMenos(string origen, string startsWith)
        {

            string[] archivos = Directory.GetFiles(origen);
            for (int i = 0; i < archivos.Length; i++)
            {

                string[] split = archivos[i].Split('\\', '/', '.');

                if (!split[split.Length - 2].StartsWith(startsWith))
                {

                    File.Delete(archivos[i]);
                }

            }


        }



        public static void borrarArchivos(string origen)
        {

            borrarArchivosMenos(origen, "Updater");


            string[] directorios = Directory.GetDirectories(origen);


            for (int i = 0; i < directorios.Length; i++)
            {
                // Borra todos los archivos en todas las carpetas que tenga, menos las que empiecen por Updater
                borrarArchivos(directorios[i]);
            }

        }

    }
}
