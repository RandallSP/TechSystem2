using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TechSystem2
{

public partial class Equipos : System.Web.UI.Page
{
    protected TextBox txtBuscarEquipo;
    protected Button btnBuscar;
    protected Button btnLimpiarBusqueda;
    protected HiddenField hfEquipoID;
    protected TextBox txtTipoEquipo;
    protected TextBox txtModelo;
    protected DropDownList ddlUsuario;
    protected Button btnGuardar;
    protected Button btnNuevo;
    protected Label lblMensaje;
    protected Panel pnlConfirmacion;
    protected Button btnSiEliminar;
    protected Button btnNoCancelar;
    protected GridView gvEquipos;

    EquipoDatos equipoDatos = new EquipoDatos();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CargarDropdownUsuarios();
            LlenarGrid();
        }
    }

    private void CargarDropdownUsuarios()
    {
        DataTable tabla = equipoDatos.ListarUsuarios();
        ddlUsuario.DataSource = tabla;
        ddlUsuario.DataTextField = "Nombre";
        ddlUsuario.DataValueField = "UsuarioID";
        ddlUsuario.DataBind();
        ddlUsuario.Items.Insert(0, new ListItem("-- Seleccione un usuario --", ""));
    }

    private void LlenarGrid()
    {
        DataTable tabla = equipoDatos.ListarTodos();
        gvEquipos.DataSource = tabla;
        gvEquipos.DataBind();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string texto = txtBuscarEquipo.Text.Trim();
        if (texto == "")
        {
            LlenarGrid();
            lblMensaje.Text = "";
        }
        else
        {
            DataTable tabla = equipoDatos.Buscar(texto);
            gvEquipos.DataSource = tabla;
            gvEquipos.DataBind();
            lblMensaje.Text = "Resultados encontrados: " + tabla.Rows.Count;
        }
    }

    protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
    {
        txtBuscarEquipo.Text = "";
        LlenarGrid();
        lblMensaje.Text = "";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (txtTipoEquipo.Text.Trim() == "")
        {
            lblMensaje.Text = "Por favor escriba el tipo de equipo.";
            return;
        }
        if (txtModelo.Text.Trim() == "")
        {
            lblMensaje.Text = "Por favor escriba el modelo del equipo.";
            return;
        }
        if (ddlUsuario.SelectedValue == "")
        {
            lblMensaje.Text = "Por favor seleccione un usuario para este equipo.";
            return;
        }

        string tipoEquipo = txtTipoEquipo.Text.Trim();
        string modelo = txtModelo.Text.Trim();
        int usuarioID = Convert.ToInt32(ddlUsuario.SelectedValue);

        if (hfEquipoID.Value == "")
        {
            equipoDatos.Insertar(tipoEquipo, modelo, usuarioID);
            lblMensaje.Text = "Equipo guardado exitosamente.";
        }
        else
        {
            int equipoID = Convert.ToInt32(hfEquipoID.Value);
            equipoDatos.Actualizar(equipoID, tipoEquipo, modelo, usuarioID);
            lblMensaje.Text = "Equipo actualizado exitosamente.";
        }

        LimpiarFormulario();
        LlenarGrid();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
        lblMensaje.Text = "";
    }

    protected void gvEquipos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int equipoID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Seleccionar")
        {
            DataTable tabla = equipoDatos.ObtenerPorId(equipoID);
            if (tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                hfEquipoID.Value = fila["EquipoID"].ToString();
                txtTipoEquipo.Text = fila["TipoEquipo"].ToString();
                txtModelo.Text = fila["Modelo"].ToString();
                ddlUsuario.SelectedValue = fila["UsuarioID"].ToString();
                lblMensaje.Text = "Equipo cargado. Modifique los datos y presione Guardar.";
            }
        }
        else if (e.CommandName == "Eliminar")
        {
            hfEquipoID.Value = equipoID.ToString();
            pnlConfirmacion.Visible = true;
            lblMensaje.Text = "";
        }
    }

    protected void gvEquipos_RowDataBound(object sender, GridViewRowEventArgs e)
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
        int equipoID = Convert.ToInt32(hfEquipoID.Value);
        equipoDatos.Eliminar(equipoID);
        lblMensaje.Text = "Equipo eliminado exitosamente.";
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
        hfEquipoID.Value = "";
        txtTipoEquipo.Text = "";
        txtModelo.Text = "";
        if (ddlUsuario.Items.Count > 0) ddlUsuario.SelectedIndex = 0;
    }
}

}
