
namespace Femecon
{
    partial class Form_progresoAutorizacion
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_progresoAutorizacion));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.richTextBox_practicas = new System.Windows.Forms.RichTextBox();
            this.backgroundWorker_ChromeDriver = new System.ComponentModel.BackgroundWorker();
            this.label_Mensaje = new System.Windows.Forms.Label();
            this.label_PracticaAutorizando = new System.Windows.Forms.Label();
            this.label_Porcentaje = new System.Windows.Forms.Label();
            this.button_Copiar = new System.Windows.Forms.Button();
            this.button_Cerrar = new System.Windows.Forms.Button();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label_Titulo = new System.Windows.Forms.Label();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 266);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(345, 23);
            this.progressBar.TabIndex = 0;
            // 
            // richTextBox_practicas
            // 
            this.richTextBox_practicas.Location = new System.Drawing.Point(11, 29);
            this.richTextBox_practicas.Name = "richTextBox_practicas";
            this.richTextBox_practicas.ReadOnly = true;
            this.richTextBox_practicas.Size = new System.Drawing.Size(345, 163);
            this.richTextBox_practicas.TabIndex = 1;
            this.richTextBox_practicas.Text = "";
            // 
            // label_Mensaje
            // 
            this.label_Mensaje.AutoSize = true;
            this.label_Mensaje.Location = new System.Drawing.Point(3, 0);
            this.label_Mensaje.Name = "label_Mensaje";
            this.label_Mensaje.Size = new System.Drawing.Size(75, 13);
            this.label_Mensaje.TabIndex = 2;
            this.label_Mensaje.Text = "Autorizando... ";
            // 
            // label_PracticaAutorizando
            // 
            this.label_PracticaAutorizando.AutoSize = true;
            this.label_PracticaAutorizando.Location = new System.Drawing.Point(84, 0);
            this.label_PracticaAutorizando.Name = "label_PracticaAutorizando";
            this.label_PracticaAutorizando.Size = new System.Drawing.Size(60, 13);
            this.label_PracticaAutorizando.TabIndex = 3;
            this.label_PracticaAutorizando.Text = "PRACTICA";
            // 
            // label_Porcentaje
            // 
            this.label_Porcentaje.AutoSize = true;
            this.label_Porcentaje.Location = new System.Drawing.Point(324, 250);
            this.label_Porcentaje.Name = "label_Porcentaje";
            this.label_Porcentaje.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label_Porcentaje.Size = new System.Drawing.Size(33, 13);
            this.label_Porcentaje.TabIndex = 4;
            this.label_Porcentaje.Text = "100%";
            this.label_Porcentaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_Copiar
            // 
            this.button_Copiar.Location = new System.Drawing.Point(201, 198);
            this.button_Copiar.Name = "button_Copiar";
            this.button_Copiar.Size = new System.Drawing.Size(75, 23);
            this.button_Copiar.TabIndex = 5;
            this.button_Copiar.Text = "Copiar";
            this.button_Copiar.UseVisualStyleBackColor = true;
            this.button_Copiar.Click += new System.EventHandler(this.button_Copiar_Click);
            // 
            // button_Cerrar
            // 
            this.button_Cerrar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button_Cerrar.Location = new System.Drawing.Point(282, 198);
            this.button_Cerrar.Name = "button_Cerrar";
            this.button_Cerrar.Size = new System.Drawing.Size(75, 23);
            this.button_Cerrar.TabIndex = 6;
            this.button_Cerrar.Text = "Cerrar";
            this.button_Cerrar.UseVisualStyleBackColor = true;
            this.button_Cerrar.Click += new System.EventHandler(this.button_Cerrar_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label_Mensaje);
            this.flowLayoutPanel1.Controls.Add(this.label_PracticaAutorizando);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 243);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(291, 17);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // label_Titulo
            // 
            this.label_Titulo.AutoSize = true;
            this.label_Titulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_Titulo.Location = new System.Drawing.Point(9, 9);
            this.label_Titulo.Name = "label_Titulo";
            this.label_Titulo.Size = new System.Drawing.Size(160, 17);
            this.label_Titulo.TabIndex = 8;
            this.label_Titulo.Text = "Códigos de autorización";
            // 
            // Form_progresoAutorizacion
            // 
            this.AcceptButton = this.button_Copiar;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button_Cerrar;
            this.ClientSize = new System.Drawing.Size(368, 301);
            this.Controls.Add(this.label_Titulo);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.button_Cerrar);
            this.Controls.Add(this.button_Copiar);
            this.Controls.Add(this.label_Porcentaje);
            this.Controls.Add(this.richTextBox_practicas);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form_progresoAutorizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Autorizaciones";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_progresoAutorizacion_FormClosing);
            this.Load += new System.EventHandler(this.Form_progresoAutorizacion_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.RichTextBox richTextBox_practicas;
        private System.ComponentModel.BackgroundWorker backgroundWorker_ChromeDriver;
        private System.Windows.Forms.Label label_Mensaje;
        private System.Windows.Forms.Label label_PracticaAutorizando;
        private System.Windows.Forms.Label label_Porcentaje;
        private System.Windows.Forms.Button button_Copiar;
        private System.Windows.Forms.Button button_Cerrar;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label_Titulo;
    }
}