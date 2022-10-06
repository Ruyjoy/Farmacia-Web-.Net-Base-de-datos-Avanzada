using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using ServicioWeb;
using System.Linq;
using System.Data;
using System.IO;
using System.Xml.Linq;
using System.ServiceModel;


public partial class ConsultaMedicamentos : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            IServicioWebBiosFarma miSer = new ServicioWebBiosFarmaClient();
            try
            {
                //obtengo el xml desde el WS
                XmlElement xml = miSer.ListaMedXml();

                XmlDocument doc = new XmlDocument();
                XmlNode _Documento = doc.CreateNode(XmlNodeType.Element, "Medicamentos", "");
                _Documento.InnerXml = xml.InnerXml;
                doc.AppendChild(_Documento);
                XElement _xDoc = new XElement(XElement.Parse(doc.OuterXml));
                Session["Medicamentos"] = _xDoc;
                mostrarTodos();
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

    private void mostrarTodos()
    {
        XElement _xDoc = (XElement)Session["Medicamentos"];
        var resultado = from m in _xDoc.Descendants("Medicamento")
                        select new
                        {
                            Nombre = m.Element("Nombre").Value
                        };
        rplistado.DataSource = resultado;
        rplistado.DataBind();
    }

    private void mostrarTipo(string tipo)
    {
        XElement _xDoc = (XElement)Session["Medicamentos"];
        var resultado = from m in _xDoc.Descendants("Medicamento")
                        where m.Element("Tipo").Value.Equals(tipo)
                        select new
                        {
                            Nombre = m.Element("Nombre").Value
                        };
        rplistado.DataSource = resultado;
        rplistado.DataBind();
    }
    protected void rpResultado_ItemCommand(object sender, RepeaterCommandEventArgs e)
    {

        try
        {
            if (e.CommandName == "Listar")
            {
                XElement _xDoc = (XElement)Session["Medicamentos"];
                string nomMed = ((TextBox)(e.Item.Controls[1])).Text;
                var resultado = from unNodo in _xDoc.Descendants("Medicamento")
                                where unNodo.Element("Nombre").Value.Equals(nomMed)
                                select unNodo;

                string _resultado = "<Medicamentos>";
                foreach (var unNodo in resultado)
                {
                    _resultado += unNodo.ToString();
                }
                _resultado += "</Medicamentos>";
                XmlListar.DocumentContent = _resultado;
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
    protected void ddlTipos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string tipo = ddlTipos.SelectedValue;
            if (tipo != "Todos")
                mostrarTipo(tipo);
            else
                mostrarTodos();
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
