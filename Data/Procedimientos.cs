using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;

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

        public static string obtenerVersionActualChrome() {


            string chromePath = (string)Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe", null, null);
            if (chromePath == null)
            {
                throw new Exception("No se ha encontrado ninguna versión de Chrome");
            }

            var fileVersionInfo = FileVersionInfo.GetVersionInfo(chromePath);
            return fileVersionInfo.FileVersion;

        }

        public static string obtenerVersionBaseChrome() {

            string versionCompleta = obtenerVersionActualChrome();
            string[] temp = versionCompleta.Split('.');
            string versionBase = temp[0];


            return versionBase;

        }


        public static void moverArchivosALocalData() {

            if (File.Exists(Rutas.CODIGOS_TXT))
            {

                try { File.Delete("Codigos.txt"); } catch (Exception e) { }

            }
            else {

                try { File.Move("Codigos.txt", Rutas.CODIGOS_TXT); } catch (Exception e) { }

            }
            if (File.Exists(Rutas.PACIENTE_TXT))
            {

                try { File.Delete("Paciente.txt"); } catch (Exception e) { }

            }
            else
            {

                try { File.Move("Paciente.txt", Rutas.PACIENTE_TXT); } catch (Exception e) { }

            }

            if (File.Exists(Rutas.CHROME_DRIVER_EXE))
            {

                killProcessChromeDriver();
                try { File.Delete("chromedriver.exe"); } catch (Exception e) { }

            }

            else {
                try { File.Move("chromedriver.exe", Rutas.CHROME_DRIVER_EXE); } catch (Exception e) { }

            }


        }

        public static string eliminarEspacios(string texto) {

            return Regex.Replace(texto, @"\s", String.Empty);
        }
        

        public static bool killProcessChromeDriver()
        {
            foreach (Process process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
                process.WaitForExit();

            }

            return true;
        }


        public static void abrirArchivoPDF(string ruta)
        {
            try
            {

                Process p = new Process();
                p.StartInfo = new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    FileName = ruta

                };

                p.Start();

            }
            catch (Exception e) {

                Console.WriteLine(e.Message);

            }


           
        }

    }

}
