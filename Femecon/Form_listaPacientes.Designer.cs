namespace Femecon
{
    partial class Form_listaPacientes
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_listaPacientes));
            this.button_autorizar = new System.Windows.Forms.Button();
            this.button_salir = new System.Windows.Forms.Button();
            this.dataGridView_pacientes = new System.Windows.Forms.DataGridView();
            this.paciente = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dni = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numeroAfiliado = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clinica = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.epo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.practicas = new System.Windows.Forms.DataGridViewButtonColumn();
            this.eliminar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.progreso = new Femecon.Clases.DataGridViewProgressColumn();
            this.copiar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.pacientesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.openFileDialog_pacientes = new System.Windows.Forms.OpenFileDialog();
            this.backgroundWorker_ChromeDriver = new System.ComponentModel.BackgroundWorker();
            this.esFemeconDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.activoDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewProgressColumn1 = new Femecon.Clases.DataGridViewProgressColumn();
            this.apellidoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sexoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fechaDeNacimientoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button_guardar = new System.Windows.Forms.Button();
            this.saveFileDialog_pacientes = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_pacientes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacientesBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // button_autorizar
            // 
            this.button_autorizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_autorizar.Location = new System.Drawing.Point(768, 345);
            this.button_autorizar.Name = "button_autorizar";
            this.button_autorizar.Size = new System.Drawing.Size(77, 23);
            this.button_autorizar.TabIndex = 1;
            this.button_autorizar.Text = "Autorizar";
            this.button_autorizar.UseVisualStyleBackColor = true;
            this.button_autorizar.Click += new System.EventHandler(this.button_autorizar_Click);
            // 
            // button_salir
            // 
            this.button_salir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_salir.Location = new System.Drawing.Point(879, 345);
            this.button_salir.Name = "button_salir";
            this.button_salir.Size = new System.Drawing.Size(77, 23);
            this.button_salir.TabIndex = 4;
            this.button_salir.Text = "Salir";
            this.button_salir.UseVisualStyleBackColor = true;
            this.button_salir.Click += new System.EventHandler(this.button_salir_Click);
            // 
            // dataGridView_pacientes
            // 
            this.dataGridView_pacientes.AllowUserToAddRows = false;
            this.dataGridView_pacientes.AllowUserToDeleteRows = false;
            this.dataGridView_pacientes.AutoGenerateColumns = false;
            this.dataGridView_pacientes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_pacientes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.paciente,
            this.dni,
            this.numeroAfiliado,
            this.clinica,
            this.epo,
            this.practicas,
            this.eliminar,
            this.progreso,
            this.copiar});
            this.dataGridView_pacientes.DataSource = this.pacientesBindingSource;
            this.dataGridView_pacientes.Location = new System.Drawing.Point(12, 12);
            this.dataGridView_pacientes.Name = "dataGridView_pacientes";
            this.dataGridView_pacientes.ReadOnly = true;
            this.dataGridView_pacientes.Size = new System.Drawing.Size(945, 322);
            this.dataGridView_pacientes.TabIndex = 5;
            this.dataGridView_pacientes.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView_pacientes_CellContentClick);
            this.dataGridView_pacientes.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView_pacientes_CellFormatting);
            // 
            // paciente
            // 
            this.paciente.HeaderText = "Paciente";
            this.paciente.Name = "paciente";
            this.paciente.ReadOnly = true;
            // 
            // dni
            // 
            this.dni.DataPropertyName = "dni";
            this.dni.HeaderText = "Dni";
            this.dni.Name = "dni";
            this.dni.ReadOnly = true;
            // 
            // numeroAfiliado
            // 
            this.numeroAfiliado.DataPropertyName = "numeroAfiliado";
            this.numeroAfiliado.HeaderText = "Num Afiliado";
            this.numeroAfiliado.Name = "numeroAfiliado";
            this.numeroAfiliado.ReadOnly = true;
            // 
            // clinica
            // 
            this.clinica.DataPropertyName = "clinica";
            this.clinica.HeaderText = "Clínica";
            this.clinica.Name = "clinica";
            this.clinica.ReadOnly = true;
            // 
            // epo
            // 
            this.epo.DataPropertyName = "epo";
            this.epo.HeaderText = "Epo";
            this.epo.Name = "epo";
            this.epo.ReadOnly = true;
            // 
            // practicas
            // 
            this.practicas.HeaderText = "Practicas";
            this.practicas.Name = "practicas";
            this.practicas.ReadOnly = true;
            this.practicas.Text = "Ver";
            // 
            // eliminar
            // 
            this.eliminar.HeaderText = "Eliminar";
            this.eliminar.Name = "eliminar";
            this.eliminar.ReadOnly = true;
            // 
            // progreso
            // 
            this.progreso.HeaderText = "Progreso";
            this.progreso.Name = "progreso";
            this.progreso.ReadOnly = true;
            this.progreso.ToolTipText = "Progreso";
            // 
            // copiar
            // 
            this.copiar.HeaderText = "Copiar";
            this.copiar.Name = "copiar";
            this.copiar.ReadOnly = true;
            this.copiar.Text = "Copiar";
            // 
            // pacientesBindingSource
            // 
            this.pacientesBindingSource.DataSource = typeof(Data.Paciente);
            // 
            // openFileDialog_pacientes
            // 
            this.openFileDialog_pacientes.FileName = "openFileDialog1";
            // 
            // esFemeconDataGridViewCheckBoxColumn
            // 
            this.esFemeconDataGridViewCheckBoxColumn.DataPropertyName = "esFemecon";
            this.esFemeconDataGridViewCheckBoxColumn.HeaderText = "esFemecon";
            this.esFemeconDataGridViewCheckBoxColumn.Name = "esFemeconDataGridViewCheckBoxColumn";
            // 
            // activoDataGridViewCheckBoxColumn
            // 
            this.activoDataGridViewCheckBoxColumn.DataPropertyName = "activo";
            this.activoDataGridViewCheckBoxColumn.HeaderText = "activo";
            this.activoDataGridViewCheckBoxColumn.Name = "activoDataGridViewCheckBoxColumn";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Dni";
            this.dataGridViewTextBoxColumn1.HeaderText = "dni";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Nombre";
            this.dataGridViewTextBoxColumn2.HeaderText = "nombre";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Num Afiliado";
            this.dataGridViewTextBoxColumn3.HeaderText = "numeroAfiliado";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Clínica";
            this.dataGridViewTextBoxColumn4.HeaderText = "clinica";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Partido";
            this.dataGridViewTextBoxColumn5.HeaderText = "epo";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            // 
            // dataGridViewProgressColumn1
            // 
            this.dataGridViewProgressColumn1.HeaderText = "Progreso";
            this.dataGridViewProgressColumn1.Name = "dataGridViewProgressColumn1";
            // 
            // apellidoDataGridViewTextBoxColumn
            // 
            this.apellidoDataGridViewTextBoxColumn.DataPropertyName = "apellido";
            this.apellidoDataGridViewTextBoxColumn.HeaderText = "apellido";
            this.apellidoDataGridViewTextBoxColumn.Name = "apellidoDataGridViewTextBoxColumn";
            // 
            // sexoDataGridViewTextBoxColumn
            // 
            this.sexoDataGridViewTextBoxColumn.DataPropertyName = "sexo";
            this.sexoDataGridViewTextBoxColumn.HeaderText = "sexo";
            this.sexoDataGridViewTextBoxColumn.Name = "sexoDataGridViewTextBoxColumn";
            // 
            // fechaDeNacimientoDataGridViewTextBoxColumn
            // 
            this.fechaDeNacimientoDataGridViewTextBoxColumn.DataPropertyName = "fechaDeNacimiento";
            this.fechaDeNacimientoDataGridViewTextBoxColumn.HeaderText = "fechaDeNacimiento";
            this.fechaDeNacimientoDataGridViewTextBoxColumn.Name = "fechaDeNacimientoDataGridViewTextBoxColumn";
            // 
            // button_guardar
            // 
            this.button_guardar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_guardar.Location = new System.Drawing.Point(685, 345);
            this.button_guardar.Name = "button_guardar";
            this.button_guardar.Size = new System.Drawing.Size(77, 23);
            this.button_guardar.TabIndex = 6;
            this.button_guardar.Text = "Guardar";
            this.button_guardar.UseVisualStyleBackColor = true;
            this.button_guardar.Click += new System.EventHandler(this.button_guardar_Click);
            // 
            // Form_listaPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(968, 380);
            this.ControlBox = false;
            this.Controls.Add(this.button_guardar);
            this.Controls.Add(this.dataGridView_pacientes);
            this.Controls.Add(this.button_salir);
            this.Controls.Add(this.button_autorizar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_listaPacientes";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pacientes";
            this.Load += new System.EventHandler(this.Form_autorizacionesLoop_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_pacientes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pacientesBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_autorizar;
        private System.Windows.Forms.Button button_salir;
        private System.Windows.Forms.DataGridView dataGridView_pacientes;
        private System.Windows.Forms.BindingSource pacientesBindingSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog_pacientes;
        private System.ComponentModel.BackgroundWorker backgroundWorker_ChromeDriver;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn apellidoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn esFemeconDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn activoDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sexoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn fechaDeNacimientoDataGridViewTextBoxColumn;
        private Clases.DataGridViewProgressColumn dataGridViewProgressColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn paciente;
        private System.Windows.Forms.DataGridViewTextBoxColumn dni;
        private System.Windows.Forms.DataGridViewTextBoxColumn numeroAfiliado;
        private System.Windows.Forms.DataGridViewTextBoxColumn clinica;
        private System.Windows.Forms.DataGridViewTextBoxColumn epo;
        private System.Windows.Forms.DataGridViewButtonColumn practicas;
        private System.Windows.Forms.DataGridViewButtonColumn eliminar;
        private Clases.DataGridViewProgressColumn progreso;
        private System.Windows.Forms.DataGridViewButtonColumn copiar;
        private System.Windows.Forms.Button button_guardar;
        private System.Windows.Forms.SaveFileDialog saveFileDialog_pacientes;
    }
}