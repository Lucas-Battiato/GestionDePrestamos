<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Cliente.Master" AutoEventWireup="true" CodeBehind="MisPrestamos.aspx.cs" Inherits="GestionDePestamos.Cliente.MisPrestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="my-5">

        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-primary">Mis Préstamos</h2>
                <p class="text-muted">Acá podés ver el estado de tus solicitudes y acceder al detalle de tus cuotas.</p>
            </div>
        </div>

        <%-- ACCORDION --%>
        <div class="accordion" id="accordionPrestamos">
            <asp:Repeater ID="rptPrestamos" runat="server" OnItemDataBound="rptPrestamos_ItemDataBound">
                <ItemTemplate>

                    <div class="accordion-item border-0 shadow-sm rounded-4 mb-3">

                        <%-- HEADER DEL ACCORDION --%>
                        <h2 class="accordion-header">
                            <button class="accordion-button collapsed rounded-4 fw-semibold"
                                type="button"
                                data-bs-toggle="collapse"
                                data-bs-target='<%# "#collapse" + Eval("IdPrestamo") %>'
                                aria-expanded="false">

                                <div class="d-flex align-items-center gap-3 w-100 me-3">
                                    <span class="fw-bold">Préstamo #<%# Eval("IdPrestamo") %></span>
                                    <span class='<%# "badge " + Eval("EstadoCssClass") %>'>
                                        <%# Eval("EstadoDescripcion") %>
                                    </span>
                                    <span class="text-secondary small ms-auto">
                                        <%# Eval("NombreProducto") %> &nbsp;|&nbsp;
                                        $<%# Eval("Monto") %> &nbsp;|&nbsp;
                                        <%# Eval("CuotasRestantes") %> cuotas restantes
                                    </span>
                                </div>

                            </button>
                        </h2>

                        <%-- CONTENIDO DESPLEGABLE --%>
                        <div id='<%# "collapse" + Eval("IdPrestamo") %>'
                            class="accordion-collapse collapse"
                            data-bs-parent="#accordionPrestamos">
                            <div class="accordion-body p-0">

                                <div class="table-responsive">
                                    <asp:GridView ID="dgvCuotas" runat="server"
                                        CssClass="table table-hover align-middle mb-0"
                                        AutoGenerateColumns="False"
                                        GridLines="None">
                                        <HeaderStyle CssClass="table-light text-secondary" />
                                        <Columns>
                                            <asp:BoundField DataField="NumeroCuota"      HeaderText="N° Cuota" />
                                            <asp:BoundField DataField="Monto"            HeaderText="Monto"       DataFormatString="{0:C2}" />
                                            <asp:BoundField DataField="FechaVencimiento" HeaderText="Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="FechaPago"        HeaderText="Fecha Pago"  DataFormatString="{0:dd/MM/yyyy}" NullDisplayText="-" />
                                            <asp:BoundField DataField="MetodoPago"       HeaderText="Método Pago" NullDisplayText="-" />

                                            <asp:TemplateField HeaderText="Estado">
                                                <ItemTemplate>
                                                    <span class='<%# "badge " + Eval("EstadoCssClass") %>'>
                                                        <%# Eval("EstadoDescripcion") %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div class="alert alert-info text-center m-3">Este préstamo no tiene cuotas generadas aún.</div>
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>
                    </div>

                </ItemTemplate>
            </asp:Repeater>
        </div>

        <%-- MENSAJE SI NO HAY PRESTAMOS --%>
        <asp:Panel ID="pnlSinPrestamos" runat="server" Visible="false">
            <div class="alert alert-info text-center">
                Todavía no tenés préstamos registrados.
            </div>
        </asp:Panel>

    </div>

</asp:Content>
