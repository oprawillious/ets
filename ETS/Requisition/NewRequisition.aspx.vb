Imports System.Data.SqlClient
Imports System.IO
Imports System.Web.Configuration
Imports ETS.General
Imports ETS.DataBase
Imports System.Globalization.CultureInfo
Public Class NewRequisition
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Page.User.Identity.IsAuthenticated Then
            If Not IsPostBack Then
                If Session("RequsitionId") <> "" Then
                    Call sb_LoadRequisitionDetails()
                End If
                Call Popola_DropList_ItemCategory_Description()
            End If
        Else
            FormsAuthentication.SignOut()
            Response.Redirect("~/Login.aspx")
        End If

    End Sub

    Private Sub Popola_DropList_ItemCategory_Description()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strSQL As String

        strSQL = "SELECT '' DESCRIPTION, 0 ID_ITEM_CATEGORY UNION"
        strSQL = strSQL & " SELECT DISTINCT DESCRIPTION "
        strSQL = strSQL & ",ID_ITEM_CATEGORY"
        strSQL = strSQL & " FROM  vs_Item_Category WITH(NOLOCK);"

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        DropListPeripheralDescription.DataSource = objDataReader
        DropListPeripheralDescription.DataValueField = "ID_ITEM_CATEGORY"
        DropListPeripheralDescription.DataTextField = "DESCRIPTION"
        DropListPeripheralDescription.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub


    Private Sub Popola_DropList__Description_Of_Item()

        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)

        Dim strSQL As String
        strSQL = "SELECT '' DESCRIPTION,0 ID_ITEMS UNION"
        strSQL = strSQL & " SELECT (MODEL +' - '+ DESCRIPTION) DESCRIPTION"
        strSQL = strSQL & ",ID_ITEMS"
        strSQL = strSQL & " FROM  vs_ItemsAvailable WITH(NOLOCK)"
        strSQL = strSQL & " WHERE ID_ITEM_CATEGORY =" + "'" + DropListPeripheralDescription.SelectedItem.Value + "'" + ""

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()
        End If

        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = connessioneDb.Connessione

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()
        DropListCatdesc.DataSource = objDataReader
        DropListCatdesc.DataValueField = "ID_ITEMS"
        DropListCatdesc.DataTextField = "DESCRIPTION"
        DropListCatdesc.DataBind()

        objDataReader.Close()
        connessioneDb.ChiudiDb()

    End Sub


    Protected Sub DropListItemDesc_SelectedIndexChanged(sender As Object, e As EventArgs)
        Call Popola_DropList__Description_Of_Item()
    End Sub

    Private Sub sb_RequestUpdateDeletePeripherals(strOp As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Insert_Update_Delete_Requisition"
                objCommand.Connection = connessioneDb.Connessione
                objCommand.Parameters.AddWithValue("@OP", strOp)
                objCommand.Parameters.AddWithValue("@ID_USER", Page.User.Identity.Name)
                objCommand.Parameters.AddWithValue("@ITEM_DESCRIPTION ", DropListPeripheralDescription.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@DESCIPTION ", DropListCatdesc.SelectedItem.Text)
                objCommand.Parameters.AddWithValue("@ID_ITEM ", DropListCatdesc.SelectedValue)
                objCommand.Parameters.AddWithValue("@ID_ITEM_CATEGORY ", DropListPeripheralDescription.SelectedValue)
                objCommand.Parameters.AddWithValue("@REASON_FOR_REQUEST", txtRemarks.Text)
                objCommand.Parameters.AddWithValue("@QUANTITY", txtQuantity.Text)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                Dim objOutputParameter1 As New SqlParameter("@ID_REQUISITION", SqlDbType.BigInt)
                objCommand.Parameters.Add(objOutputParameter1)
                objOutputParameter1.Direction = ParameterDirection.Output
                objOutputParameter1.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                'hdIDRequsition.Value = CInt(objCommand.Parameters("@ID_REQUISITION").Value)

                If strError <> "" Then
                    lblMessage.ForeColor = System.Drawing.Color.Red
                    lblMessage.Text = strError
                ElseIf strOp = "I" Then
                    lblMessage.ForeColor = System.Drawing.Color.Green
                    lblMessage.Text = "Request Sent Successfully !"
                    Response.AppendHeader("Refresh", "2;url=ViewRequisition.aspx")


                Else
                    lblMessage.Text = "Request Updated successfully !"
                    Response.AppendHeader("Refresh", "2;url=ViewRequisition.aspx")
                End If

            End If

        End If

        connessioneDb.ChiudiDb()


    End Sub



    Private Sub sb_LoadRequisitionDetails()

        Dim dbConnect As New DataBase
        Dim strSQL As String

        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_PERIPHERALS "
        strSQL = strSQL + ", DESCRIPTION"
        strSQL = strSQL + "  FROM peripherals WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE ID_PERIPHERALS =" & Session("RequsitionId") & ""

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim objDataReader As SqlDataReader
        objDataReader = objCommand.ExecuteReader()

        If objDataReader.HasRows Then
            objDataReader.Read()

            If Not IsNothing(objDataReader.Item("DESCRIPTION")) Then
                DropListPeripheralDescription.Text = CStr(objDataReader.Item("DESCRIPTION") & "")
            End If

        End If

        objDataReader.Close()
        objCommand = Nothing
        mySqlAdapter = Nothing
        dbConnect.ChiudiDb()
    End Sub


    Protected Sub btnSaveRequest_Click()
        Call sb_RequestUpdateDeletePeripherals("I")
    End Sub

End Class