Imports System.Data.SqlClient
Imports System.Threading.Tasks

Public Class InsertTestCase
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then
                'Call Popola_DropList_AppCategory()
                Call Popola_DropList_Task()


                Dim idTask = Request.QueryString("Id")
                Dim strTask = Request.QueryString("Task")

                If idTask <> "" And strTask <> "" Then
                    DropListSubType.SelectedItem.Value = idTask
                    DropListSubType.SelectedItem.Text = strTask
                    DropListSubType.Attributes.Add("disabled", "disabled")
                End If
            End If
        End If

    End Sub
    Private Sub Popola_DropList_AppCategory()
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        strSQL = "SELECT DISTINCT SUB_DESCRIPTION FROM TICKET_TYPE WITH(NOLOCK)"
        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListSubType.DataSource = objDataReader
        DropListSubType.DataValueField = "SUB_DESCRIPTION"
        DropListSubType.DataTextField = "SUB_DESCRIPTION"
        DropListSubType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()
    End Sub

    Private Sub Popola_DropList_Task()
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String

        'strSQL = "SELECT distinct ID_TASK, TASK_DESCRIPTION FROM vs_Task WITH(NOLOCK) WHERE 1=1 AND ISNULL(FLAG_COMPLETE,'N') = 'N' ORDER BY ID_TASK, TASK_DESCRIPTION DESC"
        strSQL = "SELECT DISTINCT T.ID_TASK, T.TASK_DESCRIPTION"
        strSQL = strSQL + " FROM vs_Task T WITH(NOLOCK)"
        strSQL = strSQL + " LEFT JOIN vs_Test_Cases TC ON TC.ID_TASK = T.ID_TASK"
        strSQL = strSQL + " WHERE 1=1 AND TC.ID_TASK IS NULL AND ISNULL(FLAG_COMPLETE,'N') = 'N'"
        strSQL = strSQL + " ORDER BY ID_TASK, TASK_DESCRIPTION DESC"
        '   strSQL = "SELECT B.ID_TASK, 
        '      B.TASK_DESCRIPTION  
        'From vs_Task  B  WHERE NOT EXISTS (SELECT ID_TASK FROM TEST_CASES A WHERE A.ID_TASK = B.ID_TASK)"
        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListSubType.DataSource = objDataReader
        DropListSubType.DataValueField = "ID_TASK"
        DropListSubType.DataTextField = "TASK_DESCRIPTION"
        DropListSubType.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_Insert_New_TestCases()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Test_Case"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@USER_ID ", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@TEST_DESCRIPTION", txtDescription.Text)
                objCommand.Parameters.AddWithValue("@ID_TASK", DropListSubType.SelectedItem.Value)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_TEST_CASES", SqlDbType.Int)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If
        End If
        connessioneDb.ChiudiDb()
    End Sub
    Protected Sub btnCreateNewTestCase_Click()
        Call sb_Insert_New_TestCases()
        Response.Redirect("ViewTestCase.aspx")
    End Sub



    Private Sub sb_ViewTask(strIdTask As String)

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_TASK"
        strSQL = strSQL + ", ID_TICKETS"
        strSQL = strSQL + ", TASK_DESCRIPTION"
        strSQL = strSQL + ", CATEGORY"
        strSQL = strSQL + ", PRIORITY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_ASSIGNED,109) DATE_ASSIGNED"
        strSQL = strSQL + ", TYPE_TASK"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_START_DATE, 109) EXPECTED_START_DATE"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),EXPECTED_END_DATE, 109) EXPECTED_END_DATE"
        strSQL = strSQL + ", STATUS_TASK"
        strSQL = strSQL + ", REMARK"
        strSQL = strSQL + ", DEV_REMARKS"
        strSQL = strSQL + "  FROM vs_Task WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1=1"
        strSQL = strSQL + "  AND ISNULL(FLAG_COMPLETE,'N') = 'N'"

        If strIdTask <> "" Then
            strSQL = strSQL + " AND ID_TASK =" + "'" + strIdTask + "'" + ""
        End If

        strSQL = strSQL + " ORDER BY ID_TASK DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)

        'gvTask.DataSource = myDataSet
        'gvTask.DataBind()
        dbConnect.ChiudiDb()

    End Sub

End Class