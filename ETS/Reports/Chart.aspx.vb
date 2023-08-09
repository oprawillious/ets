Imports Stimulsoft.Report
Imports Stimulsoft.Report.Dictionary
Imports Stimulsoft.Report.Web

Public Class Chart
	Inherits Page

	Shared Sub New()
		Dim path = HttpContext.Current.Server.MapPath("~/license.key")
		Stimulsoft.Base.StiLicense.LoadFromFile(path)
	End Sub

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

	End Sub

	Protected Sub StiWebViewer1_GetReport(ByVal sender As Object, ByVal e As StiReportDataEventArgs)
		Dim report = StiReport.CreateNewDashboard()
		Dim path = Server.MapPath("~/Reports/ETS_DASHBOARD.mrt")
		report.Load(path)
		e.Report = report
	End Sub
End Class