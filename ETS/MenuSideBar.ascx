<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MenuSideBar.ascx.vb" Inherits="ETS.MenuSideBar" %>

<header>

 <nav class="main">
        <ul>
            <li class="search">
                <a class="fa-search" href="#search">Search</a>
                <%--<form id="search" method="get" action="#">
                    <input type="text" name="query" placeholder="Search" />
                </form>--%>
            </li>
            <li class="menu">
                <a class="fa-bars" href="#menu">Menu</a>
            </li>
        </ul>
    </nav>
</header>

<!-- Menu -->
<section id="menu">

    <!-- Search -->
    <section>
        <form class="search" method="get" action="#">
<%--            <input type="text" name="query" placeholder="Search" />--%>
            <asp:Label ID="lblQuickSearch" runat="server" Text="Quick Search" /><br /><br />
            <table>
                <tr>
                    <td><asp:TextBox ID="txtTicketNumber" runat="server" placeholder="Ticket ID..." ClientIDMode="Static" ></asp:TextBox></td>
<%--                    <td><asp:Button ID="btnSearchTicket" runat="server" Text="Go" OnClick="btnSearchTicket_Click"/></td>--%>
                    <td><asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">Go</asp:LinkButton></td>

                </tr>
                <tr>
                    <td><asp:TextBox ID="txtTaskNumber" runat="server" placeholder="Task ID..." ClientIDMode="Static"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearchTask" runat="server" Text="Go" ClientIDMode="Static" OnClick="btnSearchTask_Click"/></td>
                </tr>
                <tr>
                    <td><asp:TextBox ID="txtTestCaseNumber" runat="server" placeholder="Test Case ID..." ClientIDMode="Static"></asp:TextBox></td>
                    <td><asp:Button ID="btnSearchTestCase" runat="server" Text="Go"/></td>
                </tr>
            </table>
        </form>
    </section>

    <!-- Links -->
    <%--<section>
        <ul class="links">
            <li>
                <a href="#">
                    <h3>Lorem ipsum</h3>
                    <p>Feugiat tempus veroeros dolor</p>
                </a>
            </li>
            <li>
                <a href="#">
                    <h3>Dolor sit amet</h3>
                    <p>Sed vitae justo condimentum</p>
                </a>
            </li>
            <li>
                <a href="#">
                    <h3>Feugiat veroeros</h3>
                    <p>Phasellus sed ultricies mi congue</p>
                </a>
            </li>
            <li>
                <a href="#">
                    <h3>Etiam sed consequat</h3>
                    <p>Porta lectus amet ultricies</p>
                </a>
            </li>
        </ul>
    </section>--%>

    <!-- Actions -->
    <section>
        <ul class="actions stacked">
            <li></li>
            <asp:LinkButton ID="LinkViewAccountSettings" runat="server" OnClick="LinkViewAccountSettings_Click" style="margin: 0 auto;"><asp:Label ID="lblUserSession" runat="server" Text=""></asp:Label></asp:LinkButton>
            <li><asp:LinkButton ID="LogOut" runat="server" OnClick="LogOut_Click" class="button large fit">Log Out</asp:LinkButton></li>            
        </ul>  
        
        <br /><br />
        <div style="text-align: center;">
            <img src="assets/img/ptml_logo.png" alt="logo" style="text-align: center;"/>
        </div>
        
        <div style="text-align: center;">
            <h4>ETS by PTML</h4>
            <h5>version 1.0.0.0</h5>
        </div>
        
    </section>

</section>

