using Data;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace Updater
{
    public partial class Updater : Form
    {

        WebClient cliente = new WebClient();
        string versionOnline;
        string versionLocal;
        string notasLastRelease;
        Configuracion global = Configuracion.getInstance();

        public Updater()
        {
            InitializeComponent();
            this.versionOnline = Configuracion.versionOnline;
            this.versionLocal = Configuracion.versionLocal;

        }

        private void Updater_Load(object sender, EventArgs e)
        {

            this.versionOnline = Configuracion.versionOnline;
            this.versionLocal = Configuracion.versionLocal;
            this.notasLastRelease = Configuracion.notasLastRelease;

            label_versionNueva.Text = versionOnline;
            richTextBox_notas.Text = notasLastRelease;
            label_versionLocal.Text = versionLocal;


        }

        private void button_descargar_Click(object sender, EventArgs e)
        {
            string url = Procedimientos.obtenerUrlZipLastRelease(versionOnline);
            string destino = Rutas.DIRECTORIO_ACTUAL + $"\\Femecon_{versionOnline}.zip";
            Uri uri = new Uri(url);

            cliente.DownloadProgressChanged += new DownloadProgressChangedEventHandler(descargando);
            cliente.DownloadFileCompleted += new AsyncCompletedEventHandler(finalizado);

            cliente.DownloadFileAsync(uri, destino);


        }



        private void descargando(object sender, DownloadProgressChangedEventArgs e)
        {

            barraProgreso.Value = e.ProgressPercentage;


        }


        private void finalizado(object sender, AsyncCompletedEventArgs e)
        {
            barraProgreso.Value = 50;



            //Procedimiento luego de descargar la nueva version
            descomprimirZipDescargado();
            barraProgreso.Value = 80;

            borrarZipDescargado();
            barraProgreso.Value = 100;

            // mostrarMensajeContinuar();


            //Despues de cerrar se abre el updater_installer
            cerrar();
            abrirUpdaterInstaller();


        }


        private void cerrar()
        {


            this.Close();


        }



        private void descomprimirZipDescargado()
        {

            //MODIFICAR AL FINALIZAR
            string destino = Application.StartupPath + "\\temp";


            ZipArchive archivo = ZipFile.OpenRead(Application.StartupPath + $"\\Femecon_{versionOnline}.zip");
            ZipArchiveExtensions.descomprimirZip(archivo, destino, true);
            archivo.Dispose();

        }

        private void borrarZipDescargado()
        {

            FileLibrary.DeleteFile(Application.StartupPath + $"\\Femecon_{versionOnline}.zip");

        }

        private void button_cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void abrirUpdaterInstaller()
        {

            Process.Start(Application.StartupPath + "\\temp\\Updater_installer.exe");

        }

    }
}
