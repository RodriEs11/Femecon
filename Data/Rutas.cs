using System;
using System.IO;

namespace Data
{
    public static class Rutas

    {
        public static string URL_SOURCE_DOWNLOAD_ZIP = "https://github.com/RodriEs11/Femecon/releases/download/";


        public static string JSON_KEY_VERSION = "version";
        public static string DIRECTORIO_ACTUAL = Directory.GetCurrentDirectory();
        public static string UPDATER = "Updater.exe";
        public static string FEMECON = "Femecon.exe";
        public static string VERSION_JSON = "version.json";

        public static string LOCAL_APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Femecon";

        //public static string LOCAL_APPDATA = DIRECTORIO_ACTUAL;

        public static string CODIGOS_TXT = LOCAL_APPDATA + "\\Codigos.txt";
        public static string PACIENTE_TXT = LOCAL_APPDATA + "\\Paciente.txt";
        public static string CERTIFICACION_PDF = LOCAL_APPDATA + "\\Certificacion.pdf";

        //public static string CODIGOS_TXT = "\\Codigos.txt";
        //public static string PACIENTE_TXT ="\\Paciente.txt";


        // CHROME DRIVER
       public static string CHROME_DRIVER_EXE = LOCAL_APPDATA + "\\chromedriver.exe";

        //public static string CHROME_DRIVER_EXE = "\\chromedriver.exe";

        public static string URL_IOMA = "http://sistemasl.ioma.gba.gov.ar/sistemas/alta_complejidad/afiliados_nuevo.php?variables=";
        public static string FEMECON_URL = "http://tecnotouch.sistramed.com/";
        public static string FEMECON_URL_AUTORIZADOR = "http://tecnotouch.sistramed.com/Secure/OrderAuthorization.aspx/";





    }
}
