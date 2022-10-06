using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;
using Persistencia;
using System.Text.RegularExpressions;

namespace Logica
{
    internal class LogicaUsuario:ILogicaUsuario
    {

        private static LogicaUsuario _instancia = null;

        private LogicaUsuario() { }

        public static LogicaUsuario GetInstancia()
        {
            if (_instancia == null)
                _instancia = new LogicaUsuario();

            return _instancia;
        }

        public Usuario Login(string nombreUsuario, string pass)
        {
            IPersistenciaEmpleado persistencia = FabricaPersistencia.getPersistenciaEmpleado();
            try
            {
                Empleado E = persistencia.Login(nombreUsuario, pass);
                return E;
            }
            catch
            {
                IPersistenciaEncargado persistenciaEn = FabricaPersistencia.getPersistenciaEncargado();
                Encargado en = persistenciaEn.Login(nombreUsuario, pass);
                return en;
            }

        }       

        public void Alta(Encargado login, Usuario pUsuario)
        {
            if (pUsuario is Empleado)
            {
                IPersistenciaEmpleado IPEm = FabricaPersistencia.getPersistenciaEmpleado();
                IPEm.Alta(login, (Empleado)pUsuario);
            }
            else
            {
                IPersistenciaEncargado IPEn = FabricaPersistencia.getPersistenciaEncargado();
                IPEn.Alta(login, (Encargado)pUsuario);
            }
        }

        public void Baja(Encargado login, Usuario pUsuario)
        {
            if (pUsuario is Empleado)
            {
                IPersistenciaEmpleado IPEm = FabricaPersistencia.getPersistenciaEmpleado();
                IPEm.Baja(login, (Empleado)pUsuario);
            }
            else
            {
                throw new Exception("Acción no permitida.");
            }
        }

        public void Mod(Encargado login, Usuario pUsuario)
        {
            if (pUsuario is Empleado)
            {
                IPersistenciaEmpleado IPEm = FabricaPersistencia.getPersistenciaEmpleado();
                IPEm.Modificar(login, (Empleado)pUsuario);
            }
            else
            {
                throw new Exception("Acción no permitida.");
            }
        }

        public void CambioPass(Usuario pUsuario, string passNuevo)
        {
            Regex regexCarac = new Regex(@"([a-z][A-Z]*)");
            Regex regexNum = new Regex(@"([0-9])");
            MatchCollection matches = regexCarac.Matches(passNuevo);
            if (matches.Count == 5)
            {
                matches = regexNum.Matches(passNuevo);
                if (matches.Count != 2)
                    throw new Exception("El nuevo pass debe tener dos números.");
            }
            else
                throw new Exception("El nuevo pass debe tener 5 letras.");
                
            if (pUsuario is Empleado)
            {
                IPersistenciaEmpleado IPEm = FabricaPersistencia.getPersistenciaEmpleado();
                IPEm.CambioPass((Empleado)pUsuario, passNuevo);
            }
            else
            {
                IPersistenciaEncargado IPEn = FabricaPersistencia.getPersistenciaEncargado();
                IPEn.CambioPass((Encargado)pUsuario, passNuevo);
            }
        }


        public Usuario Consulta(Encargado login, int pCedula)
        {
            IPersistenciaEmpleado persistencia = FabricaPersistencia.getPersistenciaEmpleado();
            Empleado E = persistencia.Consulta(login, pCedula);
            if (E != null)
            {
                return E;
            }
            else
            {
                IPersistenciaEncargado persistenciaEn = FabricaPersistencia.getPersistenciaEncargado();
                Encargado en = persistenciaEn.Consulta(login, pCedula);
                return en;
            }
        }
    }
}
