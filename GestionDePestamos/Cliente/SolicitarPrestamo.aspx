<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cliente.Master" AutoEventWireup="true" CodeBehind="SolicitarPrestamo.aspx.cs" Inherits="GestionDePestamos.Cliente.SolicitarPrestamo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-primary">Solicitar Nuevo Préstamo</h2>

            </div>
        </div>

        <div class="row g-4">

            <div class="col-md-6">
                <div class="card shadow-sm border-0 rounded-4 p-4 h-100">
                    <div class="d-flex align-items-center mb-4">
                        <span class="badge bg-primary rounded-circle me-2 px-3 py-2 fs-6">1</span>
                        <h4 class="m-0 fw-bold text-dark">Configurá tu préstamo</h4>
                    </div>

                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Línea de Préstamo</label>
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select form-select-lg bg-light text-dark" OnSelectedIndexChanged="ddlProducto_SelectedIndexChanged" AutoPostBack="true">
                            <%--<asp:ListItem Value="0">-- Seleccioná un producto --</asp:ListItem>
                            <asp:ListItem Value="1">Préstamo Personal Tradicional</asp:ListItem>
                            <asp:ListItem Value="2">Crédito Universitario UTN</asp:ListItem>
                            <asp:ListItem Value="3">Microcrédito Emprendedor</asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>

                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Monto Solicitado</label>
                        <div class="input-group input-group-lg">
                            <span class="input-group-text bg-light text-secondary fw-bold">$</span>
                            <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control bg-light text-dark" placeholder="Ej: 150000"></asp:TextBox>
                        </div>
                    </div>

                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Plazo de Devolución</label>
                        <asp:DropDownList ID="ddlCuotas" runat="server" CssClass="form-select form-select-lg bg-light text-dark">
                            <%--<asp:ListItem Value="3">3 Cuotas fijas</asp:ListItem>
                            <asp:ListItem Value="6">6 Cuotas fijas</asp:ListItem>
                            <asp:ListItem Value="12">12 Cuotas fijas</asp:ListItem>
                            <asp:ListItem Value="18">18 Cuotas fijas</asp:ListItem>--%>
                        </asp:DropDownList>
                    </div>

                    <asp:Button ID="btnCalcular" runat="server" Text="Simular Cuotas" CssClass="btn btn-outline-primary btn-lg w-100 fw-bold mt-2 shadow-sm" OnClick="btnCalcular_Click" />

                </div>
            </div>

            <div class="col-md-6">
                <div class="card shadow-sm border-0 rounded-4 p-4 h-100 bg-white">
                    <div class="d-flex align-items-center mb-4">
                        <span class="badge bg-dark rounded-circle me-2 px-3 py-2 fs-6">2</span>
                        <h4 class="m-0 fw-bold text-dark">Resumen de tu Plan</h4>
                    </div>

                    <div class="rounded-4 p-4 bg-light border border-light-subtle">

                        <div class="d-flex justify-content-between align-items-center mb-3 pb-2 border-bottom">
                            <span class="text-secondary fw-medium">Monto del préstamo</span>
                            <asp:Label ID="lblMontoResumen" runat="server" CssClass="fw-bold text-dark" Text="$ 0,00"></asp:Label>
                        </div>

                        <div class="d-flex justify-content-between align-items-center mb-3 pb-2 border-bottom">
                            <span class="text-secondary fw-medium">Interés total</span>
                            <asp:Label ID="lblInteresResumen" runat="server" CssClass="fw-bold text-danger" Text="$ 0,00"></asp:Label>
                        </div>

                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <span class="text-secondary fw-medium">Costo por cuota (<asp:Label ID="lblCantCuotasResumen" runat="server" Text="0"></asp:Label>
                                pagos)</span>
                            <asp:Label ID="lblValorCuota" runat="server" CssClass="fw-bold text-dark" Text="$ 0,00"></asp:Label>
                        </div>

                        <div class="pt-3 border-top border-dark-subtle d-flex justify-content-between align-items-center">
                            <span class="fs-5 fw-bold text-dark">Total a devolver</span>
                            <asp:Label ID="lblTotalDevolver" runat="server" CssClass="fs-4 fw-bold text-success" Text="$ 0,00"></asp:Label>
                        </div>

                    </div>

                    <div class="mt-auto pt-4">
                        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar y Solicitar Préstamo"
                            CssClass="btn btn-success btn-lg w-100 fw-bold shadow"
                            Visible="false" OnClick="btnConfirmar_Click" />
                    </div>

                </div>
            </div>

        </div>

    </div>

    <%-- MODAL PARA CUANDO NO SE INGRESA NINGUN MONTO --%>
    <div class="modal fade" id="modalError" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title">Atención</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>
                <div class="modal-body">
                    <asp:Label ID="modalBody" runat="server" Text="Label"></asp:Label>
                    <%--El monto no puede estar vacío.--%>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-bs-dismiss="modal">Aceptar</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
