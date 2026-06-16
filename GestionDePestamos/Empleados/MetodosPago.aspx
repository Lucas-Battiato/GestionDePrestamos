<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="MetodosPago.aspx.cs" Inherits="GestionDePestamos.Empleados.MetodosPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">
        
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Gestión de Métodos de Pago</h2>
                <p class="text-muted">Administrá las formas de pago habilitadas para el cobro de cuotas.</p>
            </div>
        </div>

        <div class="row g-4">
            
            <div class="col-md-4">
                <div class="card shadow-sm border-0 rounded-4">
                    <div class="card-header bg-dark text-white rounded-top-4 py-3">
                        <h5 class="m-0 fw-semibold">Nuevo Método</h5>
                    </div>
                    <div class="card-body p-4 bg-light">
                        <div class="mb-3">
                            <label class="form-label fw-bold text-secondary">Descripción</label>
                            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control bg-white" placeholder="Ej: Transferencia Bancaria"></asp:TextBox>
                        </div>
                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar Método" CssClass="btn btn-success w-100 fw-bold shadow-sm" OnClick="btnGuardar_Click" />
                    </div>
                </div>
            </div>

            <div class="col-md-8">
                <div class="card shadow-sm border-0 rounded-4 h-100">
                    <div class="card-body p-4 table-responsive">
                        
                        <asp:GridView ID="dgvMetodos" runat="server" CssClass="table table-hover align-middle" 
                                      AutoGenerateColumns="False" GridLines="None">
                            <HeaderStyle CssClass="table-light text-secondary" />
                            <Columns>
                                <asp:BoundField DataField="IdMetodo" HeaderText="ID" ItemStyle-Width="50px" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Método de Pago" />
                                
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditar" runat="server" CssClass="btn btn-sm btn-outline-primary me-1">Editar</asp:LinkButton>
                                        <asp:LinkButton ID="btnEliminar" runat="server" CssClass="btn btn-sm btn-outline-danger">Eliminar</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-info text-center m-0">No hay métodos de pago cargados.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>
                </div>
            </div>

        </div>

    </div>

</asp:Content>