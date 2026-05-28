<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="GestionDePestamos.Inicio" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio - Sistema de Préstamos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-sRIl4kxILFvY47J16cr9ZwB07vP4J8+LH7qKQnuqkuIAvNWLzeN8tE5YBujZqJLB" crossorigin="anonymous">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.8/dist/js/bootstrap.bundle.min.js" integrity="sha384-FKyoEForCGlyvwx9Hj09JcYn3nv7wiPVlz7YYwJrWVcXK/BmnVDxM+D2scQbITxI" crossorigin="anonymous"></script>
    <link href="Content/Inicio.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="vh-100 d-flex justify-content-center align-items-center bg-light">
    
    <div class="d-flex flex-column flex-md-row gap-5">
        
        <a href="Cliente/LoginCliente.aspx" class="caja-acceso bg-cliente rounded-4 shadow d-flex justify-content-center align-items-center p-4 text-center text-white">
            <h3 class="m-0 fs-4 fw-bold">Ingresar como usuario</h3>
        </a>

        <a href="Empleados/LoginEmpleado.aspx" class="caja-acceso bg-personal rounded-4 shadow d-flex justify-content-center align-items-center p-4 text-center text-white">
            <h3 class="m-0 fs-4 fw-bold">Ingresar como operador/administrador</h3>
        </a>

    </div>
</div>
    </form>
</body>
</html>
