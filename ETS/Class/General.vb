Imports System.IO
Imports System.Data.SqlClient
Imports Stimulsoft.Report
Imports Stimulsoft.Report.Web

Public Class General

    Public Function fn_UploadFile(strDirectory As String, strFileName As HttpPostedFile)

        If Not Directory.Exists(strDirectory) Then
            Directory.CreateDirectory(strDirectory)
        End If

        Dim strPath As String = Path.Combine(strDirectory, strFileName.FileName)
        strFileName.SaveAs(strPath)

        Return strPath

    End Function

    Public Function fn_GrantWebInterface(strUrl As String, strIdRole As String) As Boolean

        Dim pageAccess As Boolean = False
        Dim dbConnect As New DataBase
        Dim strSQL As String

        If strIdRole = "" Then
            FormsAuthentication.SignOut()
        Else
            If dbConnect.StatoConnessione = 0 Then
                dbConnect.connettidb()
            End If

            strSQL = "IF EXISTS(SELECT URL_LINK"
            strSQL = strSQL + " FROM vs_Grant_Web_Interface"
            strSQL = strSQL + " WHERE ID_ROLES = " & strIdRole & ""
            strSQL = strSQL + " AND URL_LINK = " & "'" & strUrl & "')" & ""
            strSQL = strSQL + " BEGIN"
            strSQL = strSQL + " SELECT 'True' Page_Access"
            strSQL = strSQL + " END"
            strSQL = strSQL + " ELSE"
            strSQL = strSQL + " BEGIN"
            strSQL = strSQL + " SELECT 'False' Page_Access"
            strSQL = strSQL + " END"

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

                If Not IsNothing(objDataReader.Item("Page_Access")) Then
                    If objDataReader.Item("Page_Access") = "True" Then
                        pageAccess = True
                    End If
                End If

            End If

            objDataReader.Close()
            dbConnect.ChiudiDb()
        End If

        Return pageAccess

    End Function

    Public Function fn_GetAbsolutePath()

        Dim absolutePath As String = HttpContext.Current.Request.Url.AbsolutePath
        Return absolutePath

    End Function

    Public Sub sb_StimulsoftExcelReport(strSQL As String, strappDirectory As String, strtablename As String)

        Dim dbConnect As New DataBase

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        Dim adapter As SqlDataAdapter = Nothing
        Dim dataSet As DataSet = New DataSet()
        Dim report As StiReport = New StiReport()
        adapter = New SqlDataAdapter(strSQL, dbConnect.Connessione)
        adapter.Fill(dataSet)

        report.Load(strappDirectory)
        dataSet.Tables(0).TableName = strtablename
        report.DataSources.Clear()
        report.Dictionary.Databases.Clear()
        report.RegData("ETS", dataSet.Tables(strtablename))
        report.Dictionary.Synchronize()
        report.Compile()
        report.Render(True)
        StiReportResponse.ResponseAsExcel2007(report)

    End Sub

    Public Sub sb_StimulsoftPdfReport(strSQL As String, strappDirectory As String, strtablename As String)

        Dim dbConnect As New DataBase

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        Dim adapter As SqlDataAdapter = Nothing
        Dim dataSet As DataSet = New DataSet()
        Dim report As StiReport = New StiReport()

        adapter = New SqlDataAdapter(strSQL, dbConnect.Connessione)
        adapter.Fill(dataSet)

        report.Load(strappDirectory)
        dataSet.Tables(0).TableName = strtablename
        report.DataSources.Clear()
        report.Dictionary.Databases.Clear()

        report.RegData("ETS", dataSet.Tables(strtablename))
        report.Dictionary.Synchronize()
        report.Compile()
        report.Render(True)

        StiReportResponse.ResponseAsPdf(report)
    End Sub

    Public Shared Function IsInRole(Role As Integer, IsRole As Integer) As Boolean

        Dim blReturn As Boolean = False

        If Role = IsRole Then
            blReturn = True
        End If

        Return blReturn

    End Function

    Public Shared Function fn_GetNumberOfDaysInMonth(Month As Integer) As Integer

        Dim DaysofMonth As Integer

        If Month = 1 Then
            DaysofMonth = 31
        End If

        If Month = 2 Then
            If DateTime.IsLeapYear(DateTime.Now.Year) Then
                DaysofMonth = 29
            Else
                DaysofMonth = 28
            End If
        End If

        If Month = 3 Then
            DaysofMonth = 31
        End If

        If Month = 4 Then
            DaysofMonth = 30
        End If

        If Month = 5 Then
            DaysofMonth = 31
        End If

        If Month = 6 Then
            DaysofMonth = 30
        End If

        If Month = 7 Then
            DaysofMonth = 31
        End If

        If Month = 8 Then
            DaysofMonth = 31
        End If

        If Month = 9 Then
            DaysofMonth = 30
        End If

        If Month = 10 Then
            DaysofMonth = 31
        End If

        If Month = 11 Then
            DaysofMonth = 30
        End If

        If Month = 12 Then
            DaysofMonth = 31
        End If

        Return DaysofMonth

    End Function
End Class
