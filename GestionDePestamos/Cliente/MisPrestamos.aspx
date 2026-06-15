<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cliente.Master" AutoEventWireup="true" CodeBehind="MisPrestamos.aspx.cs" Inherits="GestionDePestamos.Cliente.MisPrestamos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <div class="container my-5">
        
        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-primary">Mis Préstamos</h2>
                <p class="text-muted">Acá podés ver el estado de tus solicitudes y acceder al detalle de tus cuotas.</p>
            </div>
        </div>

        <div class="card shadow-sm border-0 rounded-4">
            <div class="card-body p-4 table-responsive">
                
                <asp:GridView ID="dgvMisPrestamos" runat="server" CssClass="table table-hover align-middle" 
                              AutoGenerateColumns="False" GridLines="None" OnRowCommand="dgvMisPrestamos_RowCommand">
                    
                    <HeaderStyle CssClass="table-light text-secondary" />
                    
                    <Columns>
                        <asp:BoundField DataField="ProductoPrestamo.Nombre" HeaderText="Tipo de Préstamo" />
                        <asp:BoundField DataField="Monto" HeaderText="Monto Solicitado" DataFormatString="{0:C}" />
                        <asp:BoundField DataField="CantidadCuotas" HeaderText="Cuotas" />
                        <asp:BoundField DataField="EstadoPrestamo.Descripcion" HeaderText="Estado" />
                        <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha de Aprobación" DataFormatString="{0:dd/MM/yyyy}" />
                        
                        <%-- Columna con el botón para ver el detalle --%>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:Button ID="btnVerDetalle" runat="server" Text="Ver Cuotas" 
                                            CssClass="btn btn-sm btn-outline-primary fw-bold px-3" 
                                            CommandName="VerDetalle" CommandArgument='<%# Eval("IdPrestamo") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                    <EmptyDataTemplate>
                        <div class="alert alert-info text-center m-0 fw-semibold">
                            Todavía no tenés ningún préstamo registrado en el sistema. ¡Animate a solicitar uno!
                        </div>
                    </EmptyDataTemplate>

                </asp:GridView>

            </div>
        </div>
    </div>

</asp:Content>

