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
using static System.Windows.Forms.ListViewItem;

namespace Femecon
{
    public partial class Form_detallesPaciente : Form
    {
        public enum AccionCierre { 
            Guardar,
            Cancelar
        }

        Lista listaPracticas;
        Paciente paciente;
        Paciente pacienteCopiaSinCambios = new Paciente();


        public Form_detallesPaciente(Paciente paciente, Lista listaPracticas)
        {
            InitializeComponent();
            this.paciente = paciente;
            this.pacienteCopiaSinCambios.restaurarDesde(paciente);
            this.listaPracticas = listaPracticas;
        }

        private void Form_detallesPaciente_Load(object sender, EventArgs e)
        {

            configurarCheckBoxesEcografia();
            cargarDatosPaciente();

        }

        private void cargarDatosPaciente() {

            textBox_nombre.Text = paciente.nombre;
            textBox_apellido.Text = paciente.apellido;
            textBox_numeroAfiliado.Text = paciente.numeroAfiliado;
            textBox_epo.Text = paciente.epo;
            textBox_matricula.Text = paciente.practicasParaAutorizar[0].matricula;


            textBox_nombre.Enabled = false;
            textBox_apellido.Enabled = false;
            textBox_numeroAfiliado.Enabled = false;
            textBox_epo.Enabled = false;
            textBox_matricula.Enabled = false;
            radioButton_50.Enabled = false;
            radioButton_30.Enabled = false;
            radioButton_40.Enabled = false;

            switch (paciente.clinica) {


                case "30":
                    radioButton_30.Checked = true;
                   
                    break;
                case "40":
                    radioButton_40.Checked = true;
                    break;
               
                case "50":
                    radioButton_50.Checked = true;
                    
                    break;

                default:
                    radioButton_30.Enabled = false;
                    radioButton_40.Enabled = false;
                    radioButton_50.Enabled = false;
                    break;
            }
                            
        }


        private bool validarPracticas()
        {
            bool validado = false;

            if (paciente?.practicasParaAutorizar.Count > 0)
            {

                validado = true;
                errorProvider_PracticasEco.Clear();
            }
            else
            {

                errorProvider_PracticasEco.SetError(checkBox_EcoMamaria, "Debe seleccionar por lo menos una practica");
                errorProvider_PracticasEco.SetError(checkBox_340201, "Debe seleccionar por lo menos una practica");

            }

            return validado;

        }


        private void configurarCheckBoxesEcografia() {


            foreach (CheckBox checkBox in flowLayoutPanel_Ecografia.Controls)
            {
                
                checkBox.CheckedChanged += new EventHandler(clickEnCheckBoxEcografia);
                Practica practica = listaPracticas.traerPracticaPorNombre(checkBox.Text);
                toolTip_Practicas.SetToolTip(checkBox, practica.codigo.ToString());

                for (int i = 0; i < paciente.practicasParaAutorizar.Count; i++)
                {

                    if (paciente.practicasParaAutorizar[i].nombre.Equals(checkBox.Text))
                    {
                        checkBox.Checked = true;
                        break;
                        
                    }

                }

            }

           

        }

        private void clickEnCheckBoxEcografia(object sender, EventArgs e)
        {
            errorProvider_PracticasEco.Clear();

            CheckBox checkBox = (CheckBox)sender;

            Practica practica = listaPracticas.traerPracticaPorNombre(checkBox.Text);


            if (checkBox.Checked)
            {
                if (!paciente.contienePractica(practica)) {

                    paciente.practicasParaAutorizar.Add(practica);
                }
                

                agregarPracticaALista(listView_PracticasSeleccionadas, practica, (int)Grupo.tipoDePractica.ecografia);
                actualizarContadorDePracticas();


            }
            else
            {

                paciente.practicasParaAutorizar.Remove(practica);
                eliminarItemDeLista(listView_PracticasSeleccionadas, practica.nombre);
                actualizarContadorDePracticas();



            }


        }
        public static void eliminarItemDeLista(ListView lista, string item)
        {
            lista.Items.Remove(lista.FindItemWithText(item));
        }

        private ListViewItem agregarPracticaALista(ListView lista, Practica practica, int grupo)
        {

            ListViewItem itemAgregado = lista.Items.Add(practica.nombre);
            itemAgregado.Group = lista.Groups[grupo];
            itemAgregado.ToolTipText = practica.codigo.ToString();

            ListViewSubItem subItemCantidad = new ListViewSubItem();
            subItemCantidad.Text = practica.cantidad.ToString();

            //CORRECCION AL MOSTRAR LA CANTIDAD DE MAMOGRAFIA
            if (practica.tieneCantidad && practica.codigo != 340601 && practica.codigo != 340602 && practica.codigo != 883403)
            {
                itemAgregado.SubItems.Add(subItemCantidad);
            }

            lista.Sort();

            return itemAgregado;

        }

        private void actualizarContadorDePracticas()
        {
           
            label_ContadorPracticas.Text = paciente.practicasParaAutorizar.Count.ToString();

        }

        private void button_cancelar_Click(object sender, EventArgs e)
        {

            paciente.restaurarDesde(pacienteCopiaSinCambios);
            this.Close();
        }

        private void button_guardar_Click(object sender, EventArgs e)
        {
            if (validarPracticas()) {

                this.Close();
            }
          
        }
    }
}
