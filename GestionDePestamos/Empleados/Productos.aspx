<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="GestionDePestamos.Empleados.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Gestión de Productos</h2>
                <p class="text-muted">Listado de líneas de préstamo disponibles para los clientes.</p>
            </div>
        </div>

        <div class="card shadow-sm border-0 rounded-4">
            <div class="card-body p-4 table-responsive">

                <asp:GridView ID="dgvProductos" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    DataKeyNames="IdProducto">
                    <HeaderStyle CssClass="table-light text-secondary" />
                    <Columns>
                        <asp:BoundField DataField="IdProducto"    HeaderText="ID" />
                        <asp:BoundField DataField="Nombre"        HeaderText="Nombre" />
                        <asp:BoundField DataField="Descripcion"   HeaderText="Descripción" />
                        <asp:BoundField DataField="MontoMinimo"   HeaderText="Monto Mín."  DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="MontoMaximo"   HeaderText="Monto Máx."  DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="CuotasMinimas" HeaderText="Cuotas Mín." />
                        <asp:BoundField DataField="CuotasMaximas" HeaderText="Cuotas Máx." />
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info text-center m-0">No hay productos cargados.</div>
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>

</asp:Content>
