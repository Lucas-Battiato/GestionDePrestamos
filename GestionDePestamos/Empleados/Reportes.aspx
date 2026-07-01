<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Empleado.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="GestionDePestamos.Empleados.Reportes" %>

<asp:Content ID="Content3" ContentPlaceHolderID="head" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="my-5 px-4">

        <div class="row mb-4 align-items-center">
            <div class="col">
                <h2 class="fw-bold text-dark">Reportes y Estadísticas</h2>
                <p class="text-muted">Préstamos activos &nbsp;|&nbsp; Cuotas vencidas &nbsp;|&nbsp; Balance</p>
            </div>
        </div>

        <div class="row g-3 mb-4">
            <div class="col-md-3">
                <div class="card border-0 rounded-4 shadow-sm h-100 p-4">
                    <div class="text-secondary small fw-semibold mb-1">Préstamos en curso</div>
                    <div class="fs-2 fw-bold text-primary">
                        <asp:Label ID="lblKpiPrestamosActivos" runat="server" Text="0" />
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card border-0 rounded-4 shadow-sm h-100 p-4">
                    <div class="text-secondary small fw-semibold mb-1">Cuotas vencidas (impagas)</div>
                    <div class="fs-2 fw-bold text-danger">
                        <asp:Label ID="lblKpiCuotasVencidas" runat="server" Text="0" />
                    </div>
                </div>
            </div>
            <div class="col-md-6">
                <div class="card border-0 rounded-4 shadow-sm h-100 p-4">
                    <div class="text-secondary small fw-semibold mb-1">Balance (cobrado vs perdido)</div>
                    <div class="fs-2 fw-bold">
                        <asp:Label ID="lblKpiBalance" runat="server" Text="$ 0,00" CssClass="text-success" />
                    </div>
                    <div class="text-secondary small mt-1">
                        <asp:Label ID="lblKpiBalanceDetalle" runat="server" Text="" CssClass="text-nowrap" />
                    </div>
                </div>
            </div>
        </div>

        <div class="card border-0 rounded-4 shadow-sm">
            <div class="card-header bg-white rounded-top-4 pt-3 pb-0">
                <ul class="nav nav-tabs border-0" id="tabsReportes" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active fw-semibold" data-bs-toggle="tab" data-bs-target="#tabActivos" type="button">
                            Préstamos Activos
                        </button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link fw-semibold" data-bs-toggle="tab" data-bs-target="#tabVencidas" type="button">
                            Cuotas Vencidas
                        </button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link fw-semibold" data-bs-toggle="tab" data-bs-target="#tabBalance" type="button">
                            Balance
                        </button>
                    </li>
                </ul>
            </div>

            <div class="card-body p-4">
                <div class="tab-content">

                    <div class="tab-pane fade show active" id="tabActivos" role="tabpanel">
                        <div class="table-responsive">
                            <asp:GridView ID="dgvPrestamosActivos" runat="server"
                                CssClass="table table-hover align-middle"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                DataKeyNames="IdPrestamo">
                                <HeaderStyle CssClass="table-light text-secondary" />
                                <Columns>
                                    <asp:BoundField DataField="IdPrestamo"      HeaderText="ID" />
                                    <asp:BoundField DataField="UsernameCliente" HeaderText="Cliente" />
                                    <asp:BoundField DataField="NombreProducto"  HeaderText="Producto" />
                                    <asp:BoundField DataField="Monto"           HeaderText="Monto"            DataFormatString="{0:C2}" />
                                    <asp:BoundField DataField="InteresTotal"    HeaderText="Interés Total"    DataFormatString="{0:C2}" />
                                    <asp:BoundField DataField="CuotasRestantes" HeaderText="Cuotas Restantes" />
                                    <asp:BoundField DataField="FechaAprobacion" HeaderText="Fecha Aprobación" DataFormatString="{0:dd/MM/yyyy}" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-0">No hay préstamos activos.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="tab-pane fade" id="tabVencidas" role="tabpanel">
                        <div class="d-flex justify-content-end mb-3">
                            <asp:Button ID="btnNotificarVencidas" runat="server"
                                Text="📧 Notificar a clientes con cuotas vencidas"
                                CssClass="btn btn-warning fw-semibold"
                                OnClick="btnNotificarVencidas_Click"
                                Visible="false" />
                        </div>
                        <asp:Label ID="lblResultadoNotificacion" runat="server" Visible="false"
                            CssClass="alert alert-success d-block mb-3" />
                        <div class="table-responsive">
                            <asp:GridView ID="dgvCuotasVencidas" runat="server"
                                CssClass="table table-hover align-middle"
                                AutoGenerateColumns="False"
                                GridLines="None"
                                DataKeyNames="IdCuota">
                                <HeaderStyle CssClass="table-light text-secondary" />
                                <Columns>
                                    <asp:BoundField DataField="IdCuota"         HeaderText="ID Cuota" />
                                    <asp:BoundField DataField="IdPrestamo"       HeaderText="ID Préstamo" />
                                    <asp:BoundField DataField="UsernameCliente"  HeaderText="Cliente" />
                                    <asp:BoundField DataField="Monto"            HeaderText="Monto"             DataFormatString="{0:C2}" />
                                    <asp:BoundField DataField="FechaVencimiento" HeaderText="Fecha Vencimiento" DataFormatString="{0:dd/MM/yyyy}" />
                                    <asp:BoundField DataField="DiasVencida"      HeaderText="Días Vencida" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="alert alert-info text-center m-0">No hay cuotas vencidas.</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="tab-pane fade" id="tabBalance" role="tabpanel">

                        <asp:HiddenField ID="hfDatosPrestamos" runat="server" Value="" />
                        <asp:HiddenField ID="hfDatosCuotas" runat="server" Value="" />
                        <asp:HiddenField ID="hfDatosBarras" runat="server" Value="" />

                        <div class="row g-4 mb-4">
                            <div class="col-md-6">
                                <div class="card border-0 rounded-4 shadow-sm p-4 h-100">
                                    <h6 class="fw-semibold text-secondary mb-3">Préstamos por Estado</h6>
                                    <canvas id="chartPrestamos" style="max-height: 280px;"></canvas>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="card border-0 rounded-4 shadow-sm p-4 h-100">
                                    <h6 class="fw-semibold text-secondary mb-3">Cuotas por Estado</h6>
                                    <canvas id="chartCuotas" style="max-height: 280px;"></canvas>
                                </div>
                            </div>
                        </div>

                        <div class="card border-0 rounded-4 shadow-sm p-4">
                            <h6 class="fw-semibold text-secondary mb-3">Préstamos por Producto y Estado</h6>
                            <canvas id="chartBarras" style="max-height: 320px;"></canvas>
                        </div>

                    </div>

                </div>
            </div>
        </div>

    </div>

    <script>
        document.addEventListener('DOMContentLoaded', function () {

            var datosPrestamos = JSON.parse(document.getElementById('<%= hfDatosPrestamos.ClientID %>').value || '{}');
            var datosCuotas = JSON.parse(document.getElementById('<%= hfDatosCuotas.ClientID %>').value || '{}');
            var datosBarras = JSON.parse(document.getElementById('<%= hfDatosBarras.ClientID %>').value || '{}');

            if (Object.keys(datosPrestamos).length > 0) {
                new Chart(document.getElementById('chartPrestamos'), {
                    type: 'doughnut',
                    data: {
                        labels: datosPrestamos.labels,
                        datasets: [{
                            data: datosPrestamos.data,
                            backgroundColor: ['#0d6efd', '#198754', '#dc3545', '#0dcaf0', '#ffc107', '#6c757d']
                        }]
                    },
                    options: { plugins: { legend: { position: 'bottom' } } }
                });
            }

            if (Object.keys(datosCuotas).length > 0) {
                new Chart(document.getElementById('chartCuotas'), {
                    type: 'doughnut',
                    data: {
                        labels: datosCuotas.labels,
                        datasets: [{
                            data: datosCuotas.data,
                            backgroundColor: ['#6c757d', '#198754', '#dc3545', '#212529']
                        }]
                    },
                    options: { plugins: { legend: { position: 'bottom' } } }
                });
            }

            if (Object.keys(datosBarras).length > 0) {
                new Chart(document.getElementById('chartBarras'), {
                    type: 'bar',
                    data: {
                        labels: datosBarras.labels,
                        datasets: datosBarras.datasets
                    },
                    options: {
                        plugins: { legend: { position: 'bottom' } },
                        responsive: true,
                        scales: { x: { stacked: true }, y: { stacked: true, beginAtZero: true } }
                    }
                });
            }

        });
    </script>

</asp:Content>
