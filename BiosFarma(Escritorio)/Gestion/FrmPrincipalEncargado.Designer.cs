namespace Gestion
{
    partial class FrmPrincipalEncargado
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmPrincipalEncargado));
            this.BarraMenu = new System.Windows.Forms.MenuStrip();
            this.terminalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.altaEncargadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambioDePassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBmEmpleadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.farmaceuticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMFarmaceuticaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.medicamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aBMMedicamentoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.litarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambioDeEstadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pedidoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.BarraMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // BarraMenu
            // 
            this.BarraMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.terminalesToolStripMenuItem,
            this.aBmEmpleadoToolStripMenuItem,
            this.farmaceuticaToolStripMenuItem,
            this.medicamentoToolStripMenuItem,
            this.litarToolStripMenuItem,
            this.cambioDeEstadoToolStripMenuItem});
            this.BarraMenu.Location = new System.Drawing.Point(0, 0);
            this.BarraMenu.Name = "BarraMenu";
            this.BarraMenu.Size = new System.Drawing.Size(610, 24);
            this.BarraMenu.TabIndex = 2;
            this.BarraMenu.Text = "menuStrip1";
            // 
            // terminalesToolStripMenuItem
            // 
            this.terminalesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.altaEncargadoToolStripMenuItem,
            this.cambioDePassToolStripMenuItem});
            this.terminalesToolStripMenuItem.Name = "terminalesToolStripMenuItem";
            this.terminalesToolStripMenuItem.Size = new System.Drawing.Size(75, 20);
            this.terminalesToolStripMenuItem.Text = "Encargado";
            // 
            // altaEncargadoToolStripMenuItem
            // 
            this.altaEncargadoToolStripMenuItem.Name = "altaEncargadoToolStripMenuItem";
            this.altaEncargadoToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.altaEncargadoToolStripMenuItem.Text = "Alta Encargado";
            this.altaEncargadoToolStripMenuItem.Click += new System.EventHandler(this.altaEncargadoToolStripMenuItem_Click);
            // 
            // cambioDePassToolStripMenuItem
            // 
            this.cambioDePassToolStripMenuItem.Name = "cambioDePassToolStripMenuItem";
            this.cambioDePassToolStripMenuItem.Size = new System.Drawing.Size(158, 22);
            this.cambioDePassToolStripMenuItem.Text = "Cambio de Pass";
            this.cambioDePassToolStripMenuItem.Click += new System.EventHandler(this.cambioDePassToolStripMenuItem_Click);
            // 
            // aBmEmpleadoToolStripMenuItem
            // 
            this.aBmEmpleadoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empleadoToolStripMenuItem});
            this.aBmEmpleadoToolStripMenuItem.Name = "aBmEmpleadoToolStripMenuItem";
            this.aBmEmpleadoToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.aBmEmpleadoToolStripMenuItem.Text = "Empleados";
            // 
            // empleadoToolStripMenuItem
            // 
            this.empleadoToolStripMenuItem.Name = "empleadoToolStripMenuItem";
            this.empleadoToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.empleadoToolStripMenuItem.Text = "ABMEmpleado";
            this.empleadoToolStripMenuItem.Click += new System.EventHandler(this.empleadoToolStripMenuItem_Click);
            // 
            // farmaceuticaToolStripMenuItem
            // 
            this.farmaceuticaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMFarmaceuticaToolStripMenuItem});
            this.farmaceuticaToolStripMenuItem.Name = "farmaceuticaToolStripMenuItem";
            this.farmaceuticaToolStripMenuItem.Size = new System.Drawing.Size(90, 20);
            this.farmaceuticaToolStripMenuItem.Text = "Farmaceutica";
            // 
            // aBMFarmaceuticaToolStripMenuItem
            // 
            this.aBMFarmaceuticaToolStripMenuItem.Name = "aBMFarmaceuticaToolStripMenuItem";
            this.aBMFarmaceuticaToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.aBMFarmaceuticaToolStripMenuItem.Text = "ABMFarmaceutica";
            this.aBMFarmaceuticaToolStripMenuItem.Click += new System.EventHandler(this.aBMFarmaceuticaToolStripMenuItem_Click);
            // 
            // medicamentoToolStripMenuItem
            // 
            this.medicamentoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aBMMedicamentoToolStripMenuItem});
            this.medicamentoToolStripMenuItem.Name = "medicamentoToolStripMenuItem";
            this.medicamentoToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.medicamentoToolStripMenuItem.Text = "Medicamento";
            // 
            // aBMMedicamentoToolStripMenuItem
            // 
            this.aBMMedicamentoToolStripMenuItem.Name = "aBMMedicamentoToolStripMenuItem";
            this.aBMMedicamentoToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.aBMMedicamentoToolStripMenuItem.Text = "ABMMedicamento";
            this.aBMMedicamentoToolStripMenuItem.Click += new System.EventHandler(this.aBMMedicamentoToolStripMenuItem_Click);
            // 
            // litarToolStripMenuItem
            // 
            this.litarToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listarToolStripMenuItem});
            this.litarToolStripMenuItem.Name = "litarToolStripMenuItem";
            this.litarToolStripMenuItem.Size = new System.Drawing.Size(113, 20);
            this.litarToolStripMenuItem.Text = "Listado de Pedido";
            // 
            // listarToolStripMenuItem
            // 
            this.listarToolStripMenuItem.Name = "listarToolStripMenuItem";
            this.listarToolStripMenuItem.Size = new System.Drawing.Size(102, 22);
            this.listarToolStripMenuItem.Text = "Listar";
            this.listarToolStripMenuItem.Click += new System.EventHandler(this.listarToolStripMenuItem_Click);
            // 
            // cambioDeEstadoToolStripMenuItem
            // 
            this.cambioDeEstadoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pedidoToolStripMenuItem});
            this.cambioDeEstadoToolStripMenuItem.Name = "cambioDeEstadoToolStripMenuItem";
            this.cambioDeEstadoToolStripMenuItem.Size = new System.Drawing.Size(118, 20);
            this.cambioDeEstadoToolStripMenuItem.Text = "Cambio de estado ";
            // 
            // pedidoToolStripMenuItem
            // 
            this.pedidoToolStripMenuItem.Name = "pedidoToolStripMenuItem";
            this.pedidoToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.pedidoToolStripMenuItem.Text = "Pedido";
            this.pedidoToolStripMenuItem.Click += new System.EventHandler(this.pedidoToolStripMenuItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(160, 38);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(200, 200);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // FrmPrincipalEncargado
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 259);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.BarraMenu);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmPrincipalEncargado";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.BarraMenu.ResumeLayout(false);
            this.BarraMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip BarraMenu;
        private System.Windows.Forms.ToolStripMenuItem aBmEmpleadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminalesToolStripMenuItem;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem farmaceuticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBMFarmaceuticaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem medicamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aBMMedicamentoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambioDePassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem litarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambioDeEstadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pedidoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem altaEncargadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarToolStripMenuItem;
    }
}

