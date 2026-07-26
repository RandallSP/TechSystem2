using System;
using System.Data;
using System.Data.SqlClient;

namespace TechSystem2
{

public class UsuarioDatos
{
    private string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

    public DataTable ListarTodos()
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT UsuarioID, Nombre, CorreoElectronico, Telefono FROM Usuarios ORDER BY Nombre";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable Buscar(string texto)
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT UsuarioID, Nombre, CorreoElectronico, Telefono FROM Usuarios WHERE Nombre LIKE '%" + texto + "%' OR CorreoElectronico LIKE '%" + texto + "%' ORDER BY Nombre";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable ObtenerPorId(int usuarioID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT UsuarioID, Nombre, CorreoElectronico, Telefono FROM Usuarios WHERE UsuarioID = " + usuarioID;
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public void Insertar(string nombre, string correo, string telefono)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "INSERT INTO Usuarios (Nombre, CorreoElectronico, Telefono) VALUES ('" + nombre + "', '" + correo + "', '" + telefono + "')";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Actualizar(int usuarioID, string nombre, string correo, string telefono)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "UPDATE Usuarios SET Nombre = '" + nombre + "', CorreoElectronico = '" + correo + "', Telefono = '" + telefono + "' WHERE UsuarioID = " + usuarioID;
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Eliminar(int usuarioID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "DELETE FROM Usuarios WHERE UsuarioID = " + usuarioID;
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}

}
