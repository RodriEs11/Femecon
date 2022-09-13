using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Femecon
{
    public partial class Form_CertificacionAfiliatoria : Form
    {

        Paciente paciente = Paciente.getInstance();

        public Form_CertificacionAfiliatoria()
        {
            InitializeComponent();
        }

        private void Form_CertificacionAfiliatoria_Load(object sender, EventArgs e)
        {
            label_paciente_nombre.Text = paciente.nombre;
            label_paciente_apellido.Text = paciente.apellido;
            label_paciente_sexo.Text = paciente.sexo.ToString();
            label_paciente_dni.Text = paciente.dni;
            label_paciente_NumAfiliado.Text = paciente.numeroAfiliado;
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button_siguiente_Click(object sender, EventArgs e)
        {
            ChromeDriver driver = new ChromeDriver();
            Configuracion.mostrarNavegadorChromeDriver = true;


            
            paciente.fechaDeNacimiento = dateTimePicker.Value;

            
            driver.setup();
            driver.abrirYConfigurarCertificacionAfiliatoria(paciente);

            Configuracion.mostrarNavegadorChromeDriver = false;
            

        }
    }
}
