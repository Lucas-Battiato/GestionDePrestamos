<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="GestionClientes.aspx.cs" Inherits="GestionDePestamos.Empleados.GestionClientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Directorio de Clientes</h2>
                <p class="text-muted">Listado de clientes registrados en el sistema.</p>
            </div>
        </div>

        <%--<div class="card shadow-sm border-0 rounded-4 mb-4">
            <div class="card-body p-3 d-flex align-items-center gap-3">
                <label class="fw-semibold text-secondary mb-0 text-nowrap">Buscar por usuario:</label>
                <asp:TextBox ID="txtFiltro" runat="server"
                    CssClass="form-control"
                    placeholder="Ej: juan.perez" />
                <asp:Button ID="btnBuscar" runat="server" Text="Buscar"
                    CssClass="btn btn-primary fw-bold px-4"
                    OnClick="btnBuscar_Click" />
                <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar"
                    CssClass="btn btn-outline-secondary"
                    OnClick="btnLimpiar_Click" />
            </div>
        </div>--%>

        <div class="card shadow-sm border-0 rounded-4">
            <div class="card-body p-4 table-responsive">

                <asp:GridView ID="dgvClientes" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    DataKeyNames="IdCliente">
                    <HeaderStyle CssClass="table-light text-secondary" />
                    <Columns>
                        <asp:BoundField DataField="IdCliente" HeaderText="ID" />
                        <asp:BoundField DataField="Username"  HeaderText="Usuario" />
                        <asp:BoundField DataField="Email"     HeaderText="Email" />
                        <asp:BoundField DataField="Telefono"  HeaderText="Teléfono" />
                        <asp:BoundField DataField="Direccion" HeaderText="Dirección" />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info text-center m-0">No se encontraron clientes.</div>
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>

</asp:Content>
