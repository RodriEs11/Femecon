
namespace Updater
{
    partial class Updater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Updater));
            this.button_descargar = new System.Windows.Forms.Button();
            this.barraProgreso = new System.Windows.Forms.ProgressBar();
            this.button_cerrar = new System.Windows.Forms.Button();
            this.richTextBox_notas = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label_versionNueva = new System.Windows.Forms.Label();
            this.label_versionLocal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_descargar
            // 
            this.button_descargar.Location = new System.Drawing.Point(185, 303);
            this.button_descargar.Name = "button_descargar";
            this.button_descargar.Size = new System.Drawing.Size(75, 23);
            this.button_descargar.TabIndex = 0;
            this.button_descargar.Text = "Instalar";
            this.button_descargar.UseVisualStyleBackColor = true;
            this.button_descargar.Click += new System.EventHandler(this.button_descargar_Click);
            // 
            // barraProgreso
            // 
            this.barraProgreso.Location = new System.Drawing.Point(12, 274);
            this.barraProgreso.Name = "barraProgreso";
            this.barraProgreso.Size = new System.Drawing.Size(329, 23);
            this.barraProgreso.TabIndex = 1;
            // 
            // button_cerrar
            // 
            this.button_cerrar.Location = new System.Drawing.Point(266, 303);
            this.button_cerrar.Name = "button_cerrar";
            this.button_cerrar.Size = new System.Drawing.Size(75, 23);
            this.button_cerrar.TabIndex = 2;
            this.button_cerrar.Text = "Cerrar";
            this.button_cerrar.UseVisualStyleBackColor = true;
            this.button_cerrar.Click += new System.EventHandler(this.button_cerrar_Click);
            // 
            // richTextBox_notas
            // 
            this.richTextBox_notas.Location = new System.Drawing.Point(12, 127);
            this.richTextBox_notas.Name = "richTextBox_notas";
            this.richTextBox_notas.ReadOnly = true;
            this.richTextBox_notas.Size = new System.Drawing.Size(329, 141);
            this.richTextBox_notas.TabIndex = 3;
            this.richTextBox_notas.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Versión reciente:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Versión instalada:";
            // 
            // label_versionNueva
            // 
            this.label_versionNueva.AutoSize = true;
            this.label_versionNueva.Location = new System.Drawing.Point(116, 56);
            this.label_versionNueva.Name = "label_versionNueva";
            this.label_versionNueva.Size = new System.Drawing.Size(19, 13);
            this.label_versionNueva.TabIndex = 6;
            this.label_versionNueva.Text = "----";
            // 
            // label_versionLocal
            // 
            this.label_versionLocal.AutoSize = true;
            this.label_versionLocal.Location = new System.Drawing.Point(121, 80);
            this.label_versionLocal.Name = "label_versionLocal";
            this.label_versionLocal.Size = new System.Drawing.Size(19, 13);
            this.label_versionLocal.TabIndex = 7;
            this.label_versionLocal.Text = "----";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(25, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 13);
            this.label3.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(19, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(322, 15);
            this.label4.TabIndex = 9;
            this.label4.Text = "Se ha detectado una versión más reciente para descargar";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 111);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(126, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Notas de la actualización";
            // 
            // Updater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(353, 338);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label_versionLocal);
            this.Controls.Add(this.label_versionNueva);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.richTextBox_notas);
            this.Controls.Add(this.button_cerrar);
            this.Controls.Add(this.barraProgreso);
            this.Controls.Add(this.button_descargar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(369, 377);
            this.Name = "Updater";
            this.Text = "Actualización";
            this.Load += new System.EventHandler(this.Updater_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_descargar;
        private System.Windows.Forms.ProgressBar barraProgreso;
        private System.Windows.Forms.Button button_cerrar;
        private System.Windows.Forms.RichTextBox richTextBox_notas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_versionNueva;
        private System.Windows.Forms.Label label_versionLocal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}