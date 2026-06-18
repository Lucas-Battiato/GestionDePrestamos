<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="TasasInteres.aspx.cs" Inherits="GestionDePestamos.Empleados.TasasInteres" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4 align-items-center">
            <div class="col">
                <h2 class="fw-bold text-dark">Gestión de Tasas de Interés</h2>
                <p class="text-muted">Administrá los tramos de tasas mensuales por producto y cantidad de cuotas.</p>
            </div>
            <div class="col-auto">
                <asp:Button ID="btnNuevo" runat="server" Text="+ Nueva Tasa"
                    CssClass="btn btn-success fw-bold shadow-sm"
                    OnClick="btnNuevo_Click" />
            </div>
        </div>

        <%-- FILTRO POR PRODUCTO --%>
        <div class="card shadow-sm border-0 rounded-4 mb-4">
            <div class="card-body p-3 d-flex align-items-center gap-3">
                <label class="fw-semibold text-secondary mb-0 text-nowrap">Filtrar por producto:</label>
                <asp:DropDownList ID="ddlFiltroProducto" runat="server"
                    CssClass="form-select"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlFiltroProducto_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>

        <%-- GRILLA --%>
        <div class="card shadow-sm border-0 rounded-4">
            <div class="card-body p-4 table-responsive">

                <asp:GridView ID="dgvTasas" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    DataKeyNames="IdTasaInteres">
                    <HeaderStyle CssClass="table-light text-secondary" />
                    <Columns>
                        <asp:BoundField DataField="IdTasaInteres" HeaderText="ID" Visible="false" />

                        <asp:TemplateField HeaderText="Producto">
                            <ItemTemplate>
                                <%# Eval("ProductoPrestamo.Nombre") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="CuotasDesde"  HeaderText="Cuotas Desde" />
                        <asp:BoundField DataField="CuotasHasta"  HeaderText="Cuotas Hasta" />

                        <asp:TemplateField HeaderText="Tasa Mensual">
                            <ItemTemplate>
                                <%# string.Format("{0:P2}", Eval("TasaMensual")) %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    OnClick="btnEditar_Click">Editar</asp:LinkButton>
                                <asp:LinkButton ID="btnEliminar" runat="server"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    OnClick="btnEliminar_Click">Eliminar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info text-center m-0">No hay tasas cargadas para este producto.</div>
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>

    <%-- MODAL AGREGAR / EDITAR TASA --%>
    <div class="modal fade" id="modalTasa" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content rounded-4">

                <div class="modal-header bg-dark text-white rounded-top-4">
                    <h5 class="modal-title fw-semibold">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Nueva Tasa" />
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body p-4">

                    <asp:HiddenField ID="hfIdTasa" runat="server" Value="0" />

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Producto</label>
                        <asp:DropDownList ID="ddlProducto" runat="server" CssClass="form-select bg-light" />
                    </div>

                    <div class="row g-3">
                        <div class="col-6">
                            <label class="form-label fw-semibold text-secondary">Cuotas Desde</label>
                            <asp:TextBox ID="txtCuotasDesde" runat="server"
                                CssClass="form-control bg-light"
                                placeholder="Ej: 3" />
                        </div>
                        <div class="col-6">
                            <label class="form-label fw-semibold text-secondary">Cuotas Hasta</label>
                            <asp:TextBox ID="txtCuotasHasta" runat="server"
                                CssClass="form-control bg-light"
                                placeholder="Ej: 6" />
                        </div>
                        <div class="col-12">
                            <label class="form-label fw-semibold text-secondary">Tasa Mensual</label>
                            <div class="input-group">
                                <asp:TextBox ID="txtTasaMensual" runat="server"
                                    CssClass="form-control bg-light"
                                    placeholder="Ej: 0.0450" />
                                <span class="input-group-text bg-light text-secondary">decimal</span>
                            </div>
                            <div class="form-text">Ingresá el valor decimal. Ej: 4,5% → <strong>0.0450</strong></div>
                        </div>
                    </div>

                    <asp:Label ID="lblError" runat="server" Visible="false"
                        CssClass="alert alert-danger d-block mt-3" />

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGuardar" runat="server" Text="Guardar"
                        CssClass="btn btn-success fw-bold"
                        OnClick="btnGuardar_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>
