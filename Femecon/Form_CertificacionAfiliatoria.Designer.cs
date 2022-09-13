namespace Femecon
{
    partial class Form_CertificacionAfiliatoria
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_CertificacionAfiliatoria));
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.label_paciente_NumAfiliado = new System.Windows.Forms.Label();
            this.label_paciente_dni = new System.Windows.Forms.Label();
            this.label_paciente_sexo = new System.Windows.Forms.Label();
            this.label_paciente_apellido = new System.Windows.Forms.Label();
            this.label_paciente_nombre = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.button_siguiente = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker.Location = new System.Drawing.Point(14, 19);
            this.dateTimePicker.MaxDate = new System.DateTime(2050, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new System.DateTime(1900, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(88, 20);
            this.dateTimePicker.TabIndex = 11;
            // 
            // label_paciente_NumAfiliado
            // 
            this.label_paciente_NumAfiliado.AutoSize = true;
            this.label_paciente_NumAfiliado.Location = new System.Drawing.Point(11, 25);
            this.label_paciente_NumAfiliado.Name = "label_paciente_NumAfiliado";
            this.label_paciente_NumAfiliado.Size = new System.Drawing.Size(83, 13);
            this.label_paciente_NumAfiliado.TabIndex = 10;
            this.label_paciente_NumAfiliado.Text = "NUM AFILIADO";
            // 
            // label_paciente_dni
            // 
            this.label_paciente_dni.AutoSize = true;
            this.label_paciente_dni.Location = new System.Drawing.Point(11, 25);
            this.label_paciente_dni.Name = "label_paciente_dni";
            this.label_paciente_dni.Size = new System.Drawing.Size(26, 13);
            this.label_paciente_dni.TabIndex = 9;
            this.label_paciente_dni.Text = "DNI";
            // 
            // label_paciente_sexo
            // 
            this.label_paciente_sexo.AutoSize = true;
            this.label_paciente_sexo.Location = new System.Drawing.Point(11, 25);
            this.label_paciente_sexo.Name = "label_paciente_sexo";
            this.label_paciente_sexo.Size = new System.Drawing.Size(36, 13);
            this.label_paciente_sexo.TabIndex = 8;
            this.label_paciente_sexo.Text = "SEXO";
            // 
            // label_paciente_apellido
            // 
            this.label_paciente_apellido.AutoSize = true;
            this.label_paciente_apellido.Location = new System.Drawing.Point(11, 25);
            this.label_paciente_apellido.Name = "label_paciente_apellido";
            this.label_paciente_apellido.Size = new System.Drawing.Size(59, 13);
            this.label_paciente_apellido.TabIndex = 7;
            this.label_paciente_apellido.Text = "APELLIDO";
            // 
            // label_paciente_nombre
            // 
            this.label_paciente_nombre.AutoSize = true;
            this.label_paciente_nombre.Location = new System.Drawing.Point(11, 25);
            this.label_paciente_nombre.Name = "label_paciente_nombre";
            this.label_paciente_nombre.Size = new System.Drawing.Size(54, 13);
            this.label_paciente_nombre.TabIndex = 6;
            this.label_paciente_nombre.Text = "NOMBRE";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label_paciente_nombre);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 50);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nombre";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_paciente_apellido);
            this.groupBox2.Location = new System.Drawing.Point(3, 59);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 50);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Apellido";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_paciente_sexo);
            this.groupBox3.Location = new System.Drawing.Point(3, 115);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(324, 50);
            this.groupBox3.TabIndex = 24;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Sexo";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label_paciente_dni);
            this.groupBox4.Location = new System.Drawing.Point(3, 171);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(324, 50);
            this.groupBox4.TabIndex = 25;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Dni";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label_paciente_NumAfiliado);
            this.groupBox5.Location = new System.Drawing.Point(3, 227);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(324, 50);
            this.groupBox5.TabIndex = 26;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Numero de afiliado";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.dateTimePicker);
            this.groupBox6.Location = new System.Drawing.Point(3, 283);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(324, 50);
            this.groupBox6.TabIndex = 27;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Fecha de nacimiento";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox1);
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Controls.Add(this.groupBox6);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(335, 341);
            this.flowLayoutPanel1.TabIndex = 28;
            // 
            // button_siguiente
            // 
            this.button_siguiente.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_siguiente.Location = new System.Drawing.Point(191, 366);
            this.button_siguiente.Name = "button_siguiente";
            this.button_siguiente.Size = new System.Drawing.Size(75, 23);
            this.button_siguiente.TabIndex = 29;
            this.button_siguiente.Text = "Siguiente";
            this.button_siguiente.UseVisualStyleBackColor = true;
            this.button_siguiente.Click += new System.EventHandler(this.button_siguiente_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(272, 366);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 30;
            this.button_cancel.Text = "Cancelar";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // Form_CertificacionAfiliatoria
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.button_siguiente;
            this.ClientSize = new System.Drawing.Size(354, 401);
            this.ControlBox = false;
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_siguiente);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(370, 440);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(370, 440);
            this.Name = "Form_CertificacionAfiliatoria";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Certificación afiliatoria";
            this.Load += new System.EventHandler(this.Form_CertificacionAfiliatoria_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label label_paciente_NumAfiliado;
        private System.Windows.Forms.Label label_paciente_dni;
        private System.Windows.Forms.Label label_paciente_sexo;
        private System.Windows.Forms.Label label_paciente_apellido;
        private System.Windows.Forms.Label label_paciente_nombre;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button button_siguiente;
        private System.Windows.Forms.Button button_cancel;
    }
}