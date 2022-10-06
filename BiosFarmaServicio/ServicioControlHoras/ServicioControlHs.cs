using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using System.Xml;
using System.Xml.Linq;
using System.Text;
using System.Configuration;
using System.Configuration.Install;
using System.Collections;
using System.IO;
using ServicioControlHoras.ServicioWeb;
using System.Timers;

namespace ServicioControlHoras
{
    partial class ServicioControlHs : ServiceBase
    {
        
        public ServicioControlHs()
        {
            
                InitializeComponent();

                if (!System.Diagnostics.EventLog.SourceExists("ServicioControlHs"))
                    System.Diagnostics.EventLog.CreateEventSource("ServicioControlHs", "");
                ELViewer.Source = "ServicioControlHs";

             try
             {
                    //FSWLogueo.Path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\BiosFarma" + ConfigurationManager.AppSettings["fswPath"];
                    string pathEscritorio = System.Windows.Forms.Application.StartupPath;
                FSWLogueo.Path = pathEscritorio + ConfigurationManager.AppSettings["fswPath"];

                string path = FSWLogueo.Path;
            }
            catch (Exception ex)
            {
                ELViewer.WriteEntry(ex.Message);
            }

        }

        public void crearTimer()
        {
            var timer = new System.Timers.Timer(10000);
            timer.Elapsed += HandleTimerElapsed;
            timer.Start();
        }

        private void HandleTimerElapsed(object sender, ElapsedEventArgs e)
        {

            try
            {

                string path = FSWLogueo.Path;

                IServicioWebBiosFarma _una = new ServicioWebBiosFarmaClient();

                XmlDocument controlHoras = new XmlDocument();

                controlHoras.Load(path + @"\ControlHoras.xml");
                XmlNode nodoRaiz = controlHoras.DocumentElement;    
                int cedula = (Convert.ToInt32(nodoRaiz.ChildNodes[0].ChildNodes[0].InnerText));
                DateTime horaInicio = Convert.ToDateTime(nodoRaiz.ChildNodes[0].ChildNodes[1].InnerText);
                DateTime horaFinal = Convert.ToDateTime(nodoRaiz.ChildNodes[0].ChildNodes[2].InnerText);
                TimeSpan diferenciaHoras = DateTime.Now.Subtract(horaFinal);
                if (diferenciaHoras.TotalMinutes > 0)
                {
                    HorasExtras H = new HorasExtras();
                    H.Empleado = new Empleado();
                    ((Usuario)H.Empleado).Cedula = cedula;
                    H.Fecha = horaInicio.Date;
                    H.CantMinutos = Convert.ToInt32(diferenciaHoras.TotalMinutes);

                    _una.AgregarHorasExtras(H);
                    ELViewer.WriteEntry("Se actualizo/agrego correctamente una hora extra");
                }
            }
            catch (Exception ex)
            {
                ELViewer.WriteEntry(ex.Message);
            }
        }

        protected override void OnStart(string[] args)
        {
            Cronometro.Start();
            Cronometro.Enabled = true;
        }

        protected override void OnStop()
        {
            ELViewer.WriteEntry("El Servicio Control Hs fue detenido");
            Cronometro.Enabled = false;
            Cronometro.Stop();
        }

        protected override void OnPause()
        {
            ELViewer.WriteEntry("El Servicio Control Hs fue pausado");
            Cronometro.Enabled = false;
            Cronometro.Stop();
        }

        protected override void OnContinue()
        {
            ELViewer.WriteEntry("El Servicio Control Hs fue reiniciado");
            Cronometro.Enabled = true;
        }

        private void FSWLogueo_Created(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (e.Name.ToLowerInvariant().Contains("controlhoras"))
                {
                    crearTimer();
                    ELViewer.WriteEntry("Se encontro un archivo ControlHoras");
                }
                else
                    ELViewer.WriteEntry("No se encontro un archivo ControlHoras");
            }
            catch (Exception ex)
            {
                ELViewer.WriteEntry(ex.Message);
            }

        }

        private void FSWLogueo_Deleted(object sender, FileSystemEventArgs e)
        {

            try
            {
                string path = FSWLogueo.Path;

                if (e.Name.ToLowerInvariant().Contains("controlHoras"))
                {
                    Cronometro.Stop();
                    ELViewer.WriteEntry("Se ha eliminado un archivo ControlHoras");
                }
            }
            catch (Exception ex)
            {
                ELViewer.WriteEntry(ex.Message);
            }
        }

    }
}
