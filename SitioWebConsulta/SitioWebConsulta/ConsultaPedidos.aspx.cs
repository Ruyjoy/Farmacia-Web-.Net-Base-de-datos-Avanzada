using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ServicioWeb;
using System.ServiceModel;


public partial class ConsultaPedidos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnBuscar_Click(object sender, EventArgs e)
    {        
        try
        {
            if (txtbuscar.Text.Trim().Length == 0)
            {
                throw new Exception("Ingrese número de pedido.");
            }
            int pedido;
            if (!Int32.TryParse(txtbuscar.Text.Trim(), out pedido))
            {
                txtbuscar.Focus();
                throw new Exception("Número no válido.");
            }
            
            IServicioWebBiosFarma miServicio = new ServicioWebBiosFarmaClient();
            Pedido p = miServicio.ConsultaP(pedido);

            if (p != null)
            {
                lblEstado.Text = p.Estado;
                LblError.Text = "Búsqueda exitosa.";
            }
            else
            {
                lblEstado.Text = "";
                LblError.Text = "El pedido no existe.";
            }
        }
        catch (TimeoutException ex)
        {
            if (ex.Message.Length > 25)
                LblError.Text = "Tiempo de espera agotado.";
            else
                LblError.Text = "Error: " + ex.Message;
        }
        catch (FaultException ex)
        {
            if (ex.Message.Length > 25)
                LblError.Text = "Ocurrió un error inesperado.";
            else
                LblError.Text = "Error: " + ex.Message;
        }
        catch (Exception ex)
        {
            if (ex.Message.Length > 25)
                LblError.Text = "Ocurrió un error inesperado.";
            else
                LblError.Text = "Error: " + ex.Message;
        }

    }

}