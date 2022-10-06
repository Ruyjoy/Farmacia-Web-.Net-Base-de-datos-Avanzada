using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EntidadesCompartidas;

namespace Logica
{
    public interface ILogicaFarma
    {
        void Alta(Encargado login, Farmaceutica farma);
        void Baja(Encargado login, Farmaceutica farma);
        void Modificar(Encargado login, Farmaceutica farma);
        Farmaceutica Consulta(Encargado login, string nombre);
        Farmaceutica ConsultaTodas(Encargado login, string nombre);
    }
}
