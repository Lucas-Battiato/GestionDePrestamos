<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="Empleados.aspx.cs" Inherits="GestionDePestamos.Empleados.Empleados" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="my-5">

        <div class="mb-4">
            <h2 class="fw-bold text-dark">Panel de Gestión</h2>
            <p class="text-muted mb-0">
                <asp:Label ID="lblNombreUsuario" runat="server" CssClass="fw-semibold text-dark" />, bienvenido al sistema.
            </p>
            <p class="text-muted mb-0">
                Rol actual: <asp:Label ID="lblRolActual" runat="server" CssClass="badge bg-info text-dark" />
            </p>
        </div>


        <div class="row g-3 mb-5">
            <div class="col-md-4">
                <div class="card border-0 rounded-4 shadow-sm p-4 h-100 d-flex flex-column align-items-center justify-content-center text-center">
                    <div class="text-secondary fw-semibold mb-2" style="font-size: 0.95rem;">Solicitudes pendientes</div>
                    <div class="fw-bold text-warning" style="font-size: 3.5rem; line-height: 1;">
                        <asp:Label ID="lblKpiSolicitudes" runat="server" Text="0" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card border-0 rounded-4 shadow-sm p-4 h-100 d-flex flex-column align-items-center justify-content-center text-center">
                    <div class="text-secondary fw-semibold mb-2" style="font-size: 0.95rem;">Clientes registrados</div>
                    <div class="fw-bold text-primary" style="font-size: 3.5rem; line-height: 1;">
                        <asp:Label ID="lblKpiClientes" runat="server" Text="0" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="card border-0 rounded-4 shadow-sm p-4 h-100 d-flex flex-column align-items-center justify-content-center text-center">
                    <div class="text-secondary fw-semibold mb-2" style="font-size: 0.95rem;">Préstamos aprobados esta semana</div>
                    <div class="fw-bold text-success" style="font-size: 3.5rem; line-height: 1;">
                        <asp:Label ID="lblKpiAprobados" runat="server" Text="0" />
                    </div>
                </div>
            </div>
        </div>


        <p class="text-secondary fw-semibold text-uppercase mb-3" style="font-size: 0.75rem; letter-spacing: 0.08em;">Operaciones Diarias</p>
        <div class="row g-3 mb-4">
            <div class="col-md-4">
                <a href="GestionPrestamos.aspx" class="text-decoration-none">
                    <div class="card border-0 rounded-4 shadow-sm p-4 h-100 bg-primary text-white">
                        <div class="fs-2 mb-2">📋</div>
                        <div class="fw-bold fs-5">Evaluar Solicitudes</div>
                        <div class="small opacity-75 mt-1">Revisá y aprobá o rechazá los préstamos solicitados</div>
                    </div>
                </a>
            </div>
            <div class="col-md-4">
                <a href="CobroCuotas.aspx" class="text-decoration-none">
                    <div class="card border-0 rounded-4 shadow-sm p-4 h-100 bg-success text-white">
                        <div class="fs-2 mb-2">💳</div>
                        <div class="fw-bold fs-5">Registrar Cobros</div>
                        <div class="small opacity-75 mt-1">Registrá el pago de cuotas de clientes activos</div>
                    </div>
                </a>
            </div>
            <div class="col-md-4">
                <a href="GestionClientes.aspx" class="text-decoration-none">
                    <div class="card border-0 rounded-4 shadow-sm p-4 h-100 border border-2 text-dark">
                        <div class="fs-2 mb-2">👥</div>
                        <div class="fw-bold fs-5">Directorio de Clientes</div>
                        <div class="small text-muted mt-1">Consultá el listado de clientes registrados</div>
                    </div>
                </a>
            </div>
        </div>

        <br />
        <br />

        <asp:Panel ID="pnlAdministrador" runat="server" Visible="false">

            <p class="text-secondary fw-semibold text-uppercase mb-3" style="font-size: 0.75rem; letter-spacing: 0.08em;">Administración del Sistema</p>
            <div class="row g-3 mb-4">
                <div class="col-md-6">
                    <a href="Usuarios.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-4 h-100 bg-dark text-white">
                            <div class="fs-2 mb-2">🔐</div>
                            <div class="fw-bold fs-5">Gestión de Usuarios</div>
                            <div class="small opacity-75 mt-1">Creá, editá y activá/desactivá usuarios del sistema</div>
                        </div>
                    </a>
                </div>
                <div class="col-md-6">
                    <a href="Reportes.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-4 h-100 bg-dark text-white">
                            <div class="fs-2 mb-2">📊</div>
                            <div class="fw-bold fs-5">Reportes y Estadísticas</div>
                            <div class="small opacity-75 mt-1">Visualizá el estado del negocio y enviá avisos de vencimiento</div>
                        </div>
                    </a>
                </div>
            </div>

            <p class="text-secondary fw-semibold text-uppercase mb-3" style="font-size: 0.75rem; letter-spacing: 0.08em;">Configuración de Reglas y Tablas</p>
            <div class="row g-3">
                <div class="col-md-3">
                    <a href="Productos.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-3 text-center text-dark">
                            <div class="small fw-semibold">Productos/Líneas</div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a href="TasasInteres.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-3 text-center text-dark">
                            <div class="small fw-semibold">Tasas de Interés</div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a href="MetodosPago.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-3 text-center text-dark">
                            <div class="small fw-semibold">Métodos de Pago</div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a href="EstadosPrestamo.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-3 text-center text-dark">
                            <div class="small fw-semibold">Ver Estados (Préstamos)</div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a href="EstadosCuota.aspx" class="text-decoration-none">
                        <div class="card border-0 rounded-4 shadow-sm p-3 text-center text-dark">
                            <div class="small fw-semibold">Ver Estados (Cuotas)</div>
                        </div>
                    </a>
                </div>
            </div>

        </asp:Panel>

    </div>

</asp:Content>
