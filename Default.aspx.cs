using System; // por si acaso

/// <summary>
/// Pagina de inicio (portada) del sistema TechSystem.
/// Solo muestra informacion del proyecto, no tiene logica de base de datos.
/// </summary>
public partial class Default : System.Web.UI.Page
{
    /// <summary>
    /// Cuando la pagina se carga, no se necesita hacer nada especial.
    /// Esta pagina es solo informativa.
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        // No hay nada que cargar en esta pagina de portada
        // Solo es la pagina de bienvenida del sistema
    }
}
