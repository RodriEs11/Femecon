using Data;
using Femecon.Clases;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Femecon
{

    public partial class Form_listaPacientes : Form
    {
        ChromeDriver driver = new ChromeDriver();
        Configuracion configuracion = Configuracion.getInstance();

        List<Paciente> pacientes;
        Lista listaPracticas = new Lista().cargarListadoDePracticasJson();
        bool archivoListadoPacientesGuardado = false;
      

        public Form_listaPacientes()
        {
            InitializeComponent();
           
        }
        public Form_listaPacientes(List<Paciente> pacientes)
        {
            this.pacientes = pacientes;
            InitializeComponent();

        }



        private void Form_autorizacionesLoop_Load(object sender, EventArgs e)
        {

            if (pacientes == null) {

                seleccionarPacientesFromJsonFileDialog();
            }

           

            pacientesBindingSource.DataSource = pacientes; 
            configurarDataGridViewProgressColumnValue();

            button_guardar.Enabled = false;
            dataGridView_pacientes.Columns["copiar"].Visible = false;

        }

      
        private void dataGridView_pacientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            Paciente paciente = null;
            try {

                paciente = ((List<Paciente>)pacientesBindingSource.DataSource)[e.RowIndex];
            }
            catch (Exception exception) { 
                
                Console.WriteLine(exception.Message);
            }
            
           
            if (e.ColumnIndex == dataGridView_pacientes.Columns["practicas"].Index) {



                if (!tienePracticasAutorizadas(paciente))
                {

                    Form_detallesPaciente detallesPaciente = new Form_detallesPaciente(paciente, listaPracticas);
                    detallesPaciente.ShowDialog();
                    dataGridView_pacientes.Refresh();
                }
               

               
            }

            if (e.ColumnIndex == dataGridView_pacientes.Columns["eliminar"].Index)
            {
                pacientesBindingSource.RemoveAt(e.RowIndex);

            }

            if (e.ColumnIndex == dataGridView_pacientes.Columns["copiar"].Index)
            {


                string informacionPaciente = Procedimientos.exportPacienteToTxt(paciente);
                Clipboard.SetText(informacionPaciente);

                string mensaje = $"Se han copiado al portapapeles los datos de\n\n{paciente.nombre} {paciente.apellido}\n\n";
                string practicas = "Practicas\n";

                foreach (Practica practica in paciente.practicasParaAutorizar)
                {
                    practicas += $"{practica.nombre}\n";

                }

                string mensajeFinal = mensaje + practicas;
                MessageBox.Show(mensajeFinal, "Copiar", MessageBoxButtons.OK, MessageBoxIcon.Information);
               

            }


        }

       
        private void dataGridView_pacientes_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            Paciente paciente = null;
            try
            {

                paciente = ((List<Paciente>)pacientesBindingSource.DataSource)[e.RowIndex];
            }
            catch (Exception exception)
            {

                Console.WriteLine(exception.Message);
            }

            if (e.ColumnIndex == dataGridView_pacientes.Columns["paciente"].Index && e.RowIndex >= 0)
            {

                DataGridViewCell cell = dataGridView_pacientes.Rows[e.RowIndex].Cells[e.ColumnIndex];
                cell.Value = $"{paciente.nombre} {paciente.apellido}";

            }


            if (e.ColumnIndex == dataGridView_pacientes.Columns["practicas"].Index && e.RowIndex >= 0)
            {
               
                DataGridViewCell cell = dataGridView_pacientes.Rows[e.RowIndex].Cells[e.ColumnIndex];

        
                if (cell is DataGridViewButtonCell)
                {

                    if (!tienePracticasAutorizadas(paciente))
                    {

                        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)cell;
                        buttonCell.Value = $"Editar prácticas ({paciente.practicasParaAutorizar.Count})";
                    }
                    else {

                        DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)cell;
                        buttonCell.Value = "Ya autorizado";

                    }

                }
            }

            if (e.ColumnIndex == dataGridView_pacientes.Columns["eliminar"].Index && e.RowIndex >= 0)
            {

                DataGridViewCell cell = dataGridView_pacientes.Rows[e.RowIndex].Cells[e.ColumnIndex];
                 
                if (cell is DataGridViewButtonCell)
                {

                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)cell;
                    buttonCell.Value = "Eliminar paciente";

                   

                     
                }
            }


            if (e.ColumnIndex == dataGridView_pacientes.Columns["progreso"].Index && e.RowIndex >= 0)
            {
                if (!tienePracticasAutorizadas(paciente))
                {
                    // Obtiene el valor de progreso de los datos en esa celda
                    int progressValue = Convert.ToInt32(e.Value);

                    // Crea una celda de tipo ProgressBar y establece su valor de progreso
                    DataGridViewProgressCell cell = new DataGridViewProgressCell();
                    cell.Value = progressValue;

                    // Asigna la celda personalizada al evento
                    e.Value = cell;


                }
                else {
                    progressBarPerformStep(e.RowIndex, 100);
                }




            }

            if (e.ColumnIndex == dataGridView_pacientes.Columns["copiar"].Index && e.RowIndex >= 0)
            {
               
                DataGridViewCell cell = dataGridView_pacientes.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell is DataGridViewButtonCell)
                {

                    DataGridViewButtonCell buttonCell = (DataGridViewButtonCell)cell;
                    buttonCell.Value = "Copiar";
                }

            }


        }

        private Task<bool> autorizarAsync(Paciente paciente, int rowIndex)
        {
            var tcs = new TaskCompletionSource<bool>();
            string numeroAfiliado = paciente.numeroAfiliado;

            BackgroundWorker backgroundWorker_ChromeDriver = new BackgroundWorker();
            backgroundWorker_ChromeDriver.DoWork += (sender, e) =>
            {
                try
                {
                    float totalPracticas = paciente.practicasParaAutorizar.Count;
                    int step = (int)(100 / totalPracticas);


                    bool mostrarNavegador = configuracion.getMostrarNavegadorChromeDriver();
                    driver.setup(mostrarNavegador);
                    driver.login(paciente);

                    for (int i = 0; i < totalPracticas; i++)
                    {
                        
                        driver.autorizar(paciente.practicasParaAutorizar, i, numeroAfiliado);

                        /*
                        Random random = new Random();

                        // Generar un número aleatorio entre 1000 y 9999 para los cuatro dígitos centrales
                        int numeroAleatorio = random.Next(1000, 10000);

                        // Generar tres caracteres aleatorios
                        string caracteresAleatorios = RandomString(3, random);

                        // Formatear la cadena final
                        string cadenaAleatoria = $"F-{numeroAleatorio}-{caracteresAleatorios}";


                        paciente.practicasParaAutorizar[i].codigoAutorizacion = cadenaAleatoria;

                        */

                        // ACTIVA EL EVENTO PARA HACER UN STEP EN PROGRESS BAR
                        progressBarPerformStep(rowIndex, step);


                    }


                    // Una vez que el trabajo está completo, establece el resultado en true
                    driver.salir();
                    e.Result = true;
                }
                catch (Exception ex)
                {
                    // Maneja cualquier excepción aquí y establece el resultado en false si es necesario
                    e.Result = false;
                }
            };


            backgroundWorker_ChromeDriver.RunWorkerCompleted += (sender, e) =>
            {
                // Cuando se completa el trabajo en segundo plano, marca la tarea como completada
                tcs.SetResult((bool)e.Result);
                backgroundWorker_ChromeDriver.Dispose(); // Libera los recursos del BackgroundWorker
            };

            // Inicia la tarea en segundo plano
            backgroundWorker_ChromeDriver.RunWorkerAsync(paciente);

            return tcs.Task;
        }

        private async Task autorizarPacientesAsync()
        {

            //PROCESO DE AUTORIZACION EN SEGUNDO PLANO

            for (int rowIndex = 0; rowIndex < dataGridView_pacientes.Rows.Count; rowIndex++)
            {
                this.Cursor = Cursors.AppStarting;

                Paciente paciente = (Paciente)dataGridView_pacientes.Rows[rowIndex].DataBoundItem;

                if (!tienePracticasAutorizadas(paciente)) {

                    bool resultado = await autorizarAsync(paciente, rowIndex);
                    
                }
              
                
                       
            }


            // FINALIZA EL PROCESO
            this.Cursor = Cursors.Default;
            button_guardar.Enabled = true;
            button_salir.Enabled = true;
            dataGridView_pacientes.Columns["copiar"].Visible = true;

            DialogResult result = MessageBox.Show("¿Desea guardar el listado", "Finalizado", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

            if (result == DialogResult.Yes) {

                archivoListadoPacientesGuardado = Procedimientos.guardarListaPacientesEnArchivo(pacientes);
              
            }
            
        }

        private void seleccionarPacientesFromJsonFileDialog()
        {

            openFileDialog_pacientes.Title = "Pacientes";
            openFileDialog_pacientes.DefaultExt = ".json";
            openFileDialog_pacientes.Filter = "Pacientes (*.json)|*.json";
            openFileDialog_pacientes.FileName = "";
            openFileDialog_pacientes.CheckFileExists = true;
            openFileDialog_pacientes.CheckPathExists = true;

            if (openFileDialog_pacientes.ShowDialog() == DialogResult.OK)
            {

                string jsonPath = openFileDialog_pacientes.FileName;
                pacientes = Procedimientos.jsonToPacientesFromJsonPath(jsonPath);


            }
            else
            {
                this.Close();
            }




        }

        private void configurarDataGridViewProgressColumnValue()
        {


            int columnIndex = dataGridView_pacientes.Columns["progreso"].Index;


            for (int i = 0; i < dataGridView_pacientes.Rows.Count; i++)
            {

                //PROGRESS BAR VALUE = 0;
                dataGridView_pacientes.Rows[i].Cells[columnIndex].Value = 0;
            }

        }

        private void progressBarPerformStep(int rowIndex, int step) {


            // Obtén la fila y la columna correspondientes a la celda de ProgressBar que deseas actualizar
            //int rowIndex = 0;
            int columnIndex = dataGridView_pacientes.Columns["progreso"].Index;

            // Obtén el valor actual de la celda (debe ser un valor numérico entre 0 y 100)
            int currentValue = (int)dataGridView_pacientes.Rows[rowIndex].Cells[columnIndex].Value;

            // Aumenta el valor de la celda (puedes hacerlo como desees)
            currentValue += step; 

            // Asegúrate de que el valor no supere el máximo (100)
            if (currentValue > 100)
            {
                currentValue = 100;
            }

            // Actualiza el valor de la celda con el nuevo valor de progreso
            dataGridView_pacientes.Rows[rowIndex].Cells[columnIndex].Value = currentValue;

        }

        private bool tienePracticasAutorizadas(Paciente paciente) {

            int countPracticasAutorizadas = 0;

            foreach (Practica practica in paciente.practicasParaAutorizar)
            {
                

                if (practica.codigoAutorizacion != null) {

                    countPracticasAutorizadas++;

                }

            }

            if (countPracticasAutorizadas == paciente.practicasParaAutorizar.Count)
            {

                return true;
            }
            else {
                return false;
            }

    
        }

        // BOTONES
        private void button_autorizar_Click(object sender, EventArgs e)
        {
            button_autorizar.Enabled = false;

            dataGridView_pacientes.Columns["practicas"].Visible = false;
            dataGridView_pacientes.Columns["eliminar"].Visible = false;
            button_salir.Enabled = false;

            autorizarPacientesAsync();


        }

        private void button_guardar_Click(object sender, EventArgs e)
        {
            archivoListadoPacientesGuardado = Procedimientos.guardarListaPacientesEnArchivo(pacientes);
          
        }

        private void button_salir_Click(object sender, EventArgs e)
        {
            if (archivoListadoPacientesGuardado)
            {
                this.Close();

            }
            else {

                DialogResult result = MessageBox.Show("¿Está seguro que desea salir sin guardar los cambios?",
                  "Aviso",
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {

                    this.Close();
                }

            }




        }
    }

}
