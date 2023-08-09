Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class ViewMenuTop
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If fn_ValidatePageAccess() Or IsInRole(Session("R"), Roll_Kind.Administrator) Then
                If Not IsPostBack Then
                    Call sb_LoadViewMenu("")
                End If
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/LOGIN/Login.aspx")
            End If
        End If
    End Sub

    Private Sub sb_LoadViewMenu(strMenuTop As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_MENU_TOP"
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + "  FROM MENU_TOP WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"

        If strMenuTop <> "" Then
            strSQL = strSQL + "AND DESCRIPTION LIKE" + "'%" + strMenuTop + "%'" + ""
        End If

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvMenuTop.DataSource = myDataSet
        gvMenuTop.DataBind()
        dbConnect.ChiudiDb()
    End Sub

    Protected Sub btnSearchMenuTop_Click(sender As Object, e As EventArgs)
        Call sb_LoadViewMenu(txtMenuTop.Text)
    End Sub

    Protected Sub LinkbtnAssignMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("ViewRoleMenu.aspx")
    End Sub

    Protected Sub LinkbtnSubMenu_Click(sender As Object, e As EventArgs)
        Response.Redirect("ViewRoleSubMenu.aspx")
    End Sub

    Private Function fn_ValidatePageAccess() As Boolean
        Dim general As New General
        Dim pageUrl = general.fn_GetAbsolutePath()
        Dim Access = False

        If general.fn_GrantWebInterface(pageUrl, Session("R")) Then
            Access = True
        End If

        Return Access
    End Function
End Class