using System;
using System.Data;
using System.Web.UI.WebControls;

namespace TechSystem2
{

public partial class Usuarios : System.Web.UI.Page
{
    protected TextBox txtBuscarUsuario;
    protected Button btnBuscar;
    protected Button btnLimpiarBusqueda;
    protected HiddenField hfUsuarioID;
    protected TextBox txtNombre;
    protected TextBox txtCorreo;
    protected TextBox txtTelefono;
    protected Button btnGuardar;
    protected Button btnNuevo;
    protected Label lblMensaje;
    protected Panel pnlConfirmacion;
    protected Button btnSiEliminar;
    protected Button btnNoCancelar;
    protected GridView gvUsuarios;

    UsuarioDatos usuarioDatos = new UsuarioDatos();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LlenarGrid();
        }
    }

    private void LlenarGrid()
    {
        DataTable tabla = usuarioDatos.ListarTodos();
        gvUsuarios.DataSource = tabla;
        gvUsuarios.DataBind();
    }

    protected void btnBuscar_Click(object sender, EventArgs e)
    {
        string texto = txtBuscarUsuario.Text.Trim();

        if (texto == "")
        {
            LlenarGrid();
            lblMensaje.Text = "";
        }
        else
        {
            DataTable tabla = usuarioDatos.Buscar(texto);
            gvUsuarios.DataSource = tabla;
            gvUsuarios.DataBind();
            lblMensaje.Text = "Resultados encontrados: " + tabla.Rows.Count;
        }
    }

    protected void btnLimpiarBusqueda_Click(object sender, EventArgs e)
    {
        txtBuscarUsuario.Text = "";
        LlenarGrid();
        lblMensaje.Text = "";
    }

    protected void btnGuardar_Click(object sender, EventArgs e)
    {
        if (txtNombre.Text.Trim() == "")
        {
            lblMensaje.Text = "Por favor escriba el nombre del usuario.";
            return;
        }

        if (txtCorreo.Text.Trim() == "")
        {
            lblMensaje.Text = "Por favor escriba el correo electronico.";
            return;
        }

        string nombre = txtNombre.Text.Trim();
        string correo = txtCorreo.Text.Trim();
        string telefono = txtTelefono.Text.Trim();

        if (hfUsuarioID.Value == "")
        {
            usuarioDatos.Insertar(nombre, correo, telefono);
            lblMensaje.Text = "Usuario guardado exitosamente.";
        }
        else
        {
            int usuarioID = Convert.ToInt32(hfUsuarioID.Value);
            usuarioDatos.Actualizar(usuarioID, nombre, correo, telefono);
            lblMensaje.Text = "Usuario actualizado exitosamente.";
        }

        LimpiarFormulario();
        LlenarGrid();
    }

    protected void btnNuevo_Click(object sender, EventArgs e)
    {
        LimpiarFormulario();
        lblMensaje.Text = "";
    }

    protected void gvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int usuarioID = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Seleccionar")
        {
            DataTable tabla = usuarioDatos.ObtenerPorId(usuarioID);

            if (tabla.Rows.Count > 0)
            {
                DataRow fila = tabla.Rows[0];
                hfUsuarioID.Value = fila["UsuarioID"].ToString();
                txtNombre.Text = fila["Nombre"].ToString();
                txtCorreo.Text = fila["CorreoElectronico"].ToString();
                txtTelefono.Text = fila["Telefono"].ToString();
                lblMensaje.Text = "Usuario cargado. Modifique los datos y presione Guardar.";
            }
        }
        else if (e.CommandName == "Eliminar")
        {
            hfUsuarioID.Value = usuarioID.ToString();
            pnlConfirmacion.Visible = true;
            lblMensaje.Text = "";
        }
    }

    protected void gvUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkSeleccionar = (LinkButton)e.Row.FindControl("lnkSeleccionar");
            LinkButton lnkEliminar = (LinkButton)e.Row.FindControl("lnkEliminar");

            if (lnkSeleccionar != null)
            {
                lnkSeleccionar.CssClass = "btn-accion";
            }
            if (lnkEliminar != null)
            {
                lnkEliminar.CssClass = "btn-eliminar";
            }
        }
    }

    protected void btnSiEliminar_Click(object sender, EventArgs e)
    {
        int usuarioID = Convert.ToInt32(hfUsuarioID.Value);
        usuarioDatos.Eliminar(usuarioID);

        lblMensaje.Text = "Usuario eliminado exitosamente.";
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
        hfUsuarioID.Value = "";
        txtNombre.Text = "";
        txtCorreo.Text = "";
        txtTelefono.Text = "";
    }
}

}
