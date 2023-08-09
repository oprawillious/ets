Imports System.Data.SqlClient
Imports ETS.DataBase
Imports ETS.General
Imports System.Web.Script.Serialization
Public Class ViewRequisition
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Page.User.Identity.IsAuthenticated Then
            If Not Page.IsPostBack Then
                If Not IsInRole(Session("R"), Roll_Kind.Administrator) Then
                    gvViewRequisition.Columns(15).Visible = False
                    gvViewRequisition.Columns(16).Visible = False
                    trlblEmpName.Visible = False
                    trtxtEmpName.Visible = False
                End If

                If IsInRole(Session("R"), Roll_Kind.IT_Manager) Then
                    btnAddRequisition.Visible = False
                End If
                Call sb_viewRequisition()
            End If
        End If
    End Sub

    Private Sub sb_viewRequisition()
        Dim dbConnect As New DataBase
        Dim strSQL As String
        If dbConnect.StatoConnessione = 0 Then
            dbConnect.connettidb()
        End If

        strSQL = "SELECT ID_REQUISITION"
        strSQL = strSQL + ", ID_USER"
        strSQL = strSQL + ", ITEM_DESCRIPTION"
        strSQL = strSQL + ", ID_ITEM_CATEGORY"
        strSQL = strSQL + ", USERNAME"
        strSQL = strSQL + ", QUANTITY"
        strSQL = strSQL + ", REMARKS"
        strSQL = strSQL + ", DATE_IT_MANAGER_APPROVE"
        strSQL = strSQL + ", REQUESTED_BY"
        strSQL = strSQL + ", DESCRIPTION_BY_CATEGORY"
        strSQL = strSQL + ", DEPT"
        strSQL = strSQL + ", ID_ITEM"
        strSQL = strSQL + ", CONVERT(NVARCHAR(12),DATE_REQUESTED, 109) DATE_REQUESTED"
        strSQL = strSQL + ", IIF(FLAG_ADMIN_APPROVE IS NULL,'Pending', IIF(FLAG_ADMIN_APPROVE= 'Y', 'Approved', 'Rejected'))FLAG_ADMIN_APPROVE"
        strSQL = strSQL + ", REASON_FOR_REQUEST"
        strSQL = strSQL + "  FROM vs_Requisition WITH(NOLOCK)"
        strSQL = strSQL + "  WHERE 1 = 1"

        If IsInRole(Session("R"), Roll_Kind.Administrator) And IsInRole(Session("R"), Roll_Kind.HR_Manager) And IsInRole(Session("R"), Roll_Kind.Senior_Manager) And IsInRole(Session("R"), Roll_Kind.Manager) Then
            strSQL = strSQL + " AND DATE_ADMIN_APPROVE IS NULL"
        End If

        If IsInRole(Session("R"), Roll_Kind.Administrator) Then
            strSQL = strSQL + " AND DATE_ADMIN_APPROVE IS NULL"
        End If

        If IsInRole(Session("R"), Roll_Kind.IT_Manager) Then
            strSQL = strSQL + " AND DATE_IT_MANAGER_APPROVE IS NULL"
            strSQL = strSQL + " AND DATE_ADMIN_APPROVE IS NOT NULL"
            strSQL = strSQL + " AND FLAG_ADMIN_APPROVE = 'Y'"
        End If

        If Not IsInRole(Session("R"), Roll_Kind.Administrator) And Not IsInRole(Session("R"), Roll_Kind.IT_Manager) Then
            strSQL = strSQL + " AND USERNAME =" + "'" + Page.User.Identity.Name + "'" + ""
        End If

        If txtEmployeeName.Text <> "" Then
            strSQL = strSQL + " AND REQUESTED_BY LIKE '%" + txtEmployeeName.Text + "%'"
        End If

        If txtRequestDate.Text <> "" Then
            strSQL = strSQL + " AND CONVERT(NVARCHAR(12),DATE_REQUESTED,109) =" & "CONVERT(NVARCHAR(12)," & "CAST('" & txtRequestDate.Text & "' AS DATETIME)" & ",109)"
        End If

        strSQL = strSQL + " ORDER BY ID_REQUISITION DESC"

        Dim objCommand As SqlCommand = New SqlCommand()
        objCommand.CommandText = strSQL
        objCommand.CommandType = CommandType.Text
        objCommand.Connection = dbConnect.Connessione

        Dim mySqlAdapter As SqlDataAdapter = New SqlDataAdapter(objCommand)
        Dim myDataSet As DataSet = New DataSet()
        mySqlAdapter.Fill(myDataSet)

        Dim dt As DataTable = New DataTable()
        mySqlAdapter.Fill(dt)
        gvViewRequisition.DataSource = myDataSet
        gvViewRequisition.DataBind()
        dbConnect.ChiudiDb()

    End Sub

    Protected Sub btnSearchRequisition_Click()
        Call sb_viewRequisition()

    End Sub

    Protected Sub btnAddRequisition_Click()
        Response.Redirect("NewRequisition.aspx")
    End Sub

    Protected Sub gvViewRequestion_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles gvViewRequisition.RowCommand
        If e.CommandName <> "Page" Then
            Dim Index As Integer = (CType(CType(e.CommandSource, Control).NamingContainer, GridViewRow)).RowIndex
            Dim row As GridViewRow = gvViewRequisition.Rows(Index)
            Dim strItemCat As String = TryCast(row.FindControl("hdItemCategory"), HiddenField).Value
            Dim strIdRequisition As String = TryCast(row.FindControl("hdIdRequisition"), HiddenField).Value
            Dim strIdUser As String = TryCast(row.FindControl("hdIdUser"), HiddenField).Value
            Dim strQuantity As String = TryCast(row.FindControl("hdQuantity"), HiddenField).Value
            Dim strItemDesc As String = TryCast(row.FindControl("hdItemdesc"), HiddenField).Value
            Dim strIdItem As String = TryCast(row.FindControl("hdIdItem"), HiddenField).Value

            If e.CommandName = "Acknowledge" Then
                Session("RequsitionId") = strIdRequisition
                Response.Redirect("NewRequisition.aspx")

            End If

            If e.CommandName = "Approve" Then
                Call sb_ApproveRejectRequisition("Y", strIdUser, strIdRequisition, "", strIdItem, strQuantity)
                Call sb_Update_Item_Remaining("Y", strItemCat, strIdRequisition, strItemDesc, strQuantity, strIdItem)
                Response.Redirect("ViewRequisition.aspx")
            End If

            If e.CommandName = "Reject" Then
                Call sb_ApproveRejectRequisition("N", strIdUser, strIdRequisition, "", 0, "")
                Response.Redirect("ViewRequisition.aspx")
            End If

        End If


    End Sub

    Private Sub sb_Update_Item_Remaining(strFlag As String, strItemCat As String, strIdRequest As String, strDesc As String, strQuantity As String, strIdItem As String)
        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strError As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then

                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Update_Item_Remaining"
                objCommand.Connection = connessioneDb.Connessione
                objCommand.Parameters.AddWithValue("@FLAG_APPROVE_REJECT", strFlag)
                objCommand.Parameters.AddWithValue("@ID_ITEM_CATEGORY", strItemCat)
                objCommand.Parameters.AddWithValue("@DESC_BY_CAT ", strDesc)
                objCommand.Parameters.AddWithValue("@ID_ITEM", strIdItem)
                objCommand.Parameters.AddWithValue("@ID_REQUISITION ", strIdRequest)
                objCommand.Parameters.AddWithValue("@QUANTITY_REQUESTED", strQuantity)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100

                objCommand.ExecuteNonQuery()
                strError = CStr(objCommand.Parameters("@ERROR_CODE").Value)
                If strError <> "" Then

                End If

            End If

        End If

        connessioneDb.ChiudiDb()

    End Sub

    Private Sub sb_ApproveRejectRequisition(strFlag As String, strIdUser As String, strIdRequisition As String, strFlagITManagerApprove As String, strIdItem As String, strQuantity As String)


        Dim connessioneDb As New DataBase
        Dim objCommand As New SqlCommand
        Dim mySqlAdapter As New SqlDataAdapter(objCommand)
        Dim strErrorStored As String

        If connessioneDb.StatoConnessione = 0 Then
            connessioneDb.connettidb()

            If connessioneDb.StatoConnessione > 0 Then
                objCommand.CommandType = CommandType.StoredProcedure
                objCommand.CommandText = "GUI_Approve_Reject_Requisition"
                objCommand.Connection = connessioneDb.Connessione

                objCommand.Parameters.AddWithValue("@FLAG_ADMIN_APPROVE", strFlag)
                objCommand.Parameters.AddWithValue("@REMARKS", hAdminRemark.Value)
                objCommand.Parameters.AddWithValue("@ID_USER", strIdUser)
                objCommand.Parameters.AddWithValue("@ID_ROLE", Session("R"))
                objCommand.Parameters.AddWithValue("@FLAG_IT_MANAGER_APPROVE", strFlagITManagerApprove)
                objCommand.Parameters.AddWithValue("@ID_ITEM", strIdItem)
                objCommand.Parameters.AddWithValue("@QUANTITY", strQuantity)
                objCommand.Parameters.AddWithValue("@IT_MANAGER_REMARKS", hdITManagerRemarks.Value)
                objCommand.Parameters.AddWithValue("@ID_REQUISITION", strIdRequisition)

                Dim objOutputParameter As New SqlParameter("@ERROR_CODE", SqlDbType.NVarChar)
                objCommand.Parameters.Add(objOutputParameter)
                objOutputParameter.Direction = ParameterDirection.Output
                objOutputParameter.Size = 100
                objCommand.ExecuteReader()

                strErrorStored = CStr(objCommand.Parameters("@ERROR_CODE").Value)

                If strErrorStored <> "" Then
                    ''MsgBox("hello Admin,this item is currently of of stock,Kindly reject the users request Thanks")
                    ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "RequestError();", True)
                End If

            End If
        End If

        connessioneDb.ChiudiDb()

    End Sub

    Protected Sub gvViewRequisition_PageIndexChanging(sender As Object, e As GridViewPageEventArgs) Handles gvViewRequisition.PageIndexChanging
        gvViewRequisition.PageIndex = e.NewPageIndex()
        Call sb_viewRequisition()
    End Sub

End Class