<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" Inherits="GestionDePestamos.Empleados.Usuarios" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4 align-items-center">
            <div class="col">
                <h2 class="fw-bold text-dark">Gestión de Usuarios</h2>
                <p class="text-muted">Listado de usuarios del sistema.</p>
            </div>
            <div class="col-auto">
                <asp:Button ID="btnNuevo" runat="server" Text="+ Nuevo Usuario"
                    CssClass="btn btn-success fw-bold shadow-sm"
                    OnClick="btnNuevo_Click" />
            </div>
        </div>

        <div class="card shadow-sm border-0 rounded-4 mb-4">
            <div class="card-body p-3">
                <asp:TextBox ID="txtFiltro" runat="server"
                    CssClass="form-control"
                    placeholder="Buscar por ID, usuario, rol o estado..."
                    AutoPostBack="true"
                    OnTextChanged="txtFiltro_TextChanged" />
            </div>
        </div>

        <div class="card shadow-sm border-0 rounded-4">
            <div class="card-body p-4 table-responsive">

                <asp:GridView ID="dgvUsuarios" runat="server"
                    CssClass="table table-hover align-middle"
                    Style="table-layout: fixed;"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    DataKeyNames="IdUsuario">
                    <HeaderStyle CssClass="table-light text-secondary" />
                    <Columns>
                        <asp:BoundField DataField="IdUsuario" HeaderText="ID" />
                        <asp:BoundField DataField="Username" HeaderText="Usuario" />
                        <asp:BoundField DataField="Rol" HeaderText="Rol" />

                        <asp:TemplateField HeaderText="Estado">
                            <ItemTemplate>
                                <span class='<%# "badge " + Eval("EstadoCssClass") %>'>
                                    <%# Eval("Estado") %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="180px">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEditar" runat="server"
                                    CssClass="btn btn-sm btn-outline-primary me-1"
                                    OnClick="btnEditar_Click">Editar</asp:LinkButton>
                                <asp:LinkButton ID="btnToggleActivo" runat="server"
                                    CssClass='<%# Eval("TogglecCssClass") %>'
                                    OnClick="btnToggleActivo_Click">
                                    <%# Eval("TextoBotonToggle") %>
                                </asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info text-center m-0">No se encontraron usuarios.</div>
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>


    <div class="modal fade" id="modalUsuario" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content rounded-4">

                <div class="modal-header bg-dark text-white rounded-top-4">
                    <h5 class="modal-title fw-semibold">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="" />
                    </h5>
                    <button type="button" class="btn-close btn-close-white" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body p-4">

                    <asp:HiddenField ID="hfIdUsuario" runat="server" Value="" />

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Nombre de usuario</label>
                        <asp:TextBox ID="txtUsername" runat="server"
                            CssClass="form-control bg-light"
                            placeholder="Ej: juan.perez" />
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server"
                            TextMode="Password"
                            CssClass="form-control bg-light"
                            placeholder="Ingresá una contraseña"
                            autocomplete="new-password"/>
                        <div class="form-text">
                            <asp:Label ID="lblContaseña" runat="server" Text=""></asp:Label>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Rol</label>
                        <asp:DropDownList ID="ddlRol" runat="server" CssClass="form-select bg-light" />
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
