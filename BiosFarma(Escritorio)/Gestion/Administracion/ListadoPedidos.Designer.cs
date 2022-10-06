namespace Gestion.Administracion
{
    partial class ListadoPedidos
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.ddlFarma = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Filtro3 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.Filtro1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Filtro2 = new System.Windows.Forms.Button();
            this.Gvtodo = new System.Windows.Forms.DataGridView();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.statusBar = new System.Windows.Forms.StatusStrip();
            this.lblError = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gvtodo)).BeginInit();
            this.statusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.ddlFarma);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.Filtro3);
            this.panel3.Location = new System.Drawing.Point(535, 17);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(193, 65);
            this.panel3.TabIndex = 52;
            // 
            // ddlFarma
            // 
            this.ddlFarma.FormattingEnabled = true;
            this.ddlFarma.Location = new System.Drawing.Point(16, 35);
            this.ddlFarma.Name = "ddlFarma";
            this.ddlFarma.Size = new System.Drawing.Size(91, 21);
            this.ddlFarma.TabIndex = 48;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 47;
            this.label3.Text = "Farmacéutica:";
            // 
            // Filtro3
            // 
            this.Filtro3.Location = new System.Drawing.Point(113, 35);
            this.Filtro3.Name = "Filtro3";
            this.Filtro3.Size = new System.Drawing.Size(75, 23);
            this.Filtro3.TabIndex = 45;
            this.Filtro3.Text = "Filtrar";
            this.Filtro3.UseVisualStyleBackColor = true;
            this.Filtro3.Click += new System.EventHandler(this.Filtro3_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.Filtro1);
            this.panel2.Location = new System.Drawing.Point(61, 17);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(269, 65);
            this.panel2.TabIndex = 53;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(72, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 13);
            this.label2.TabIndex = 48;
            this.label2.Text = "Resume de Empleado";
            // 
            // Filtro1
            // 
            this.Filtro1.Location = new System.Drawing.Point(85, 35);
            this.Filtro1.Name = "Filtro1";
            this.Filtro1.Size = new System.Drawing.Size(75, 23);
            this.Filtro1.TabIndex = 43;
            this.Filtro1.Text = "Filtrar";
            this.Filtro1.UseVisualStyleBackColor = true;
            this.Filtro1.Click += new System.EventHandler(this.Filtro1_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Filtro2);
            this.panel1.Location = new System.Drawing.Point(336, 17);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(193, 65);
            this.panel1.TabIndex = 51;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 13);
            this.label1.TabIndex = 47;
            this.label1.Text = "Resumen de Medicamento";
            // 
            // Filtro2
            // 
            this.Filtro2.Location = new System.Drawing.Point(57, 35);
            this.Filtro2.Name = "Filtro2";
            this.Filtro2.Size = new System.Drawing.Size(75, 23);
            this.Filtro2.TabIndex = 0;
            this.Filtro2.Text = "Filtrar";
            this.Filtro2.UseVisualStyleBackColor = true;
            this.Filtro2.Click += new System.EventHandler(this.Filtro2_Click);
            // 
            // Gvtodo
            // 
            this.Gvtodo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Gvtodo.Location = new System.Drawing.Point(61, 88);
            this.Gvtodo.Name = "Gvtodo";
            this.Gvtodo.Size = new System.Drawing.Size(667, 126);
            this.Gvtodo.TabIndex = 49;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Location = new System.Drawing.Point(724, 230);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpiar.TabIndex = 50;
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.UseVisualStyleBackColor = true;
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // statusBar
            // 
            this.statusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblError});
            this.statusBar.Location = new System.Drawing.Point(0, 260);
            this.statusBar.Name = "statusBar";
            this.statusBar.Padding = new System.Windows.Forms.Padding(1, 0, 10, 0);
            this.statusBar.Size = new System.Drawing.Size(819, 22);
            this.statusBar.TabIndex = 54;
            this.statusBar.Text = "statusStrip1";
            // 
            // lblError
            // 
            this.lblError.Name = "lblError";
            this.lblError.Size = new System.Drawing.Size(0, 17);
            // 
            // ListadoPedidos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 282);
            this.Controls.Add(this.statusBar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Gvtodo);
            this.Controls.Add(this.btnLimpiar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ListadoPedidos";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ListadoPedidos";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Gvtodo)).EndInit();
            this.statusBar.ResumeLayout(false);
            this.statusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Filtro3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button Filtro1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Filtro2;
        private System.Windows.Forms.DataGridView Gvtodo;
        private System.Windows.Forms.Button btnLimpiar;
        private System.Windows.Forms.StatusStrip statusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblError;
        private System.Windows.Forms.ComboBox ddlFarma;
    }
}