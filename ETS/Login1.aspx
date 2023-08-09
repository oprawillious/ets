<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login1.aspx.vb" Inherits="ETS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bug System - Login</title>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link href="https://fonts.googleapis.com/css?family=Karla:400,700&display=swap" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdn.materialdesignicons.com/4.8.95/css/materialdesignicons.min.css" />
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" />
    <link rel="stylesheet" href="assets/css/login.css" />
    <link rel="stylesheet" href="assets/css/fontawesome-all.min.css" />
    <link href="https://cdn.jsdelivr.net/npm/@fortawesome/fontawesome-free@5.15.4/css/fontawesome.min.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />

    <style>
        .loading {
            display: flex;
            justify-content: center;
        }

            .loading::after {
                content: "";
                width: 50px;
                height: 50px;
                border: 10px solid #dddddd;
                border-top-color: #1e8449;
                border-bottom-color: #1e8449;
                border-radius: 50%;
                animation: loading 1s ease infinite;
                top: calc(50% - 30px);
                left: calc(50% - 30px);
                background-color: #EBEBEB;
                filter: alpha(opacity=70);
                opacity: 0.7;
            }

        @keyframes loading {
            to {
                transform: rotate(1turn);
            }
        }
    </style>
</head>


<body>
    <form id="form1" runat="server">

        <div class="preloader" runat="server" id="divloader" visible="true"></div>
        <main class="d-flex align-items-center min-vh-100 py-3 py-md-0">
            <div class="container">
                <div class="card login-card">
                    <div class="row no-gutters">
                        <div class="col-md-5">
                            <img src="assets/img/tech-support.png" alt="login" class="login-card-img" />
                        </div>
                        <div class="col-md-7">
                            <div class="card-body">
                                <div class="brand-wrapper">
                                    <img src="assets/img/ptml_logo.png" alt="logo" />
                                </div>
                                <p class="login-card-description">Bug System - Login</p>

                                <div class="form-group mb-4">
                                    <label for="email" class="sr-only">Username</label>
                                    <label>Username<span style="color: red;">*</span></label><asp:TextBox ID="txtUsername" runat="server" class="form-control" placeholder="Username" Style="border-left: 5px solid #1e8449;"></asp:TextBox>

                                </div>

                                <div class="form-group mb-4">
                                    <label for="password" class="sr-only">Password</label>
                                    <label>Password<span style="color: red;">*</span></label><asp:TextBox ID="txtPassword" runat="server" class="form-control" placeholder="Password" TextMode="Password" Style="border-left: 5px solid #1e8449;"></asp:TextBox>
                                </div>

                                <asp:CheckBox ID="chkPersistCookie" runat="server" Text="Remember Me" />
                                <asp:Button ID="btnLogin" runat="server" Text="&#xf090; Login" class="btn btn-block login-btn mb-4 fa" OnClick="btnLogin_Click" Style="background-color: #1e8449;" /><br />

                                <asp:Label ID="lblMessage" runat="server" Text="" Style="color: red;"></asp:Label><br />

                                <%--<a href="ResetPassword.aspx" class="forgot-password-link">Forgot password?</a>--%>
                                <%--<p class="login-card-footer-text">Don't have an account? <a href="#!" class="text-reset">Request here</a></p>--%>
                                <nav class="login-card-footer-nav">
                                    <a href="ResetPassword.aspx">Forgot Password ?</a>
                                </nav>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </main>
        <script src="https://code.jquery.com/jquery-3.4.1.min.js"></script>
        <script src="https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"></script>
        <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"></script>

        <%--<script src="https://ajax.googleapis.com/ajax/libs/jquery/2.2.4/jquery.min.js"></script>
  <script src="https://cdnjs.cloudflare.com/ajax/libs/modernizr/2.8.3/modernizr.js"></script>--%>


    </form>
</body>
</html>
