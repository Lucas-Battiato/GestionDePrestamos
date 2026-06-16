<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="Roles.aspx.cs" Inherits="GestionDePestamos.Empleados.Roles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">
        
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Roles del Sistema</h2>
                <p class="text-muted">Listado de los perfiles y niveles de acceso habilitados en la plataforma. <span class="badge bg-warning text-dark">Solo lectura</span></p>
            </div>
        </div>

        <div class="row">
            <div class="col-12">
                <div class="card shadow-sm border-0 rounded-4 h-100">
                    <div class="card-body p-4 table-responsive">
                        
                        <asp:GridView ID="dgvRoles" runat="server" CssClass="table table-hover align-middle border" 
                                      AutoGenerateColumns="False" GridLines="None">
                            <HeaderStyle CssClass="table-dark text-white" />
                            <Columns>
                                <asp:BoundField DataField="IdRol" HeaderText="ID" ItemStyle-Width="80px" />
                                <asp:BoundField DataField="Descripcion" HeaderText="Descripción del Rol" />
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="alert alert-info text-center m-0">No se encontraron roles configurados en la base de datos.</div>
                            </EmptyDataTemplate>
                        </asp:GridView>

                    </div>
                </div>
            </div>
        </div>

    </div>

</asp:Content>
