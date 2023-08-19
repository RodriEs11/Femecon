using Data;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;

public class ChromeDriverUpdater
{

    string path = Rutas.LOCAL_APPDATA;
    string zipPath = Rutas.LOCAL_APPDATA + "/chromedriver_win32.zip";
    string localVersionChrome;

    public ChromeDriverUpdater() {

        localVersionChrome = Procedimientos.obtenerVersionBaseChrome();

    }

    public void updateChromeDriver()
    {
        try
        {
                        
            killProcessChromeDriver();
            deleteChromeDriver();
            downloadSameVersionInstalledChromeDriver();
            unzipFile();
            

        }
        catch (Exception e)
        {

            throw new Exception(e.Message);
        }

    }

    public string getUrlLastKnownGoodVersion() {

               
        string url = "https://googlechromelabs.github.io/chrome-for-testing/latest-versions-per-milestone-with-downloads.json";

        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());

        JObject json = JObject.Parse(reader.ReadToEnd());

        JToken chrodriverEndPoints = json["milestones"][localVersionChrome]["downloads"]["chromedriver"];
        JToken win32Url = chrodriverEndPoints.FirstOrDefault(item => item["platform"].ToString() == "win32")?["url"];

        
        return win32Url.ToString();

    }


    
    private bool downloadSameVersionInstalledChromeDriver() {

        WebClient webClient = new WebClient();


        try
        {

            webClient.DownloadFile(getUrlLastKnownGoodVersion(), zipPath);
        }
        catch {

            throw new Exception("Ha ocurrido un error al intentar descargar el archivo chromedriver_win32.zip");

        }
        

        return true;

    }

    private bool deleteChromeDriver()
    {
        try
        {

            if (File.Exists(path + "/chromedriver.exe"))
            {

                File.Delete(path + "/chromedriver.exe");
            }
        }
        catch {

            throw new Exception("Ha ocurrido un error al eliminar el archivo chromedriver.exe antiguo");

        }
        

        return true;
    }

    private bool unzipFile()
    {

        try
        {

            
            using (ZipArchive archive = ZipFile.OpenRead(zipPath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string entryPath = Path.Combine(path, entry.Name);

                    // Si el archivo ya existe, se reemplaza
                    if (File.Exists(entryPath))
                    {
                        File.Delete(entryPath);
                    }

                    entry.ExtractToFile(entryPath);
                }
            }

            if (File.Exists(zipPath))
            {

                File.Delete(zipPath);
            }

            
        }
        catch {

            throw new Exception("Ha ocurrido un error al intentar descomprimir el archivo chromedriver_win32.zip");

        }
        


        return true;
    }

    public bool killProcessChromeDriver()
    {
        try
        {
            foreach (Process process in Process.GetProcessesByName("chromedriver"))
            {
                process.Kill();
                process.WaitForExit();

            }
        }
        catch {

            throw new Exception("Ha ocurrido un error al cerrar el proceso chromdriver.exe");

        }
        

        return true;
    }
}



