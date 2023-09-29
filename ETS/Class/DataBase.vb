Imports System.Data.SqlClient

Public Class DataBase

    Dim ConnessioneDB As New SqlConnection

    Public ReadOnly Property Connessione() As SqlConnection

        Get

            Return ConnessioneDB

        End Get

    End Property

    Public ReadOnly Property StatoConnessione() As Integer

        Get

            Return ConnessioneDB.State

        End Get

    End Property

    Public Function connettidb()

        Try

            ConnessioneDB.ConnectionString = fun_Get_ConnectionString()
            ConnessioneDB.Open()

        Catch ex As Exception

            Return -1

            Exit Function

        End Try

        Return ConnessioneDB

    End Function

    Public Function ChiudiDb()

        Try
            If ConnessioneDB.State = 1 Then

                ConnessioneDB.Close()
                ConnessioneDB.Dispose()
                SqlConnection.ClearPool(ConnessioneDB)

            End If

            ChiudiDb = 1

        Catch ex As Exception

            ChiudiDb = -1

            Exit Function

        End Try

    End Function

    Public Function fun_Get_ConnectionString() As String

        Dim strConn As String = ConfigurationManager.ConnectionStrings("ETS_ConnessioneSqlSERVER").ConnectionString
        Return strConn

    End Function

    Public Enum Roll_Kind

        Administrator = 1
        Developer = 2
        Tester = 3
        HelpDesk = 4
        Staff_User = 5
        HR_Manager = 6
        Manager = 7
        Senior_Manager = 8
        EDP_User = 9
        IT_Manager = 10

    End Enum

End Class

