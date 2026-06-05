<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginEmpleado.aspx.cs" Inherits="GestionDePestamos.Operador.LoginEmpleado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Login Empleados</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/Style.css" rel="stylesheet" type="text/css" />
</head>
<body class="bg-light">

    <form id="form1" runat="server">

        <div class="vh-100 d-flex justify-content-center align-items-center">

            <div class="card shadow-lg border-0 rounded-4 tarjeta-login">

                <div class="card-header bg-personal text-white text-center py-4 rounded-top-4">
                    <h3 class="m-0 fw-bold">Acceso Personal</h3>
                </div>

                <div class="card-body p-5 text-dark">

                    <div class="mb-4">
                        <label class="form-label fw-semibold text-secondary">Legajo o Usuario</label>
                        <asp:TextBox ID="txtEmpleado" runat="server" CssClass="form-control form-control-lg bg-light text-dark"></asp:TextBox>
                    </div>

                    <div class="mb-3">
                        <label class="form-label fw-semibold text-secondary">Contraseña</label>
                        <asp:TextBox ID="txtPasswordEmpleado" runat="server" TextMode="Password" CssClass="form-control form-control-lg bg-light text-dark"></asp:TextBox>
                    </div>

                    <asp:Label ID="lblErrorEmpleado" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>

                    <asp:Button ID="btnIngresarEmpleado" runat="server" Text="Ingresar al Sistema" CssClass="btn btn-ingresar w-100 fw-bold fs-5 py-2 mt-1 shadow-sm" OnClick="btnIngresarEmpleado_Click" />

                    <div class="text-center mt-4">
                        <a href="../Inicio.aspx" class="text-decoration-none text-muted">
                            <small>← Volver al inicio </small>
                        </a>
                    </div>

                </div>
            </div>

        </div>

    </form>

</body>
</html>
