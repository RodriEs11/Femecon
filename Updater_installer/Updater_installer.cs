using Data;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Updater_installer
{
    public partial class Updater_installer : Form
    {

        Configuracion configuracion = Configuracion.getInstance();

        public Updater_installer()
        {
            InitializeComponent();
        }

        private void Updater_installer_Load(object sender, EventArgs e)
        {

            label_version.Text = Configuracion.versionLocal;

            string origen = Application.StartupPath;
            string destino = Application.StartupPath + "\\..";


            bool estaEnCarpetaTemp = origen.EndsWith("temp");

            Process proceso;

            if (estaEnCarpetaTemp)
            {
                FileLibrary.CopyDirectory(origen, destino, false);

                progressBar.Value = 100;
                MessageBox.Show("Se ha instalado la nueva versión correctamente, haga click en aceptar para continuar",
                    "Actualización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);


                proceso = Process.Start(Application.StartupPath + "\\..\\Femecon.exe");

            }
            else
            {

                proceso = Process.Start(Application.StartupPath + "\\Femecon.exe");

            }



            this.Close();

        }



    }
}
