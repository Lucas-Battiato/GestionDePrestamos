<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="GestionDePestamos.Cliente.Registro" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Registro - PrestamoYa</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
    <link href="../Content/Style.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg-light">

    <form id="form1" runat="server">

        <div class="min-vh-100 d-flex justify-content-center align-items-center py-4">

            <div class="card shadow-lg border-0 rounded-4 tarjeta-registro" style="width: 400px";>

                <div class="card-header bg-cliente text-white text-center py-4 rounded-top-4">
                    <h3 class="m-0 fw-bold">Crear cuenta</h3>
                    <p class="m-0 mt-1 opacity-75 small">Completá tus datos para registrarte</p>
                </div>

                <div class="card-body p-5 text-dark">

                    <asp:Label ID="lblMensaje" runat="server" Visible="false"
                        CssClass="alert alert-danger d-block mb-3" />

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Nombre de usuario</label>
                        <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control form-control-lg bg-light text-dark"/>
                        <div style="min-height: 30px;">
                            <asp:Label ID="lblErrorUsername" runat="server" Text="" CssClass="text-danger small"></asp:Label>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg bg-light text-dark"/>
                        <div style="min-height: 30px;">
                            <asp:Label ID="lblErrorPassword" runat="server" Text="" CssClass="text-danger small"></asp:Label>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Correo electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control form-control-lg bg-light text-dark"/>
                        <div style="min-height: 30px;">
                            <asp:Label ID="lblErrorEmail" runat="server" Text="" CssClass="text-danger small"></asp:Label>
                        </div>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Teléfono</label>
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control form-control-lg bg-light text-dark"/>
                        <div style="min-height: 30px;">
                            <asp:Label ID="lblErrorTelefono" runat="server" Text="" CssClass="text-danger small"></asp:Label>
                        </div>
                    </div>

                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-lg bg-light text-dark"/>
                        <div style="min-height: 30px;">
                            <asp:Label ID="lblErrorDireccion" runat="server" Text="" CssClass="text-danger small"></asp:Label>
                        </div>
                    </div>

                    <asp:Button ID="btnRegistrar" runat="server" Text="Crear cuenta" CssClass="btn btn-iniciar w-100 fw-bold fs-5 py-2 shadow-sm" OnClick="btnRegistrar_Click" />

                    <div class="text-center mt-4">
                        <span class="text-secondary">¿Ya tenés cuenta?</span>
                        <a href="../Inicio.aspx" class="fw-bold link-registro ms-1">Ingresá acá</a>
                    </div>

                </div>
            </div>

        </div>

    </form>
</body>
</html>
