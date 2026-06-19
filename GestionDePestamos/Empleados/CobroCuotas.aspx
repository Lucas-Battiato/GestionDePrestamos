<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="CobroCuotas.aspx.cs" Inherits="GestionDePestamos.Empleados.CobroCuotas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Registrar Cobros</h2>
                <p class="text-muted">Buscá un préstamo en curso por ID o por usuario del cliente para gestionar sus cuotas.</p>
            </div>
        </div>

        <%-- BUSCADOR --%>
        <div class="card shadow-sm border-0 rounded-4 mb-4">
            <div class="card-body p-4">
                <div class="row g-2 align-items-end">
                    <div class="col-md-8">
                        <label class="form-label fw-semibold text-secondary">ID de Préstamo o Usuario del Cliente</label>
                        <asp:TextBox ID="txtBusqueda" runat="server"
                            CssClass="form-control form-control-lg bg-light"
                            placeholder="Ej: 4  ó  juan.perez" />
                    </div>
                    <div class="col-md-4">
                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                            CssClass="btn btn-primary btn-lg w-100 fw-bold shadow-sm"
                            OnClick="btnBuscar_Click" />
                    </div>
                </div>

                <asp:Label ID="lblSinResultados" runat="server" Visible="false"
                    CssClass="alert alert-warning d-block mt-3 mb-0"
                    Text="No se encontró ningún préstamo en curso con ese criterio." />
            </div>
        </div>

        <%-- RESULTADO: DATOS DEL PRESTAMO --%>
        <asp:Panel ID="pnlPrestamo" runat="server" Visible="false">

            <div class="card shadow-sm border-0 rounded-4 mb-4">
                <div class="card-header bg-primary text-white rounded-top-4 py-3 d-flex justify-content-between align-items-center">
                    <h5 class="m-0 fw-semibold">
                        Préstamo #<asp:Label ID="lblIdPrestamo" runat="server" />
                    </h5>
                    <span class="badge bg-light text-primary fw-semibold">
                        <asp:Label ID="lblEstadoPrestamo" Text="En Curso" runat="server" />
                    </span>
                </div>
                <div class="card-body p-4">
                    <div class="row g-3">
                        <div class="col-md-3">
                            <div class="text-secondary small fw-semibold">Cliente</div>
                            <div class="fw-bold"><asp:Label ID="lblCliente" runat="server" /></div>
                        </div>
                        <div class="col-md-3">
                            <div class="text-secondary small fw-semibold">Producto</div>
                            <div class="fw-bold"><asp:Label ID="lblProducto" runat="server" /></div>
                        </div>
                        <div class="col-md-3">
                            <div class="text-secondary small fw-semibold">Monto Total</div>
                            <div class="fw-bold"><asp:Label ID="lblMontoTotal" runat="server" /></div>
                        </div>
                        <div class="col-md-3">
                            <div class="text-secondary small fw-semibold">Cuotas Restantes</div>
                            <div class="fw-bold"><asp:Label ID="lblCuotasRestantes" runat="server" /></div>
                        </div>
                    </div>
                </div>
            </div>

            <%-- GRILLA DE CUOTAS --%>
            <div class="card shadow-sm border-0 rounded-4">
                <div class="card-body p-4 table-responsive">

                    <asp:GridView ID="dgvCuotas" runat="server"
                        CssClass="table table-hover align-middle"
                        AutoGenerateColumns="False"
                        GridLines="None"
                        DataKeyNames="IdCuota">
                        <HeaderStyle CssClass="table-light text-secondary" />
                        <Columns>
                            <asp:BoundField DataField="IdCuota" HeaderText="ID" />
                            <asp:BoundField DataField="NumeroCuota" HeaderText="N° Cuota" />
                            <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C2}" />
                            <asp:BoundField DataField="FechaVencimiento" HeaderText="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />

                            <asp:TemplateField HeaderText="Estado">
                                <ItemTemplate>
                                    <span class='<%# "badge " + Eval("EstadoCssClass") %>'>
                                        <%# Eval("EstadoDescripcion") %>
                                    </span>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnRegistrarPago" runat="server"
                                        CssClass="btn btn-sm btn-success"
                                        Visible='<%# (bool)Eval("PuedeRegistrarPago") %>'
                                        OnClick="btnRegistrarPago_Click">Registrar Pago</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div class="alert alert-info text-center m-0">Este préstamo no tiene cuotas generadas.</div>
                        </EmptyDataTemplate>
                    </asp:GridView>

                </div>
            </div>

        </asp:Panel>

    </div>

    <%-- MODAL REGISTRAR PAGO --%>
    <div class="modal fade" id="modalPago" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content rounded-4">

                <div class="modal-header bg-success text-white rounded-top-4">
                    <h5 class="modal-title fw-semibold">Registrar Pago de Cuota</h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body p-4">

                    <asp:HiddenField ID="hfIdCuota" runat="server" Value="0" />

                    <p class="text-secondary">
                        Cuota N° <strong><asp:Label ID="lblModalNumeroCuota" runat="server" /></strong>
                        por <strong><asp:Label ID="lblModalMontoCuota" runat="server" /></strong>
                    </p>

                    <div class="mb-2">
                        <label class="form-label fw-semibold text-secondary">Método de Pago</label>
                        <asp:DropDownList ID="ddlMetodoPago" runat="server" CssClass="form-select bg-light" />
                    </div>

                    <asp:Label ID="lblErrorPago" runat="server" Visible="false"
                        CssClass="alert alert-danger d-block mt-3" />

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarPago" runat="server" Text="Confirmar Pago"
                        CssClass="btn btn-success fw-bold"
                        OnClick="btnConfirmarPago_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
