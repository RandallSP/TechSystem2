using System;
using System.Data;
using System.Data.SqlClient;

namespace TechSystem2
{

public class TecnicoDatos
{
    private string conexion = System.Configuration.ConfigurationManager.ConnectionStrings["ConexionDB"].ConnectionString;

    public DataTable ListarTodos()
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT TecnicoID, Nombre, Especialidad FROM Tecnicos ORDER BY Nombre";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable Buscar(string texto)
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT TecnicoID, Nombre, Especialidad FROM Tecnicos WHERE Nombre LIKE '%" + texto + "%' OR Especialidad LIKE '%" + texto + "%' ORDER BY Nombre";
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public DataTable ObtenerPorId(int tecnicoID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        string query = "SELECT TecnicoID, Nombre, Especialidad FROM Tecnicos WHERE TecnicoID = " + tecnicoID;
        SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
        DataTable tabla = new DataTable();
        adapter.Fill(tabla);
        conn.Close();
        return tabla;
    }

    public void Insertar(string nombre, string especialidad)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "INSERT INTO Tecnicos (Nombre, Especialidad) VALUES ('" + nombre + "', '" + especialidad + "')";
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Actualizar(int tecnicoID, string nombre, string especialidad)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "UPDATE Tecnicos SET Nombre = '" + nombre + "', Especialidad = '" + especialidad + "' WHERE TecnicoID = " + tecnicoID;
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }

    public void Eliminar(int tecnicoID)
    {
        SqlConnection conn = new SqlConnection(conexion);
        conn.Open();
        string query = "DELETE FROM Tecnicos WHERE TecnicoID = " + tecnicoID;
        SqlCommand cmd = new SqlCommand(query, conn);
        cmd.ExecuteNonQuery();
        conn.Close();
    }
}

}
