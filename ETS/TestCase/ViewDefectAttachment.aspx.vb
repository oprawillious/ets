Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General

Public Class viewDefectAttachment
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If IsInRole(Session("R"), Roll_Kind.Administrator) Or IsInRole(Session("R"), Roll_Kind.Tester) Then
                If Not IsPostBack Then
                    If Session("TD") <> "" Then
                        Call sb_LoadAttachments()
                    End If
                End If
            Else
                FormsAuthentication.SignOut()
                Response.Redirect("~/Login.aspx")
            End If
        End If

    End Sub


    Private Sub sb_LoadAttachments()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_DEFECT_DOCUMENT"
        strSQL = strSQL + ", FILE_PATH"
        strSQL = strSQL + ", FILE_NAME"
        strSQL = strSQL + "  FROM DEFECT_DOCUMENT WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_TEST_DEFECT =" & Session("TD") & ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvDefectAttachments.DataSource = myDataSet
        gvDefectAttachments.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub gvDefectAttachments_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvDefectAttachments.RowCommand

        Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
        Dim row As GridViewRow = gvDefectAttachments.Rows(Index)
        Dim strFilePath As String = TryCast(row.FindControl("hdFilePath"), HiddenField).Value
        Dim strIdDefectDoc As String = TryCast(row.FindControl("hdIdDefectDoc"), HiddenField).Value

        If e.CommandName = "Download" Then
            sb_DownloadAttachment(strFilePath)
        End If

        If e.CommandName = "Remove" Then
            sb_DeleteDefectImage(strIdDefectDoc)
            Response.Redirect("ViewDefectAttachment.aspx")
        End If
    End Sub

    Private Sub sb_DeleteDefectImage(strIdDefectDoc As String)
        Throw New NotImplementedException()

    End Sub

    Protected Sub btClose_Click(sender As Object, e As EventArgs)
        PopupImageView.Hide()
        Response.Redirect("ViewDefectAttachment.aspx")
    End Sub

    Private Sub sb_DownloadAttachment(strFilePath As String)
        Response.ContentType = ContentType
        Response.AppendHeader("Content-Disposition", "attachment; filename=" & IO.Path.GetFileName(strFilePath))
        Response.WriteFile(strFilePath)
        Response.End()

    End Sub
End Class