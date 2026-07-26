<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>TechSystem - Departamento Tecnico</title>
    <link rel="stylesheet" type="text/css" href="Style.css" />
</head>
<body>
    <form id="form1" runat="server">

        <!-- Encabezado con navegacion -->
        <div class="header">
            <div class="titulo-app">TechSystem - Soporte Tecnico</div>
            <div class="links-nav">
                <a href="Default.aspx">Inicio</a>
                <a href="Usuarios.aspx">Usuarios</a>
                <a href="Equipos.aspx">Equipos</a>
                <a href="Tecnicos.aspx">Tecnicos</a>
            </div>
        </div>

        <!-- Contenido de portada -->
        <div class="contenedor">
            <div class="tarjeta" style="text-align: center; padding: 60px 30px;">
                <h2 style="font-size: 28px; border-bottom: none; margin-bottom: 10px;">
                    Sistema de Soporte Tecnico
                </h2>
                <p style="color: #8b949e; font-size: 16px; margin-bottom: 30px;">
                    Gestion de usuarios, equipos y tecnicos para el departamento tecnico
                </p>
                <hr style="border: 1px solid #30363d; margin: 25px 0;" />
                <p style="color: #58a6ff; font-size: 15px;">
                    Dr. Randall Sanchez Perez
                </p>
                <p style="color: #8b949e; font-size: 14px;">
                    Universidad Hispanoamericana, Costa Rica, 2026
                </p>
            </div>

            <!-- Resumen de lo que hace el sistema -->
            <div class="tarjeta">
                <h2>Que puedes hacer en este sistema</h2>
                <ul style="line-height: 2; font-size: 15px;">
                    <li><strong>Usuarios:</strong> Agregar, modificar, consultar y eliminar usuarios (buscar con filtro)</li>
                    <li><strong>Equipos:</strong> Registrar equipos asignados a usuarios (buscar con filtro)</li>
                    <li><strong>Tecnicos:</strong> Administrar el equipo de tecnicos y sus especialidades (buscar con filtro)</li>
                </ul>
            </div>
        </div>

        <!-- Pie de pagina -->
        <div class="footer">
            TechSystem - Hecho en C# - Curso de Programacion II &copy; 2026
        </div>
    </form>
</body>
</html>
