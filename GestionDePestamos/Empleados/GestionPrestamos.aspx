<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="GestionPrestamos.aspx.cs" Inherits="GestionDePestamos.Empleados.GestionPrestamos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="container my-5">

        <div class="row mb-4">
            <div class="col-12">
                <h2 class="fw-bold text-dark">Evaluar Solicitudes</h2>
                <p class="text-muted">Préstamos en estado <strong>Solicitado</strong> a la espera de aprobación o rechazo.</p>
            </div>
        </div>

        <div class="card shadow-sm border-0 rounded-4">
            <div class="card-body p-4 table-responsive">

                <asp:GridView ID="dgvSolicitudes" runat="server"
                    CssClass="table table-hover align-middle"
                    AutoGenerateColumns="False"
                    GridLines="None"
                    DataKeyNames="IdPrestamo">
                    <HeaderStyle CssClass="table-light text-secondary" />
                    <Columns>
                        <asp:BoundField DataField="IdPrestamo" HeaderText="ID" />
                        <asp:BoundField DataField="UsernameCliente" HeaderText="Cliente" />
                        <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                        <asp:BoundField DataField="FechaSolicitud" HeaderText="Fecha Solicitud" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField DataField="MontoSolicitado" HeaderText="Monto Solicitado" DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="MontoADevolver" HeaderText="Monto a Devolver" DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="GananciaEstimada" HeaderText="Ganancia Estimada" DataFormatString="{0:C2}" />
                        <asp:BoundField DataField="DetalleCuotas" HeaderText="Cuotas" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAprobar" runat="server"
                                    CssClass="btn btn-sm btn-success me-1"
                                    OnClick="btnAprobar_Click">Aprobar</asp:LinkButton>
                                <asp:LinkButton ID="btnRechazar" runat="server"
                                    CssClass="btn btn-sm btn-outline-danger"
                                    OnClick="btnRechazar_Click">Rechazar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div class="alert alert-info text-center m-0">No hay solicitudes pendientes de evaluación.</div>
                    </EmptyDataTemplate>
                </asp:GridView>

            </div>
        </div>

    </div>

    <%-- MODAL CONFIRMACION APROBAR / RECHAZAR --%>
    <div class="modal fade" id="modalDecision" tabindex="-1">
        <div class="modal-dialog">
            <div class="modal-content rounded-4">

                <div class="modal-header rounded-top-4">
                    <h5 class="modal-title fw-semibold">
                        <asp:Label ID="lblModalTitulo" runat="server" Text="Confirmar decisión" />
                    </h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal"></button>
                </div>

                <div class="modal-body p-4">

                    <asp:HiddenField ID="hfIdPrestamo" runat="server" Value="0" />
                    <asp:HiddenField ID="hfAccion" runat="server" Value="" />

                    <p class="text-secondary">
                        <asp:Label ID="lblModalMensaje" runat="server" Text="¿Estás seguro de que querés confirmar esta decisión?" />
                    </p>

                    <div class="mb-2">
                        <label class="form-label fw-semibold text-secondary">Observación <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtObservacion" runat="server"
                            CssClass="form-control bg-light"
                            TextMode="MultiLine" Rows="3"
                            placeholder="Ingresá el motivo de la decisión" />
                    </div>

                    <asp:Label ID="lblError" runat="server" Visible="false"
                        CssClass="alert alert-danger d-block mt-3" />

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnConfirmarDecision" runat="server" Text="Confirmar"
                        CssClass="btn btn-dark fw-bold"
                        OnClick="btnConfirmarDecision_Click" />
                </div>

            </div>
        </div>
    </div>

</asp:Content>