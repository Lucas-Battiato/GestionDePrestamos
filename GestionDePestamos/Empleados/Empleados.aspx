<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="GestionDePestamos.Empleados.Empleados" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">
        
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Panel de Gestión</h2>
                <p class="text-muted">Bienvenido al sistema. Rol actual: <asp:Label ID="lblRolActual" runat="server" CssClass="badge bg-info text-dark"></asp:Label></p>
            </div>
        </div>

        <h4 class="fw-semibold text-secondary mb-3">Operaciones Diarias</h4>
        <div class="row g-4 mb-5">
            
            <div class="col-md-4">
                <a href="GestionPrestamos.aspx" class="text-decoration-none">
                    <div class="card shadow-sm border-0 rounded-4 bg-primary text-white text-center p-4 hover-zoom h-100">
                        <h5 class="fw-bold m-0">📥 Evaluar Solicitudes</h5>
                    </div>
                </a>
            </div>

            <div class="col-md-4">
                <a href="CobroCuotas.aspx" class="text-decoration-none">
                    <div class="card shadow-sm border-0 rounded-4 bg-success text-white text-center p-4 hover-zoom h-100">
                        <h5 class="fw-bold m-0">💵 Registrar Cobros</h5>
                    </div>
                </a>
            </div>

            <div class="col-md-4">
                <a href="GestionClientes.aspx" class="text-decoration-none">
                    <div class="card shadow-sm border-0 rounded-4 bg-white text-dark border-start border-4 border-primary text-center p-4 hover-zoom h-100">
                        <h5 class="fw-bold m-0">👥 Directorio de Clientes</h5>
                    </div>
                </a>
            </div>

        </div>

        <asp:Panel ID="pnlAdministrador" runat="server">
            
            <h4 class="fw-semibold text-secondary mb-3 border-top pt-4">Administración del Sistema</h4>
            
            <div class="row g-3 mb-4">
                <div class="col-md-4">
                    <a href="Usuarios.aspx" class="btn btn-dark w-100 py-3 fw-bold shadow-sm">Gestión de Usuarios</a>
                </div>
                <div class="col-md-4">
                    <a href="Reportes.aspx" class="btn btn-dark w-100 py-3 fw-bold shadow-sm">Reportes y Estadísticas</a>
                </div>
                <div class="col-md-4">
                    <a href="AvisosVencimiento.aspx" class="btn btn-outline-danger w-100 py-3 fw-bold shadow-sm">Ejecutar Avisos de Vencimiento</a>
                </div>
            </div>

            <h5 class="fw-semibold text-secondary mb-3">Configuración de Reglas y Tablas</h5>
            <div class="row g-3">
                <div class="col-md-3">
                    <a href="Productos.aspx" class="btn btn-outline-secondary w-100 py-2 fw-semibold">Productos/Líneas</a>
                </div>
                <div class="col-md-3">
                    <a href="TasasInteres.aspx" class="btn btn-outline-secondary w-100 py-2 fw-semibold">Tasas de Interés</a>
                </div>
                <div class="col-md-3">
                    <a href="MetodosPago.aspx" class="btn btn-outline-secondary w-100 py-2 fw-semibold">Métodos de Pago</a>
                </div>
                <%--<div class="col-md-3">
                    <a href="Roles.aspx" class="btn btn-outline-secondary w-100 py-2 fw-semibold">Ver Roles</a>
                </div>--%>
                <div class="col-md-3">
                    <a href="EstadosPrestamo.aspx" class="btn btn-outline-secondary w-100 py-2 fw-semibold">Ver Estados (Préstamos)</a>
                </div>
                <div class="col-md-3">
                    <a href="EstadosCuota.aspx" class="btn btn-outline-secondary w-100 py-2 fw-semibold">Ver Estados (Cuotas)</a>
                </div>
            </div>

        </asp:Panel>

    </div>

</asp:Content>