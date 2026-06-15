<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GestionDePestamos.Inicio" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ingreso - PrestamoYa</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js"></script>
    <link href="Content/Style.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg-light">

    <form id="form1" runat="server">

        <div class="vh-100 d-flex justify-content-center align-items-center">

            <div class="card shadow-lg border-0 rounded-4 tarjeta-login" style="width: 400px;">

                <div class="card-header bg-primary text-white text-center py-4 rounded-top-4">
                    <h3 class="m-0 fw-bold">Ingreso</h3>
                </div>

                <div class="card-body p-5 text-dark">

                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Nombre de Usuario</label>
                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-lg bg-light text-dark"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Contraseña</label>
                        <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg bg-light text-dark"></asp:TextBox>
                    </div>

                    <asp:Button ID="btnIngresar" runat="server" Text="Iniciar Sesión" CssClass="btn btn-primary w-100 fw-bold fs-5 py-2 mt-3 shadow-sm" OnClick="btnIngresar_Click" />

                    <div class="text-center mt-3">
                        <a href="Empleados/Empleados.aspx" class="text-muted"><small>Ir a vista Empleado (Test)</small></a>
                    </div>

                    <div class="text-center mt-4">
                        <span class="text-secondary">¿Sos cliente nuevo?</span>
                        <a href="Cliente/Registro.aspx" class="fw-bold link-registro">¡Registrate acá!</a>
                    </div>

                </div>
            </div>

        </div>

    </form>
</body>
</html>