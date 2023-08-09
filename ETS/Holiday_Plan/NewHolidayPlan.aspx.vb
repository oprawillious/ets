Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration
Imports ETS.General
Imports ETS.DataBase
Imports System.Globalization.CultureInfo
Public Class NewHolidayPlan
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack() Then
                Call sb_RestHoliday_Days(Page.User.Identity.Name)
                sb_LoadLeaveDetails()
            End If
        Else

            FormsAuthentication.SignOut()
            Response.Redirect("~/Login.aspx")
        End If

    End Sub

    Private Sub sb_InsertHolidayRequest(strOp As String)

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Holiday_Plan"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_HOLIDAY", "")
                objCommand.Parameters.AddWithValue("@HOLIDAY_TYPE", DropDownListHolidayType.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@HOLIDAY_START_DATE", txtStartDate.Text)
                objCommand.Parameters.AddWithValue("@HOLIDAY_END_DATE", txtEndDate.Text)
                objCommand.Parameters.AddWithValue("@NO_OF_DAYS", txtDays.Text)
                objCommand.Parameters.AddWithValue("@ID_DEPT", Session("D"))
                objCommand.Parameters.AddWithValue("@REMARKS", txtRemarks.Text)
                objCommand.Parameters.AddWithValue("@USERNAME ", Page.User.Identity.Name)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_HOLIDAY_PLAN", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                hDIdHoliday_plan.Value = CInt(objCommand.Parameters("@ID_HOLIDAY_PLAN").Value)

                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If

        End If

        connessioneDb.ChiudiDb()
    End Sub

    Private Sub sb_RestHoliday_Days(username As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Reset_Holiday_Used_Days"
                objCommand.Connection = connessioneDb.Connessione

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strErrorStored <> "" Then
                    lblMessage.Text = strErrorStored
                End If

            End If

        End If
        connessioneDb.ChiudiDb()
    End Sub

    Protected Sub txtEndDate_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim strStartDate As DateTime = DateTime.ParseExact(txtStartDate.Text, "MM/dd/yyyy", CurrentUICulture.DateTimeFormat)
        Dim strEndDate As DateTime = DateTime.ParseExact(txtEndDate.Text, "MM/dd/yyyy", CurrentUICulture.DateTimeFormat)
        Dim countdays As TimeSpan = strEndDate.Subtract(strStartDate)
        Dim strDays = Convert.ToInt32(countdays.Days)

        If Convert.ToInt32(countdays.Days) >= 0 Then
            txtDays.Text = strDays + 1
        ElseIf (strEndDate < strStartDate) Then
            lblMessage.Text = "End Date cannot be earlier than start Date Or Holiday Year exceeds current year"
            txtDays.Text = ""
            txtEndDate.Text = ""
        Else

            lblMessage.Text = "you have choosen an invalid Date, please confirm!!!"
            txtDays.Text = ""
            txtStartDate.Text = ""
            txtEndDate.Text = ""
        End If
        If Convert.ToInt32(txtDays.Text) > Convert.ToInt32(lblRemainingDays.Text) Then
            lblMessage.Text = "Leave days cannot exceed remaining days"
            txtDays.Text = ""
            txtEndDate.Text = ""
        End If
    End Sub

    Protected Sub btnRequestHoliday_Click()
        If fn_ValidateHolidayUsedDays() Then
            Call sb_InsertHolidayRequest("I")
            Response.Redirect("ViewHolidayPlan.aspx")
        Else
            lblMessage.Text = "you have previously applied using the same date"
        End If
    End Sub
    Private Sub sb_LoadLeaveDetails()
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT USED_DAYS"
        strSQL = strSQL + ", REMAINING_DAYS "
        strSQL = strSQL + " FROM vs_Holiday_Used_Days WITH(NOLOCK)"
        strSQL = strSQL + " WHERE 1=1"
        strSQL = strSQL + " AND USERNAME =" + "'" + Page.User.Identity.Name + "'" + ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim myDataSet As New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then
            objDataReader.Read()

            If Not IsNothing(objDataReader.Item("USED_DAYS")) Then
                lblUsedDays.Text = CStr(objDataReader.Item("USED_DAYS") & "")
            End If
            If Not IsNothing(objDataReader.Item("REMAINING_DAYS")) Then
                lblRemainingDays.Text = CStr(objDataReader.Item("REMAINING_DAYS") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub
    Private Sub sb_InsertFileHoliday(strpaths As String, strfilename As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_File"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@OP", "H")
                objCommand.Parameters.AddWithValue("@ID_HOLIDAY_PLAN", hDIdHoliday_plan.Value)
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


    Private Function fn_ValidateHolidayUsedDays()
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String
        Dim blReturn As Boolean = False

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Holiday_Check_Employee_Used_Days"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@START_DATE", txtStartDate.Text)
                objCommand.Parameters.AddWithValue("@END_DATE", txtEndDate.Text)
                objCommand.Parameters.AddWithValue("@ID_USER", Page.User.Identity.Name)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@BL_RETURN", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                blReturn = CBool(objCommand.Parameters("@BL_RETURN").Value)
            End If

        End If

        connessioneDb.ChiudiDb()
        Return blReturn

    End Function
End Class