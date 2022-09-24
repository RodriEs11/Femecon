using Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Femecon
{
    public partial class Form_progresoAutorizacion : Form
    {

        Paciente paciente = Paciente.getInstance();
        ChromeDriver driver = new ChromeDriver();
        bool cierreProcesoALaMitad = false;


        public Form_progresoAutorizacion()
        {
            InitializeComponent();



        }

        private void Form_progresoAutorizacion_Load(object sender, EventArgs e)
        {

            setearVentana();
            backgroundWorker_ChromeDriver.DoWork += new DoWorkEventHandler(autorizarEnSegundoPlano);
            backgroundWorker_ChromeDriver.ProgressChanged += new ProgressChangedEventHandler(procesoCambia);
            backgroundWorker_ChromeDriver.RunWorkerCompleted += new RunWorkerCompletedEventHandler(finDelProceso);
            backgroundWorker_ChromeDriver.WorkerReportsProgress = true;

            backgroundWorker_ChromeDriver.WorkerSupportsCancellation = true;

            backgroundWorker_ChromeDriver.RunWorkerAsync();

        }

        private void setearVentana()
        {

            label_PracticaAutorizando.Text = "";
            label_Porcentaje.Text = "0%";
            label_Mensaje.Text = "Autorizando...";

            button_Copiar.Enabled = false;
            button_Cerrar.Enabled = false;

            this.Cursor = Cursors.AppStarting;

        }

        private void setearVentanaFinalizado()
        {



            label_Porcentaje.Text = "100%";
            label_PracticaAutorizando.Text = "";
            label_Mensaje.Text = "Finalizado";

            button_Copiar.Enabled = true;
            button_Cerrar.Enabled = true;

            this.Cursor = Cursors.Default;

        }

        public void autorizarEnSegundoPlano(object sender, DoWorkEventArgs e)
        {

            try
            {
                this.Invoke(new MethodInvoker(() =>
                {

                    label_Mensaje.Text = "Cargando Femecon...";

                }));


                driver.setup(false);
                driver.login(paciente);

                autorizar(paciente);

            }
            catch (InvalidOperationException)
            {

                cierreProcesoALaMitad = true;

            }
            catch (Exception exception)
            {

                // MENSAJE DE ERROR CUANDO LA PAGINA ESTA CAIDA O EN MANTENIMIENTO
                MessageBox.Show(exception.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                cierreProcesoALaMitad = true;
                driver.salir();

                
            }



        }


        // SE EJECUTA CADA VEZ QUE SE TERMINA DE AUTORIZAR UN ESTUDIO
        public void procesoCambia(object sender, ProgressChangedEventArgs e)
        {



            this.Invoke(new MethodInvoker(() =>
            {


                progressBar.PerformStep();

            }));



        }
        public void finDelProceso(object sender, RunWorkerCompletedEventArgs e)
        {



            // EVALUA SI SE CIERRA LA VENTANA EN LA MITAD DEL PROCESO

            if (!cierreProcesoALaMitad)
            {

                driver.salir();

                setearVentanaFinalizado();

                MessageBox.Show("Se han autorizado todas las prácticas", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);


                // PROCEDIMIENTO PARA COPIAR PRACTICAS EN ARCHIVOS

                crearArchivoTxtPaciente(paciente);

                //guardarTodoEnArchivos();
            }
            else
            {

                MessageBox.Show("Se cerró la ventana en mitad del proceso, para ver las autorizaciones realizadas hasta el momento, haga click en \"Ver última autorización\" \n\nSi el problema persiste, reinicie el programa",
                    "Aviso", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);



                this.Invoke(new MethodInvoker(() =>
                {


                    this.Close();

                }));

            }


        }


        public void guardarTodoEnArchivos()
        {
            crearArchivoTxtPaciente(paciente);
            crearArchivoCodigosDeAutorizacion(paciente.practicasParaAutorizar);
        }


        public void crearArchivoTxtPaciente(Paciente paciente)
        {
            string file = Rutas.PACIENTE_TXT;
            StreamWriter writer = new StreamWriter(file);

            try
            {
                writer.WriteLine(paciente.nombre + " " + paciente.apellido);
                writer.WriteLine(paciente.clinica);
                writer.WriteLine(paciente.epo);

            }
            finally
            {

                writer.Dispose();
                writer.Close();
            }

        }

        public void crearArchivoCodigosDeAutorizacion(List<Practica> lista)
        {
            StreamWriter writer = new StreamWriter(Rutas.CODIGOS_TXT);

            try
            {

                foreach (Practica practica in lista)
                {

                    writer.Write(practica.nombreGuardadoEnArchivos);
                    writer.Write(practica.codigoAutorizacion);
                    if (practica.tieneCantidad && practica.codigo != 340601 && practica.codigo != 340602)
                    {
                        writer.Write("  x" + practica.cantidad);
                    }
                    writer.WriteLine();

                }
            }
            finally
            {

                writer.Dispose();
                writer.Close();
            }
        }


        public void crearArchivoCodigosDeAutorizacion(Practica practica)
        {

            StreamWriter writer;

            if (File.Exists(Rutas.CODIGOS_TXT))
            {

                writer = File.AppendText(Rutas.CODIGOS_TXT);
            }
            else
            {

                writer = new StreamWriter(Rutas.CODIGOS_TXT);

            }



            try
            {


                writer.Write(practica.nombreGuardadoEnArchivos);
                writer.Write(practica.codigoAutorizacion);
                if (practica.tieneCantidad && practica.codigo != 340601 && practica.codigo != 340602)
                {
                    writer.Write("  x" + practica.cantidad);
                }
                writer.WriteLine();


            }
            finally
            {

                writer.Dispose();
                writer.Close();
            }



        }

        public void autorizar(Paciente paciente)

        {
            float totalPracticas = paciente.practicasParaAutorizar.Count;

            string numeroAfiliado = paciente.numeroAfiliado;

            this.Invoke(new MethodInvoker(() =>
            {

                // SETEA EL PROGRESS BAR Y EL LABEL 

                label_Mensaje.Text = "Autorizando...";
                progressBar.Value = (int)totalPracticas;
                progressBar.Step = (int)(100 / totalPracticas);

            }));

            for (int i = 0; i < totalPracticas; i++)
            {

                float porcentaje = i / totalPracticas * 100;

                // SETEA LA VENTANA CON LOS DATOS DE LA PRACTICA Y EL PORCENTAJE 
                this.Invoke(new MethodInvoker(() =>
                {

                    label_PracticaAutorizando.Text = paciente.practicasParaAutorizar[i].nombre;
                    label_Porcentaje.Text = ((int)porcentaje).ToString() + "%";


                }));


                driver.autorizar(paciente.practicasParaAutorizar, i, numeroAfiliado);

                // RESETEA EL ARCHIVO DE LOS CODIGOS, SOLO SI SE AUTORIZA EL PRIMER ESTUDIO, 
                if (File.Exists(Rutas.CODIGOS_TXT) && i == 0)
                {

                    File.Delete(Rutas.CODIGOS_TXT);

                }

                // AGREGA TAMBIEN LA PRACTICA AL RICHTEXTBOX Y AL ARCHIVO TXT UNA VEZ QUE ESTA AUTORIZADA
                this.Invoke(new MethodInvoker(() =>
                {


                    string nombre = paciente.practicasParaAutorizar[i].nombreGuardadoEnArchivos;
                    string codigo = paciente.practicasParaAutorizar[i].codigoAutorizacion;

                    crearArchivoCodigosDeAutorizacion(paciente.practicasParaAutorizar[i]);

                    if (paciente.practicasParaAutorizar[i].tieneCantidad && paciente.practicasParaAutorizar[i].codigo != 340601 && paciente.practicasParaAutorizar[i].codigo != 340602)
                    {
                        int cantidad = paciente.practicasParaAutorizar[i].cantidad;
                        richTextBox_practicas.AppendText(nombre + codigo + " x" + cantidad + "\n");
                    }
                    else
                    {

                        richTextBox_practicas.AppendText(nombre + codigo + "\n");
                    }


                }));

                // ACTIVA EL EVENTO PARA HACER UN STEP EN PROGRESS BAR
                backgroundWorker_ChromeDriver.ReportProgress((int)porcentaje);


            }


        }

        private void button_Cerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_Copiar_Click(object sender, EventArgs e)
        {
            //Clipboard.SetText(richTextBox_practicas.Text);
            richTextBox_practicas.SelectAll();
            richTextBox_practicas.Copy();
            richTextBox_practicas.DeselectAll();

            MessageBox.Show("Códigos copiados al portapapeles", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }



        private void Form_progresoAutorizacion_FormClosing(object sender, FormClosingEventArgs e)
        {

            // EVALUA SI TODAVIA SE ESTA EJECUTANDO ALGUN PROCESO EN SEGUNDO PLANO
            if (backgroundWorker_ChromeDriver.IsBusy)
            {

                driver.salir();
                backgroundWorker_ChromeDriver.CancelAsync();


            }
        }
    }



}
