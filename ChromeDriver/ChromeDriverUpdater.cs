using Data;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;




public class ChromeDriverUpdater
{

    string path = Rutas.LOCAL_APPDATA;



    public void updateToLastVersion()
    {
        try
        {
            killProcessChromeDriver();
            deleteChromeDriver();
            downloadLastChromeDriver();
            //downloadSameVersionInstalledChromeDriver();
            unzipFile();

        }
        catch
        {

            throw new Exception("Ha ocurrido un error al descargar la última versión del driver");
        }



    }

    public string getVersion()
    {

        string url = "https://chromedriver.storage.googleapis.com/LATEST_RELEASE";

        WebRequest request = WebRequest.Create(url);
        WebResponse response = request.GetResponse();

        StreamReader reader = new StreamReader(response.GetResponseStream());


        return reader.ReadToEnd().Trim();

    }

    private string getUrlLastVersion()
    {
        string url = "https://chromedriver.storage.googleapis.com/";

        return url + getVersion() + "/chromedriver_win32.zip";
    }

    private string getUrlSameVersionInstalled()
    {
        string url = "https://chromedriver.storage.googleapis.com/";

        return url + Procedimientos.obtenerVersionActualChrome() + "/chromedriver_win32.zip";
    }

    private bool downloadLastChromeDriver()
    {
        WebClient webClient = new WebClient();


        webClient.DownloadFile(getUrlLastVersion(), path + "/chromedriver_win32.zip");

        return true;
    }

    private bool downloadSameVersionInstalledChromeDriver() {

        WebClient webClient = new WebClient();

        Console.WriteLine(getUrlSameVersionInstalled(), path + "/chromedriver_win32.zip");

        webClient.DownloadFile(getUrlSameVersionInstalled(), path + "/chromedriver_win32.zip");

        return true;

    }

    private bool deleteChromeDriver()
    {

        if (File.Exists(path + "/chromedriver.exe"))
        {

            File.Delete(path + "/chromedriver.exe");
        }

        return true;
    }

    private bool unzipFile()
    {


        ZipFile.ExtractToDirectory(path + "/chromedriver_win32.zip", path);


        if (File.Exists(path + "/chromedriver_win32.zip"))
        {

            File.Delete(path + "/chromedriver_win32.zip");
        }
        return true;
    }

    public bool killProcessChromeDriver()
    {
        foreach (Process process in Process.GetProcessesByName("chromedriver"))
        {
            process.Kill();
            process.WaitForExit();

        }

        return true;
    }
}



