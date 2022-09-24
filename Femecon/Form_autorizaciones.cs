using Data;
using System;
using System.IO;
using System.Windows.Forms;

namespace Femecon
{
    public partial class Form_autorizaciones : Form
    {
        public Form_autorizaciones()
        {
            InitializeComponent();
        }

        public string afiliado;
        public string epo;
        public string clinica;

        private void button_Salir_Click(object sender, EventArgs e)
        {
            //CIERRA LA VENTANA
            Close();

        }

        private void button_Copiar_Click(object sender, EventArgs e)
        {

            //Clipboard.SetText(richTextBox_Autorizaciones.Text
            richTextBox_Autorizaciones.SelectAll();
            richTextBox_Autorizaciones.Copy();
            richTextBox_Autorizaciones.DeselectAll();

            MessageBox.Show("Códigos copiados al portapapeles", "Femecon", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void Form_autorizaciones_Load(object sender, EventArgs e)
        {

            // DEFINE EL TIEMPO DE CREACION DEL ARCHIVO CON LAS AUTORIZACIONES
            DateTime horaDeCreacion = File.GetLastWriteTime(Rutas.CODIGOS_TXT);
            label_UltimaModificacionVariable.Text = horaDeCreacion.ToLongTimeString() + " " + horaDeCreacion.ToShortDateString();


            // CARGA TODOS LOS CODIGOS EN EL RichTextBox
            string[] codigos = File.ReadAllLines(Rutas.CODIGOS_TXT);

            for (int i = 0; i < codigos.Length; i++)
            {
                richTextBox_Autorizaciones.AppendText(codigos[i]);
                richTextBox_Autorizaciones.AppendText(Environment.NewLine);
                
            }


            //CARGA LOS DATOS DEL PACIENTE OBTENIDOS EN LA VENTANA 'AUTORIZAR'
            string[] datosPaciente = File.ReadAllLines(Rutas.PACIENTE_TXT);

            if (datosPaciente.Length != 0)
            {
                /* Nombre de afiliado = 0
                 * Clinica = 1
                 * Epo = 2
                 */

                label_NombreAfiliadoVariable.Text = datosPaciente[0];
                label_ClinicaVariable.Text = datosPaciente[1];
                label_EpoVariable.Text = datosPaciente[2];

            }
        }
    }
}
