using Data;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;




public class ChromeDriverUpdater
{

    string path = Rutas.LOCAL_APPDATA;
    string localVersionChrome;
    string localVersionComplete;

    public ChromeDriverUpdater() {

        localVersionChrome = Procedimientos.obtenerVersionBaseChrome();
        localVersionComplete = getVersionReleaseSpecific(localVersionChrome);

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

   
    public string getVersionReleaseSpecific(string version)
    {

        string url = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE_" + version;

        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());


        return reader.ReadToEnd().Trim();

    }


    private string getUrlInstalledVersion()
    {
        string url = "https://chromedriver.storage.googleapis.com/";

        return url + localVersionComplete + "/chromedriver_win32.zip";
    }

    
    private bool downloadSameVersionInstalledChromeDriver() {

        WebClient webClient = new WebClient();


        try
        {

            webClient.DownloadFile(getUrlInstalledVersion(), path + "/chromedriver_win32.zip");
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

            ZipFile.ExtractToDirectory(path + "/chromedriver_win32.zip", path);


            if (File.Exists(path + "/chromedriver_win32.zip"))
            {

                File.Delete(path + "/chromedriver_win32.zip");
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



