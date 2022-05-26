using System.IO;
using System.IO.Compression;
using System.Net;


namespace Data
{

    public static class Procedimientos
    {
        public static string obtenerUrlZipLastRelease(string version)
        {
            string url = Rutas.URL_SOURCE_DOWNLOAD_ZIP + $"{version}/Femecon_{version}.zip";
            return url;

        }



        public static string obtenerRespuestaHTTP(string url)
        {

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();


            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }



        public static bool estaActualizado(string versionLocal, string versionOnline)
        {

            bool versionValidada = false;

            if (versionLocal.Equals(versionOnline))
            {

                versionValidada = true;
            }

            return versionValidada;
        }


        public static void copiarArchivosTemporalesACarpetaSource()
        {


            string destino = Directory.GetCurrentDirectory();

            FileLibrary.CopyDirectory(Rutas.DIRECTORIO_ACTUAL + "\\temp", destino, false);



        }

        public static void generarZipRelease()
        {


            if (!Directory.Exists($"{Directory.GetCurrentDirectory()}\\release\\"))
            {

                Directory.CreateDirectory($"{Directory.GetCurrentDirectory()}\\release");

            }

            var zipFile = $"{Directory.GetCurrentDirectory()}\\release\\Femecon_{Constantes.VERSION_LOCAL}.zip";
            var files = Directory.GetFiles(Directory.GetCurrentDirectory());

            using (var archive = ZipFile.Open(zipFile, ZipArchiveMode.Create))
            {


                foreach (var fPath in files)
                {

                    if (!fPath.EndsWith("Codigos.txt") && !fPath.EndsWith("Paciente.txt") && !fPath.EndsWith("chromedriver.exe"))
                    {

                        archive.CreateEntryFromFile(fPath, Path.GetFileName(fPath));

                    }

                }
            }



        }


    }

}
