using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

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

        public static DateTime parseToDateTime(string fecha) {


            string[] datos = fecha.Split('-');

            string day = datos[0];
            string month = getPosicionSegunMes(datos[1]);
            string year = datos[2];

            string fechaParse = $"{day}-{month}-{year}";

            return DateTime.ParseExact(fechaParse, "dd-MM-yyyy", CultureInfo.InvariantCulture);

        }

        private static string getPosicionSegunMes(string mes) {

            int posicion;

            switch (mes) {

                case "Ene":
                    posicion = 1;
                    break;

                case "Feb":
                    posicion = 2;
                    break;

                case "Mar":
                    posicion = 3;
                    break;

                case "Abr":
                    posicion = 4;
                    break;

                case "May":
                    posicion = 5;
                    break;

                case "Jun":
                    posicion = 6;
                    break;

                case "Jul":
                    posicion = 7;
                    break;

                case "Ago":
                    posicion = 8;
                    break;

                case "Sep":
                    posicion = 9;
                    break;

                case "Oct":
                    posicion = 10;
                    break;

                case "Nov":
                    posicion = 11;
                    break;

                case "Dic":
                    posicion = 12;
                    break;

                default:
                    posicion = -1;
                    break;


            }

            return posicion.ToString("D2");    

        }

        public static string obtenerCodigoSegunURL(string url) {

            string codigo = "";

            string[] urlParametros = url.Split('?');
            string[] parametro = new string[10] ;
            string[] valorParametro = new string[parametro.Length];

            foreach (string item in urlParametros)
            {
                parametro = item.Split('&');
                

            }


            for (int i = 0; i < parametro.Length; i++)
            {
                string[] valoresParametros = new string[10];

                valoresParametros = parametro[i].Split('=');

                valorParametro[i] = valoresParametros[valoresParametros.Length-1];


            }


            codigo = valorParametro[3];


            if (codigo.Equals("")) {

                throw new Exception("No se ha podido obtener el código de autorizacion desde la url");
            }
           

            return codigo;
        }

        public static Paciente jsonToPacienteFromJsonPath(string jsonPath) {

            string jsonString = leerArchivo(jsonPath);


            return JsonConvert.DeserializeObject<Paciente>(jsonString);
                
        }

        public static List<Paciente> jsonToPacientesFromJsonPath(string jsonPath) {
            
            Lista listaPracticas = new Lista().cargarListadoDePracticasJson();
            
            string jsonString = leerArchivo(jsonPath);
            List<Paciente> pacientes = JsonConvert.DeserializeObject<List<Paciente>>(jsonString);

            foreach (var paciente in pacientes) {

                List<Practica> listaPracticasTemp = new List<Practica>();

                foreach (var practica in paciente.practicasParaAutorizar) {

                    Practica practicaTemp;
                    //DISTINGUE MAMOGRAFIA MAGNIFICADA UNILATERAL O BILATERAL 
                    if (practica.codigo == 883403 && practica.cantidad == 2)
                    {
                        practicaTemp = listaPracticas.traerPracticaPorNombre(practica.nombre).Clone();
                    }
                    else {
                        practicaTemp = listaPracticas.traerPracticaPorCodigoEcografia(practica.codigo).Clone();

                    }

                   
                    practicaTemp.codigoDx = practica.codigoDx;
                    practicaTemp.cantidad = practica.cantidad;
                    practicaTemp.matricula = practica.matricula;
                    practicaTemp.codigoAutorizacion = practica.codigoAutorizacion;


                    listaPracticasTemp.Add(practicaTemp);
                 
                }

                paciente.practicasParaAutorizar.Clear();
                paciente.practicasParaAutorizar = listaPracticasTemp;


            }


            return pacientes;

        }

        public static string getJson(List<Paciente> pacientes) {

           
            return JsonConvert.SerializeObject(pacientes);
        }

        public static string leerArchivo(string path) { 
        
            return File.ReadAllText(path);
            
        }

        public static string exportPacienteToTxt(Paciente paciente) {

            string nombreCompleto = $"{paciente.nombre} {paciente.apellido}";
            string epo = $"{paciente.epo}";
            string numeroAfiliado = $"{paciente.numeroAfiliado}";
            string practicas = "";

            foreach (Practica practica in paciente.practicasParaAutorizar)
            {
                practicas += $"{practica.nombreGuardadoEnArchivos}{practica.codigoAutorizacion}\n";

            }

            return $"{nombreCompleto}" +
                    $"\n{epo}" +
                    $"\n{numeroAfiliado}" +
                    $"\n{practicas}\n";
            }



        public static bool guardarListaPacientesEnArchivo(List<Paciente> pacientes)
        {

            //string hora = DateTime.Now.ToString("ddMMyyyy_HHmmss");
            bool archivoListadoPacientesGuardado = false;


            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Guardar archivo";
            saveFileDialog.FileName = "Pacientes";
            saveFileDialog.Filter = "Archivo de texto (*.txt)|*.txt|Archivo json (*.json)|*.json";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.AddExtension = true;

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                string fileExtension = Path.GetExtension(filePath);

                if (fileExtension == ".txt")
                {

                    string txt = "";
                    foreach (Paciente paciente in pacientes)
                    {
                     

                        txt = $"{txt}{Procedimientos.exportPacienteToTxt(paciente)}";

                    }

                    File.WriteAllText(filePath, txt);
                    archivoListadoPacientesGuardado = true;
                 
                }

                if (fileExtension == ".json")
                {

                    File.WriteAllText(filePath, Procedimientos.getJson(pacientes));
                    archivoListadoPacientesGuardado = true;

                }


            }

            if (archivoListadoPacientesGuardado) {
                MessageBox.Show("Archivo guardado", "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return archivoListadoPacientesGuardado;
        }


    }

}
