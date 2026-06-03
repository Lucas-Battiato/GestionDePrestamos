<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginCliente.aspx.cs" Inherits="GestionDePestamos.Cliente.LoginCliente" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Ingreso</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
    <link href="../Content/Style.css" rel="stylesheet" type="text/css"/>
</head>
<body class="bg-light">
    
    <form id="form1" runat="server">
        
       <div class="vh-100 d-flex justify-content-center align-items-center">
    
    <div class="card shadow-lg border-0 rounded-4 tarjeta-login">
        
        <div class="card-header bg-cliente text-white text-center py-4 rounded-top-4">
            <h3 class="m-0 fw-bold">Acceso Clientes</h3>
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
            
            <asp:Label ID="lblMensajeError" runat="server" CssClass="text-danger fw-bold d-block mb-3" Visible="false"></asp:Label>

            <asp:Button ID="btnIngresar" runat="server" Text="Iniciar Sesión" CssClass="btn btn-iniciar w-100 fw-bold fs-5 py-2 mt-1 shadow-sm" OnClick="btnIngresar_Click" />
            
            <div class="text-center mt-4">
                <span class="text-secondary">¿Aún no tenés cuenta?</span> 
                <a href="Registro.aspx" class="fw-bold link-registro">
                    ¡Registrate acá!
                </a>
            </div>

            <div class="text-center mt-3">
                <a href="../Inicio.aspx" class="text-decoration-none text-muted">
                    <small>← Volver al inicio</small>
                </a>
            </div>
            
        </div>
    </div>
    
</div>
        
    </form>
    
</body>
</html>
