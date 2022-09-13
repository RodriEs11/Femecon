using Data;
using Femecon;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using static System.Windows.Forms.ListViewItem;

namespace Femecon_2_0
{
    public partial class Form_principal : Form
    {
        Lista listaPracticas = new Lista().cargarListadoDePracticasJson();
        Paciente paciente = Paciente.getInstance();
        bool chromeDriverActualizado = false;

        public Form_principal()
        {
            InitializeComponent();
        }

        private void Form_principal_Load(object sender, EventArgs e)
        {

            verificarConexionInternet();
           

            if (Directory.Exists(Application.StartupPath + "\\temp"))
            {
                FileLibrary.DeleteFolder(Application.StartupPath + "\\temp");
            }

            listaPracticas.setCodigoDxEco();
            listaPracticas.setCodigoDxRayos();

            label_version.Text = "Ver. " + Constantes.VERSION_LOCAL;

            configurarCheckBoxesEcografia();
            configurarPracticasRayos();

            label_AfiliadoInactivo.Visible = false;

            validarChromeDriver(false);


        }
        private void Form_principal_FormClosing(object sender, FormClosingEventArgs e)
        {
            Procedimientos.killProcessChromeDriver();
        }
        private void textBox_Afiliado_TextChanged(object sender, EventArgs e)
        {
            errorProvider_AfiliadoTextBox.Clear();
        }

        private void textBox_Matricula_TextChanged(object sender, EventArgs e)
        {
            errorProvider_MatriculaTextBox.Clear();
            textBox_Matricula.Text = Procedimientos.eliminarEspacios(textBox_Matricula.Text);
        }

        private void textBox_Dni_TextChanged(object sender, EventArgs e)
        {
            textBox_Dni.Text = Procedimientos.eliminarEspacios(textBox_Dni.Text);
        }

        private void configurarPracticasRayos()
        {
            // SE CONFIGURAN TODOS LOS CONTROLES DEL PANEL DE RAYOS

            foreach (GroupBox groupBox in flowLayoutPanel_Rx.Controls)
            {

                Practica practica = listaPracticas.traerPracticaPorCodigoRayos(int.Parse(groupBox.Text));


                foreach (FlowLayoutPanel panel in groupBox.Controls)
                {

                    //CHECKBOX PRINCIPAL
                    CheckBox checkBoxPrincipal = (CheckBox)panel.Controls[0];
                    checkBoxPrincipal.CheckedChanged += new EventHandler(clickCheckBoxRayosPrincipal); ;
                    toolTip_Practicas.SetToolTip(checkBoxPrincipal, practica.descripcion);


                    for (int i = 1; i < panel.Controls.Count; i++)
                    {
                        panel.Controls[i].Visible = false;

                        // CHECKBOX SUBSIGUIENTE
                        if (panel.Controls[i].GetType() == typeof(CheckBox))
                        {
                            CheckBox checkBox = (CheckBox)panel.Controls[i];
                            checkBox.CheckedChanged += new EventHandler(clickCheckBoxSubsiguiente);
                        }

                        // CONTADOR DE PRACTICA SUBSIGUIENTE
                        if (panel.Controls[i].GetType() == typeof(NumericUpDown))
                        {
                            NumericUpDown numericUpDown = (NumericUpDown)panel.Controls[i];
                            numericUpDown.ValueChanged += new EventHandler(cambioDeValorNumericUpDown);

                        }

                    }

                }

            }


        }

        private void mostrarBotonesSubsiguientes(FlowLayoutPanel layout, bool mostrar)
        {

            int totalDeBotones = layout.Controls.Count;



            if (totalDeBotones > 1)
            {


                if (mostrar)
                {
                    for (int i = 1; i < totalDeBotones; i++)
                    {
                        layout.Controls[i].Visible = true;



                    }

                }
                else
                {

                    // CUANDO SE DESACTIVA EL CHECKBOX PRINCIPAL, SE RESETEAN LOS CHECKBOXES SUBSIGUIENTES
                    for (int i = 1; i < totalDeBotones; i++)
                    {
                        layout.Controls[i].Visible = false;

                        if (layout.Controls[i].GetType() == typeof(CheckBox))
                        {
                            CheckBox checkBox = (CheckBox)layout.Controls[i];
                            checkBox.Checked = false;

                        }

                        if (layout.Controls[i].GetType() == typeof(NumericUpDown))
                        {
                            NumericUpDown numericUpDown = (NumericUpDown)layout.Controls[i];
                            numericUpDown.Value = 1;

                        }
                    }


                }

            }

        }

        private ListViewItem agregarPracticaALista(ListView lista, Practica practica, int grupo)
        {

            ListViewItem itemAgregado = lista.Items.Add(practica.nombre);
            itemAgregado.Group = lista.Groups[grupo];
            itemAgregado.ToolTipText = practica.codigo.ToString();

            ListViewSubItem subItemCantidad = new ListViewSubItem();
            subItemCantidad.Text = practica.cantidad.ToString();

            //CORRECCION AL MOSTRAR LA CANTIDAD DE MAMOGRAFIA
            if (practica.tieneCantidad && practica.codigo != 340601 && practica.codigo != 340602)
            {
                itemAgregado.SubItems.Add(subItemCantidad);
            }

            lista.Sort();

            return itemAgregado;

        }
        public static void eliminarItemDeLista(ListView lista, string item)
        {
            lista.Items.Remove(lista.FindItemWithText(item));
        }

        private void clickCheckBoxSubsiguiente(object sender, EventArgs e)
        {

            CheckBox checkBoxSubsiguiente = (CheckBox)sender;
            FlowLayoutPanel layout = (FlowLayoutPanel)checkBoxSubsiguiente.Parent;
            NumericUpDown contador = null;




            for (int i = 0; i < layout.Controls.Count; i++)
            {
                if (layout.Controls[i].GetType() == typeof(NumericUpDown))
                {
                    contador = (NumericUpDown)layout.Controls[i];

                }

            }


            int codigoPracticaSubsiguiente = int.Parse(checkBoxSubsiguiente.Parent.Parent.Text) + 1;


            Practica practica = listaPracticas.traerPracticaPorCodigoRayos(codigoPracticaSubsiguiente);
            bool existePractica = paciente.practicasParaAutorizar.Contains(practica);


            if (checkBoxSubsiguiente.Checked)
            {
                paciente.practicasParaAutorizar.Add(practica);
                if (contador != null)
                {
                    paciente.practicasParaAutorizar.SingleOrDefault(p => p.codigo == codigoPracticaSubsiguiente).cantidad = (int)contador.Value;
                }


                agregarPracticaALista(listView_PracticasSeleccionadas, practica, (int)Grupo.tipoDePractica.rx);


            }
            else
            {

                if (existePractica)
                {
                    paciente.practicasParaAutorizar.SingleOrDefault(p => p.codigo == codigoPracticaSubsiguiente).cantidad = 1;
                }

                if (contador != null)
                {
                    contador.Value = 1;
                }

                paciente.practicasParaAutorizar.Remove(practica);
                eliminarItemDeLista(listView_PracticasSeleccionadas, practica.nombre);
            }




            actualizarContadorDePracticas();

        }

        private void cambioDeValorNumericUpDown(object sender, EventArgs e)
        {
            // SI EL CHECKBOX NO ESTA ACTIVADO, SE ACTIVA Y SE AGREGA LA CANTIDAD DE PRACTICAS SELECCIONADAS

            NumericUpDown contador = (NumericUpDown)sender;
            FlowLayoutPanel layout = (FlowLayoutPanel)contador.Parent;
            CheckBox checkBoxSubsiguiente = null;

            for (int i = 0; i < layout.Controls.Count; i++)
            {
                if (layout.Controls[i].GetType() == typeof(CheckBox))
                {
                    checkBoxSubsiguiente = (CheckBox)layout.Controls[i];

                }

            }

            int codigoPractica = int.Parse(contador.Parent.Parent.Text);



            if (codigoPractica == 340213)
            {

                Practica rx_340213 = null;
                rx_340213 = paciente.practicasParaAutorizar.SingleOrDefault(p => p.codigo == codigoPractica);
                contador = (NumericUpDown)layout.Controls[1];

                if (rx_340213 != null)
                {
                    rx_340213.cantidad = (int)contador.Value;
                }

                actualizarLista(listView_PracticasSeleccionadas, listaPracticas.traerPracticaPorCodigoRayos(codigoPractica));
                actualizarContadorDePracticas();

                return;
            }


            int codigoPracticaSubsiguiente = int.Parse(contador.Parent.Parent.Text) + 1;

            Practica practica = listaPracticas.traerPracticaPorCodigoRayos(codigoPracticaSubsiguiente);

            bool existePractica = paciente.practicasParaAutorizar.Contains(practica);


            if (existePractica)
            {

                paciente.practicasParaAutorizar.SingleOrDefault(p => p.codigo == codigoPracticaSubsiguiente).cantidad = (int)contador.Value;
                actualizarLista(listView_PracticasSeleccionadas, practica);

            }
            else
            {

                checkBoxSubsiguiente.Checked = true;


            }

            actualizarContadorDePracticas();

        }

        private void actualizarCheckBoxesYNumericUpDown(FlowLayoutPanel layout)
        {



            int totalDeBotones = layout.Controls.Count;

            for (int i = 1; i < totalDeBotones; i++)
            {

                if (layout.Controls[i].GetType() == typeof(CheckBox))
                {
                    CheckBox checkBox = (CheckBox)layout.Controls[i];
                    checkBox.Checked = false;

                }

                if (layout.Controls[i].GetType() == typeof(NumericUpDown))
                {
                    NumericUpDown numericUpDown = (NumericUpDown)layout.Controls[i];
                    numericUpDown.Value = 1;

                }
            }

            //CheckBox checkBoxSubsiguiente = (CheckBox)layout.Controls[1];
            //NumericUpDown numericUpDown = (NumericUpDown)layout.Controls[2];
            //checkBoxSubsiguiente.Checked = false;
            //numericUpDown.Value = 1;

        }

        private void clickCheckBoxRayosPrincipal(object sender, EventArgs e)
        {
            errorProvider_PracticasEco.Clear();

            CheckBox checkBox = (CheckBox)sender;
            FlowLayoutPanel layout = (FlowLayoutPanel)checkBox.Parent;


            int codigoPractica = int.Parse(checkBox.Parent.Parent.Text);
            Practica practica = listaPracticas.traerPracticaPorCodigoRayos(codigoPractica);




            // ACTIVA O DESACTIVA LOS BOTONES SUBSIGUIENTES
            if (checkBox.Checked)
            {

                paciente.practicasParaAutorizar.Add(practica);

                agregarPracticaALista(listView_PracticasSeleccionadas, practica, (int)Grupo.tipoDePractica.rx);
                mostrarBotonesSubsiguientes(layout, true);
                actualizarContadorDePracticas();



            }
            else
            {

                if (practica.codigo == 340213)
                {

                    NumericUpDown contador = null;
                    contador = (NumericUpDown)layout.Controls[1];

                    if (contador != null)
                    {
                        contador.Value = 1;
                        practica.cantidad = (int)contador.Value;
                        eliminarItemDeLista(listView_PracticasSeleccionadas, practica.nombre);
                        mostrarBotonesSubsiguientes(layout, false);
                        actualizarCheckBoxesYNumericUpDown(layout);

                    }
                    paciente.practicasParaAutorizar.Remove(practica);
                    actualizarContadorDePracticas();
                    return;
                }

                paciente.practicasParaAutorizar.Remove(practica);

                eliminarItemDeLista(listView_PracticasSeleccionadas, practica.nombre);
                mostrarBotonesSubsiguientes(layout, false);
                actualizarCheckBoxesYNumericUpDown(layout);
                actualizarContadorDePracticas();



            }

        }

        private void configurarCheckBoxesEcografia()
        {
            foreach (CheckBox checkBox in flowLayoutPanel_Ecografia.Controls)
            {
                checkBox.CheckedChanged += new EventHandler(clickEnCheckBoxEcografia);
                Practica practica = listaPracticas.traerPracticaPorNombre(checkBox.Text);
                toolTip_Practicas.SetToolTip(checkBox, practica.codigo.ToString());

            }

        }

        private void clickEnCheckBoxEcografia(object sender, EventArgs e)
        {
            errorProvider_PracticasEco.Clear();

            CheckBox checkBox = (CheckBox)sender;

            Practica practica = listaPracticas.traerPracticaPorNombre(checkBox.Text);


            if (checkBox.Checked)
            {

                paciente.practicasParaAutorizar.Add(practica);

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
        private void button_Buscar_Click(object sender, EventArgs e)
        {


            label_AfiliadoInactivo.Visible = false;


            bool dniValidado = validarDni();

            if (dniValidado)
            {

                string dni = textBox_Dni.Text;

                try
                {
                    Paciente paciente = ConexionIoma.obtenerDatosPaciente(dni, 'F');

                }

                catch (Exception)
                {
                    paciente = null;
                }


                if (paciente == null)
                {

                    try
                    {
                        paciente = ConexionIoma.obtenerDatosPaciente(dni, 'M');

                    }

                    catch (Exception exception)
                    {

                        MessageBox.Show(exception.Message,
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        paciente = Paciente.getInstance();
                    }

                }


                // EL PACIENTE SI NO ES NULL SE CARGA EN EL FORM

                if (paciente != null)
                {

                    if (existePaciente())
                    {

                        notificacionPacienteInactivo();
                        notificacionEsFemecon();
                        cargarDatosPacienteEnForm();
                        

                        if (!paciente.activo)
                        {
                            resetRadioButtons();
                            activarDesactivarRadioButtons(false);
                            label_AfiliadoInactivo.Visible = true;
                        }

                    }

                }

                else
                {

                    resetPaciente();
                    resetRadioButtons();
                    label_NombreAfiliado.Text = "NOMBRE";
                    label_numAfiliado.Text = "AFILIADO";
                    label_Epo.Text = "EPO";

                }


            }

        }
        private void printButton_Click(object sender, EventArgs e)
        {
            if (paciente.dni != null)
            {

                try
                {

                    ChromeDriver driver = new ChromeDriver();
                    driver.setup();
                    driver.descargarValidacionPDF(paciente);
                    Procedimientos.abrirArchivoPDF(Rutas.CERTIFICACION_PDF);

                }
                catch (Exception exception)
                {

                    MessageBox.Show(exception.Message);

                }

            }
            else {

                MessageBox.Show("Primero debe buscar un paciente por DNI antes de imprimir la certificación", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
            



        }
        private void button_Salir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_VerAutorizaciones_Click(object sender, EventArgs e)
        {

            if (File.Exists(Rutas.CODIGOS_TXT) && File.Exists(Rutas.PACIENTE_TXT))
            {

                //GUARDA LOS DATOS DE LOS ARCHIVOS TXT PARA VERIFICAR QUE NO ESTEN VACIOS
                string codigosTxt = File.ReadAllText(Rutas.CODIGOS_TXT);
                string pacienteTxt = File.ReadAllText(Rutas.PACIENTE_TXT);

                //VERIFICA QUE LOS ARCHIVOS NO ESTEN VACIOS
                if (codigosTxt.Length > 0 && pacienteTxt.Length > 0)
                {

                    Form_autorizaciones ventanaAutorizaciones = new Form_autorizaciones();
                    ventanaAutorizaciones.ShowDialog();

                }
                else
                {
                    MessageBox.Show("Los archivos con los datos de las autorizaciones están vacíos, debe autorizar un estudio antes de poder visualizarlo", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }

            }
            else
            {
                MessageBox.Show("No existe ninguna autorización previa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }

        private string obtenerClinicaSegunRadioButton()
        {

            string clinica = "";


            if (radioButton_30.Checked)
            {
                clinica = "30";

            }

            if (radioButton_40.Checked)
            {
                clinica = "40";

            }

            if (radioButton_50.Checked)
            {
                clinica = "50";

            }
            return clinica;
        }

        private void button_Autorizar_Click(object sender, EventArgs e)
        {


            bool afiliadoValidado = validarAfiliado();
            bool practicasValidado = validarPracticas();
            bool matriculaValidado = validarMatricula();
            bool radioButtonsValidado = validarRadioButtons();
            //bool partidoValidado = validarPartido();
            //bool pacienteActivoValidado = validarPacienteActivo();


            bool variablesValidadas = afiliadoValidado && practicasValidado && matriculaValidado && radioButtonsValidado;


            if (variablesValidadas)
            {


                if (paciente.clinica == null || paciente.clinica.Equals(""))
                {

                    paciente.clinica = obtenerClinicaSegunRadioButton();

                }

                if (paciente.numeroAfiliado == null || paciente.numeroAfiliado.Equals(""))
                {

                    paciente.numeroAfiliado = textBox_Afiliado.Text;

                }


                string listadoPracticas = obtenerListadoDePracticasPorAutorizar();


                string mensaje = $"Está por autorizar las siguientes prácticas\n\n{listadoPracticas}\n¿Desea continuar?";


                DialogResult opcion = MessageBox.Show(mensaje, "Aviso", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // Procedimientos CHROME DRIVER
                if (opcion == DialogResult.Yes)
                {

                    paciente.practicasParaAutorizar.Sort();
                    foreach (Practica practica in paciente.practicasParaAutorizar)
                    {
                        // A TODAS LAS PRACTICAS SE LE PONE EL MISMO CODIGO Y MATRICULA 
                        // TODO MODIFICAR LUEGO
                        if (!practica.matriculaModificada)
                        {
                            practica.matricula = textBox_Matricula.Text;
                        }

                        //practica.codigoDx = "V700";

                    }

                    Form_progresoAutorizacion autorizar = new Form_progresoAutorizacion();
                    autorizar.ShowDialog();

                }

            }

        }

        string obtenerListadoDePracticasPorAutorizar()
        {

            string practicas = "";

            foreach (Practica practica in paciente.practicasParaAutorizar)
            {
                if (practica.tieneCantidad && practica.codigo != 340601 && practica.codigo != 340602)
                {

                    practicas += practica.nombre + " x" + practica.cantidad + "\n";

                }
                else
                {

                    practicas += practica.nombre + "\n";
                }

            }

            return practicas;

        }

        private void button_Restablecer_Click(object sender, EventArgs e)
        {
            textBox_Dni.Text = "";
            textBox_Afiliado.Text = "";
            textBox_Matricula.Text = "";

            resetRadioButtons();
            resetPaciente();
            resetRayos();

            errorProvider_AfiliadoTextBox.Clear();
            errorProvider_MatriculaTextBox.Clear();
            errorProvider_PracticasEco.Clear();
            errorProvider_RadioButtons.Clear();

            label_AfiliadoInactivo.Visible = false;

            printButton.Enabled = false;
            printValidacionDelAfiliadoToolStripMenuItem.Enabled = false;

            certificacionAfiliatoriaToolStripMenuItem.Enabled = false;

            label_NombreAfiliado.Text = "NOMBRE";
            label_numAfiliado.Text = "AFILIADO";
            label_Epo.Text = "EPO";

            foreach (CheckBox item in flowLayoutPanel_Ecografia.Controls)
            {
                item.Checked = false;
            }

        }

        private void resetRayos()
        {

            foreach (GroupBox groupBox in flowLayoutPanel_Rx.Controls)
            {

                foreach (FlowLayoutPanel panel in groupBox.Controls)
                {

                    ((CheckBox)panel.Controls[0]).Checked = false;

                }

            }

        }

        private bool validarDni()
        {

            bool validado = false;

            if (textBox_Dni.TextLength > 0 && textBox_Dni.Text.All(char.IsDigit) && textBox_Dni.Text.Length > 5)
            {
                validado = true;

            }
            else
            {
                MessageBox.Show("No ha ingresado un dni válido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return validado;

        }
        private bool validarAfiliado()
        {


            bool validado = false;


            // SOLO PUEDE INICIAR CON UNA LETRA, EL RESTO DE CARACTERES SON UNICAMENTE NUMEROS
            if (textBox_Afiliado.Text.Length < 12 && textBox_Afiliado.Text.Length > 0)
            {
                errorProvider_AfiliadoTextBox.SetError(textBox_Afiliado, "Debe contener 12 caracteres");

                if (!textBox_Afiliado.Text.Substring(1).All(char.IsDigit))
                {
                    errorProvider_AfiliadoTextBox.SetError(textBox_Afiliado, "Debe ingresar un valor válido");

                }
            }

            if (textBox_Afiliado.Text.Length == 12 && textBox_Afiliado.Text.Substring(1).All(char.IsDigit))
            {
                errorProvider_AfiliadoTextBox.Clear();
                validado = true;

            }
            else
            {

                errorProvider_AfiliadoTextBox.SetError(textBox_Afiliado, "Debe ingresar un valor válido");

            }


            return validado;
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
        private bool validarMatricula()
        {

            bool validado = false;


            if (textBox_Matricula.Text.Equals("") || !textBox_Matricula.Text.All(char.IsDigit))
            {
                errorProvider_MatriculaTextBox.SetError(textBox_Matricula, "Debe ingresar una matrícula válida");

            }
            else
            {
                errorProvider_MatriculaTextBox.Clear();
                validado = true;

            }

            return validado;

        }
        private bool validarRadioButtons()
        {

            bool validado = radioButton_30.Checked || radioButton_40.Checked || radioButton_50.Checked;

            if (!validado)
            {
                errorProvider_RadioButtons.SetError(radioButton_50, "Debe seleccionar una clínica");
            }
            else
            {
                errorProvider_RadioButtons.Clear();
            }

            return validado;
        }
        private bool validarPartido()
        {
            bool validado = false;

            if (paciente != null && paciente.esFemecon)
            {
                validado = true;
            }

            return validado;
        }
        private bool validarPacienteActivo()
        {
            bool validado = false;

            if (paciente != null && paciente.activo)
            {
                validado = true;
            }

            return validado;
        }

        private void resetPaciente()
        {
            if (paciente != null)
            {

                paciente.nombre = null;
                paciente.apellido = null;
                paciente.numeroAfiliado = null;
                paciente.clinica = null;
                paciente.epo = null;
                paciente.activo = false;
                paciente.practicasParaAutorizar.Clear();
                paciente.sexo = ' ';
                paciente.dni = null;
                paciente.fechaDeNacimiento = DateTime.Now;
            }



        }
        private void resetRadioButtons()
        {

            activarDesactivarRadioButtons(true);
            radioButton_30.Checked = false;
            radioButton_40.Checked = false;
            radioButton_50.Checked = false;

        }
        private void cargarDatosPacienteEnForm()
        {

            if (paciente != null)
            {

                textBox_Afiliado.Text = paciente.numeroAfiliado;
                label_numAfiliado.Text = paciente.numeroAfiliado;
                label_NombreAfiliado.Text = paciente.nombre + " " + paciente.apellido;
                label_Epo.Text = paciente.epo;
                setearRadioButtons();

                printButton.Enabled = true;
                printValidacionDelAfiliadoToolStripMenuItem.Enabled = true;
                certificacionAfiliatoriaToolStripMenuItem.Enabled = true;

                if (!paciente.esFemecon)
                {
                    activarDesactivarRadioButtons(false);
                }
            }

        }
        private void activarDesactivarRadioButtons(bool opcion)
        {

            radioButton_30.Enabled = opcion;
            radioButton_40.Enabled = opcion;
            radioButton_50.Enabled = opcion;


        }

        private void setearRadioButtons()
        {

            activarDesactivarRadioButtons(false);

            switch (paciente.clinica)
            {
                case "30":
                    radioButton_30.Checked = true;
                    break;

                case "40":
                    radioButton_40.Checked = true;
                    break;

                case "50":
                    radioButton_50.Checked = true;
                    break;

            }
        }

        private bool existePaciente()
        {

            bool existe = false;


            if (paciente.nombre != null)
            {


                if (!paciente.nombre.Equals(""))
                {
                    existe = true;

                }

            }

            return existe;

        }

        private void notificacionEsFemecon()
        {



            if (!paciente.esFemecon)
            {
                MessageBox.Show("El afiliado no corresponde a Femecon",
                        "Aviso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                resetRadioButtons();

            }

            if (paciente.epo.Equals("002") || paciente.epo.Equals("441")) {
               
                MessageBox.Show("El afiliado debe autorizar los estudios en una delegación de IOMA",
                            "Aviso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);


            }

        }

        private void notificacionPacienteInactivo()
        {


            if (!paciente.activo)
            {
                MessageBox.Show("El afiliado no se encuentra activo en el padrón de afiliados",
                                        "Aviso",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Warning);


            }


            resetRadioButtons();


        }

        private void actualizarLista(ListView lista, Practica practica)
        {

            for (int i = 0; i < lista.Items.Count; i++)
            {
                if (lista.Items[i].Text.Equals(practica.nombre))
                {


                    lista.Items[i].SubItems[0].Text = practica.nombre;
                    lista.Items[i].SubItems[1].Text = practica.cantidad.ToString();


                }

            }

        }

        private void actualizarContadorDePracticas()
        {
            int totalPracticas = 0;


            for (int i = 0; i < paciente.practicasParaAutorizar.Count; i++)
            {
                totalPracticas += paciente.practicasParaAutorizar[i].cantidad;

                //CORRECION PARA LAS MAMOGRAFIAS, QUE CUENTAN COMO 1 PRACTICA
                if (paciente.practicasParaAutorizar[i].codigo == 340601 || paciente.practicasParaAutorizar[i].codigo == 340602)
                {
                    totalPracticas -= 1;

                }
            }


            label_ContadorPracticas.Text = totalPracticas.ToString();

        }


        // MENU STRIP

        private void autorizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_Autorizar.PerformClick();
        }

        private void verAutorizacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_VerAutorizaciones.PerformClick();
        }

        private void restablecerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_Restablecer.PerformClick();
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            button_Salir.PerformClick();
        }

        private void comoAutorizarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string mensaje = "1 - Busque el paciente por dni" +
                            "\n2 - Seleccione las prácticas que desee autorizar" +
                            "\n3 - Haga click en \"Autorizar\"" +
                            "\n4 - Se abrirá una nueva ventana, espere a que termine de autorizarse" +
                            "\n5 - Le saldrán los códigos de autorización una vez finalizado";
            MessageBox.Show(mensaje, "Ayuda", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void acercaDeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Desarrollado por Rodrigo Espíndola\nVersion " + Constantes.VERSION_LOCAL, "Femecon", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void verificarActualizaciónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            validarChromeDriver(true);
        }

        private void printValidacionDelAfiliadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printButton.PerformClick();
        }

        private void certificacionAfiliatoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
         
            Form_CertificacionAfiliatoria certificacionAfiliatoria = new Form_CertificacionAfiliatoria();
            certificacionAfiliatoria.ShowDialog();

        }

        // DEV TOOLS

        private void agregarPracticasAFormDesdeJson(object sender, EventArgs e)
        {

            foreach (Practica practica in listaPracticas.ecografia)
            {
                Console.WriteLine(practica.ToString());
                string nombreEstudio = practica.nombre;
                string nombreCheckBox = "checkBox_" + nombreEstudio;
                CheckBox checkBox = new CheckBox();


                checkBox.AccessibleDescription = "";
                checkBox.AccessibleName = "";
                checkBox.AutoSize = true;
                checkBox.Location = new System.Drawing.Point(3, 3);
                checkBox.Name = nombreCheckBox;
                checkBox.Size = new System.Drawing.Size(115, 17);
                checkBox.TabIndex = practica.orden * 10;
                checkBox.Tag = "0";
                checkBox.Text = nombreEstudio;
                checkBox.UseVisualStyleBackColor = true;
                // checkBox.CheckedChanged += new EventHandler(funcion);
                flowLayoutPanel_Ecografia.Controls.Add(checkBox);

            }



        }

        private void verificarConexionInternet() {

            string url = "https://www.google.com/";

            WebRequest request = WebRequest.Create(url);
            WebResponse response;
            DialogResult resultadoDialog = DialogResult.Cancel;

            try
            {

                response = request.GetResponse();

            }
            catch (Exception e)
            {
                resultadoDialog = MessageBox.Show("No hay conexión a internet, intente nuevamente", "Femecon", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                
                if (resultadoDialog == DialogResult.Retry)
                {

                    verificarConexionInternet();

                }
                if (resultadoDialog == DialogResult.Cancel)
                {

                    this.Close();

                }

            }
            

        }
        public void validarChromeDriver(bool mostrarMensaje) {

            ChromeDriver chromeDriver = new ChromeDriver();

            try
            {

                chromeDriver.validarVersion();
                chromeDriver.salir();
                chromeDriverActualizado = true;

            }
            catch (Exception e)
            {

                if (mostrarMensaje)
                {

                    MessageBox.Show(e.Message + "\nHaga click en aceptar para continuar", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ChromeDriverUpdater update = new ChromeDriverUpdater();
                    update.updateChromeDriver();
                }

                if (!chromeDriverActualizado) {

                    ChromeDriverUpdater update = new ChromeDriverUpdater();
                    update.updateChromeDriver();
                }

            }
            finally {

                if (!chromeDriverActualizado && mostrarMensaje)
                {

                    MessageBox.Show("El programa no se ha actualizado correctamente, intente abrir el programa nuevamente", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                if (chromeDriverActualizado && mostrarMensaje)
                {

                    MessageBox.Show("El programa se encuentra actualizado", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }


            }
    

        }

        
    }
}
