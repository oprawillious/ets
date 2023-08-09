Imports System.Data.SqlClient
Imports System.Web.Configuration
Imports System.Drawing
Imports System.IO
Public Class InsertUpdateTicket
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then
                Call Popola_DropList_TicketType()
            End If
        End If
    End Sub

    Private Sub Popola_DropList_TicketType()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT '' DESCRIPTION"
        strSQL = strSQL & " UNION"
        strSQL = strSQL & " SELECT DISTINCT DESCRIPTION"
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK)"
        strSQL = strSQL & " WHERE DESCRIPTION NOT IN ('New Request')"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListTicketType.DataSource = objDataReader
        DropListTicketType.DataTextField = "DESCRIPTION"
        DropListTicketType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Private Sub Popola_DropList_TicketSubType()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT SUB_DESCRIPTION"
        strSQL = strSQL & " ,ID_TICKET_TYPE"
        strSQL = strSQL & " FROM TICKET_TYPE WITH(NOLOCK)"
        strSQL = strSQL & " WHERE DESCRIPTION =" + "'" + DropListTicketType.SelectedItem.Text + "'" + ""

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListSubType.DataSource = objDataReader
        DropListSubType.DataValueField = "ID_TICKET_TYPE"
        DropListSubType.DataTextField = "SUB_DESCRIPTION"
        DropListSubType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub DropListTicketType_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call Popola_DropList_TicketSubType()
    End Sub

    Private Sub sb_CreateTicket()

        Dim uploadfile As New General
        Dim strdirectory = Convert.ToString(WebConfigurationManager.AppSettings("PathDoxServiceRequest"))
        Dim paths = New List(Of String)
        Dim filenames = New List(Of String)
        Dim strUploadedFile = TicketFileUpload.PostedFile

        If TicketFileUpload.HasFiles And (Path.GetExtension(strUploadedFile.FileName.ToString().ToUpper()) = ".PNG" Or Path.GetExtension(strUploadedFile.FileName.ToString().ToUpper()) = ".JPG") Then

            If Not Directory.Exists(strdirectory) Then
                Directory.CreateDirectory(strdirectory)
            End If

            Dim strPath = uploadfile.fn_UploadFile(strdirectory, strUploadedFile)
            paths.Add(strPath)
            filenames.Add(strUploadedFile.FileName)

            Call sb_InsertTicket()
            Call sb_InsertFileTicket(String.Join(";", paths), String.Join(";", filenames))

            txtDescription.Text = ""

        ElseIf Not TicketFileUpload.HasFiles Then
            Call sb_InsertTicket()
            txtDescription.Text = ""
        Else
            lblMessage.Text = "Invalid File Type...(.PNG or .JPG) required !"
            Response.AddHeader("Refresh", "3")
        End If

    End Sub

    Private Sub sb_InsertTicket()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Ticket"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "I")
                objCommand.Parameters.AddWithValue("@ID_TICKET", 0)
                objCommand.Parameters.AddWithValue("@STATUS_TICKET", "")
                objCommand.Parameters.AddWithValue("@ISSUE_TYPE", DropListTicketType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@PRIORITY", DropListPriority.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@ISSUE_SUB_TYPE", DropListSubType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@DESCRIPTION", txtDescription.Text)
                objCommand.Parameters.AddWithValue("@INTERCOMMS", txtIntercomms.Text)
                objCommand.Parameters.AddWithValue("@HELPDESK_REMARKS", "")
                objCommand.Parameters.AddWithValue("@REQUEST_FROM", "ETS")
                objCommand.Parameters.AddWithValue("@USER_ID", Page.User.Identity.Name)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TICKETS", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                hDTicketId.Value = CInt(objCommand.Parameters("@ID_TICKETS").Value)

                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_InsertFileTicket(strpaths As String, strfilename As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_File"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "T")
                objCommand.Parameters.AddWithValue("@ID_TICKETS", hDTicketId.Value)
                objCommand.Parameters.AddWithValue("@FILE_PATH", strpaths)
                objCommand.Parameters.AddWithValue("@FILE_NAME", strfilename)

                Dim objDataReader As SqlDataReader
                objDataReader = objCommand.ExecuteReader()

                objDataReader.Close()
                objCommand = Nothing
                connessioneDb.ChiudiDb()

            End If
        End If

        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub btnCreateTicket_Click(sender As Object, e As EventArgs)
        Call sb_CreateTicket()
        If lblMessage.Text = "" Then
            Response.Redirect("ViewTickets.aspx")
        End If

    End Sub

End Class