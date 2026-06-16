<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cliente.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="GestionDePestamos.Cliente.Clientes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">
        
        <div class="row mb-5">
            <div class="col-12">
                <h2 class="fw-bold text-dark">¡Hola, <asp:Label ID="lblNombreCliente" runat="server" Text="(Nombre Cliente)"></asp:Label>! 👋</h2> 
                <p class="text-muted fs-5">Este es el resumen de tu cuenta al día de hoy.</p>
            </div>
        </div>

        <div class="row g-4 mb-5">
            
            <div class="col-md-6">
                <div class="card shadow-sm border-0 rounded-4 h-100 bg-primary text-white p-3">
                    <div class="card-body">
                        <h5 class="card-title fw-normal mb-3">Próximo Vencimiento</h5>
                        <h3 class="fw-bold mb-1">
                            <asp:Label ID="lblMontoProximo" runat="server" Text="$ 0,00"></asp:Label>
                        </h3>
                        <p class="mb-0">Vence el: <asp:Label ID="lblFechaProximo" runat="server" Text="--/--/----"></asp:Label></p>
                    </div>
                </div>
            </div>

            <div class="col-md-6">
                <div class="card shadow-sm border-0 rounded-4 h-100 bg-white p-3 border-start border-4 border-success">
                    <div class="card-body text-dark">
                        <h5 class="card-title fw-semibold text-secondary mb-3">Préstamos Activos</h5>
                        <h3 class="fw-bold text-dark mb-1">
                            <asp:Label ID="lblCantidadPrestamos" runat="server" Text="0"></asp:Label>
                        </h3>
                        <p class="text-muted mb-0">Al día con tus pagos.</p>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
            <div class="col-12 mb-3">
                <h4 class="fw-bold text-dark">¿Qué querés hacer hoy?</h4>
            </div>
            
            <div class="col-md-6 mb-3">
                <a href="SolicitarPrestamo.aspx" class="text-decoration-none">
                    <div class="card shadow-sm border-0 rounded-4 bg-light text-center p-4 hover-zoom">
                        <h5 class="fw-bold text-primary m-0">+ Solicitar Nuevo Préstamo</h5>
                    </div>
                </a>
            </div>

            <div class="col-md-6 mb-3">
                <a href="MisPrestamos.aspx" class="text-decoration-none">
                    <div class="card shadow-sm border-0 rounded-4 bg-light text-center p-4 hover-zoom">
                        <h5 class="fw-bold text-secondary m-0">📄 Ver mis Préstamos y Cuotas</h5>
                    </div>
                </a>
            </div>
        </div>

    </div>

</asp:Content>