namespace ServicioControlHoras
{
    partial class ServicioControlHs
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

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar 
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.Cronometro = new System.Windows.Forms.Timer(this.components);
            this.FSWLogueo = new System.IO.FileSystemWatcher();
            this.ELViewer = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.FSWLogueo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ELViewer)).BeginInit();
            // 
            // Cronometro
            // 
            this.Cronometro.Enabled = true;
            this.Cronometro.Interval = 60000;
            // 
            // FSWLogueo
            // 
            this.FSWLogueo.EnableRaisingEvents = true;
            this.FSWLogueo.Created += new System.IO.FileSystemEventHandler(this.FSWLogueo_Created);
            this.FSWLogueo.Deleted += new System.IO.FileSystemEventHandler(this.FSWLogueo_Deleted);
            // 
            // ServicioControlHs
            // 
            this.ServiceName = "ServicioControlHs";
            ((System.ComponentModel.ISupportInitialize)(this.FSWLogueo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ELViewer)).EndInit();

        }

        #endregion

        private System.Windows.Forms.Timer Cronometro;
        private System.IO.FileSystemWatcher FSWLogueo;
        private System.Diagnostics.EventLog ELViewer;
    }
}
