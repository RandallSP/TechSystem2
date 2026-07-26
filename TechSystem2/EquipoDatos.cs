using System;
using System.Data;
using System.Data.SqlClient;

namespace TechSystem2
{

public class EquipoDatos
{
    private string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

    public DataTable ListarTodos()
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT e.EquipoID, e.TipoEquipo, e.Modelo, e.UsuarioID, u.Nombre AS NombreUsuario FROM Equipos e LEFT JOIN Usuarios u ON e.UsuarioID = u.UsuarioID ORDER BY e.TipoEquipo";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable Buscar(string texto)
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT e.EquipoID, e.TipoEquipo, e.Modelo, e.UsuarioID, u.Nombre AS NombreUsuario FROM Equipos e LEFT JOIN Usuarios u ON e.UsuarioID = u.UsuarioID WHERE e.TipoEquipo LIKE '%" + texto + "%' OR e.Modelo LIKE '%" + texto + "%' OR u.Nombre LIKE '%" + texto + "%' ORDER BY e.TipoEquipo";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable ObtenerPorId(int equipoID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT EquipoID, TipoEquipo, Modelo, UsuarioID FROM Equipos WHERE EquipoID = " + equipoID;
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable ListarUsuarios()
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT UsuarioID, Nombre FROM Usuarios ORDER BY Nombre";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public void Insertar(string tipoEquipo, string modelo, int usuarioID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "INSERT INTO Equipos (TipoEquipo, Modelo, UsuarioID) VALUES ('" + tipoEquipo + "', '" + modelo + "', " + usuarioID + ")";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Actualizar(int equipoID, string tipoEquipo, string modelo, int usuarioID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "UPDATE Equipos SET TipoEquipo = '" + tipoEquipo + "', Modelo = '" + modelo + "', UsuarioID = " + usuarioID + " WHERE EquipoID = " + equipoID;
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Eliminar(int equipoID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "DELETE FROM Equipos WHERE EquipoID = " + equipoID;
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}

}
