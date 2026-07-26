using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TechSystem2
{

public partial class Tecnicos : System.Web.UI.Page
{
    protected TextBox txtBuscarTecnico;
    protected Button btnBuscar;
    protected Button btnLimpiarBusqueda;
    protected HiddenField hfTecnicoID;
    protected TextBox txtNombre;
    protected TextBox txtEspecialidad;
    protected Button btnGuardar;
    protected Button btnNuevo;
    protected Label lblMensaje;
    protected Panel pnlConfirmacion;
    protected Button btnSiEliminar;
    protected Button btnNoCancelar;
    protected GridView gvTecnicos;

    TecnicoDatos tecnicoDatos = new TecnicoDatos();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LlenarGrid();
        }
    }

    private void LlenarGrid()
    {
        DataTable tabla = tecnicoDatos.ListarTodos();
        gvTecnicos.DataSource = tabla;
        gvTecnicos.DataBind();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string texto = txtBuscarTecnico.Text.Trim();
        if (texto == "")
        {
            LlenarGrid();
            lblMensaje.Text = "";
        }
        else
        {
            DataTable tabla = tecnicoDatos.Buscar(texto);
            gvTecnicos.DataSource = tabla;
            gvTecnicos.DataBind();
            lblMensaje.Text = "Resultados encontrados: " + tabla.Rows.Count;
        }
    }

    protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
    {
        txtBuscarTecnico.Text = "";
        LlenarGrid();
        lblMensaje.Text = "";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (txtNombre.Text.Trim() == "")
        {
            lblMensaje.Text = "Por favor escriba el nombre del tecnico.";
            return;
        }
        if (txtEspecialidad.Text.Trim() == "")
        {
            lblMensaje.Text = "Por favor escriba la especialidad del tecnico.";
            return;
        }

        string nombre = txtNombre.Text.Trim();
        string especialidad = txtEspecialidad.Text.Trim();

        if (hfTecnicoID.Value == "")
        {
            tecnicoDatos.Insertar(nombre, especialidad);
            lblMensaje.Text = "Tecnico guardado exitosamente.";
        }
        else
        {
            int tecnicoID = Convert.ToInt32(hfTecnicoID.Value);
            tecnicoDatos.Actualizar(tecnicoID, nombre, especialidad);
            lblMensaje.Text = "Tecnico actualizado exitosamente.";
        }

        LimpiarFormulario();
        LlenarGrid();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
        lblMensaje.Text = "";
    }

    protected void gvTecnicos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int tecnicoID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Seleccionar")
        {
            DataTable tabla = tecnicoDatos.ObtenerPorId(tecnicoID);
            if (tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                hfTecnicoID.Value = fila["TecnicoID"].ToString();
                txtNombre.Text = fila["Nombre"].ToString();
                txtEspecialidad.Text = fila["Especialidad"].ToString();
                lblMensaje.Text = "Tecnico cargado. Modifique los datos y presione Guardar.";
            }
        }
        else if (e.CommandName == "Eliminar")
        {
            hfTecnicoID.Value = tecnicoID.ToString();
            pnlConfirmacion.Visible = true;
            lblMensaje.Text = "";
        }
    }

    protected void gvTecnicos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkSeleccionar = (LinkButton)e.Row.FindControl("lnkSeleccionar");
            LinkButton lnkEliminar = (LinkButton)e.Row.FindControl("lnkEliminar");
            if (lnkSeleccionar != null) lnkSeleccionar.CssClass = "btn-accion";
            if (lnkEliminar != null) lnkEliminar.CssClass = "btn-eliminar";
        }
    }

    protected void btnSiEliminar_Click(object sender, EventArgs e)
    {
        int tecnicoID = Convert.ToInt32(hfTecnicoID.Value);
        tecnicoDatos.Eliminar(tecnicoID);
        lblMensaje.Text = "Tecnico eliminado exitosamente.";
        pnlConfirmacion.Visible = false;
        LimpiarFormulario();
        LlenarGrid();
    }

    protected void btnNoCancelar_Click(object sender, EventArgs e)
    {
        pnlConfirmacion.Visible = false;
        lblMensaje.Text = "";
    }

    private void LimpiarFormulario()
    {
        hfTecnicoID.Value = "";
        txtNombre.Text = "";
        txtEspecialidad.Text = "";
    }
}

}
