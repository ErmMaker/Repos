Imports System.Data.SQLite
Imports System.IO

Public Class SQLiteHelper
    Private ConnectionString As String

    ' Constructor
    Public Sub New(ByVal connectionString As String)
        Me.ConnectionString = connectionString
    End Sub

    ' Method: Create SQLite Database File
    Public Shared Sub CreateDatabase(ByVal filePath As String)
        If Not File.Exists(filePath) Then
            SQLiteConnection.CreateFile(filePath)
            Console.WriteLine("Database file created at " & filePath)
        Else
            Console.WriteLine("Database file already exists at " & filePath)
        End If
    End Sub

    ' Method: Create a Table
    Public Sub CreateTable(ByVal createTableQuery As String)
        Using connection As New SQLiteConnection(ConnectionString)
            Using command As New SQLiteCommand(createTableQuery, connection)
                connection.Open()
                command.ExecuteNonQuery()
                Console.WriteLine("Table created successfully.")
            End Using
        End Using
    End Sub

    ' Execute a SQL query (e.g., SELECT)
    Public Function ExecuteQuery(ByVal query As String) As DataTable
        Dim dataTable As New DataTable()
        Using connection As New SQLiteConnection(ConnectionString)
            Using command As New SQLiteCommand(query, connection)
                connection.Open()
                Using reader As SQLiteDataReader = command.ExecuteReader()
                    dataTable.Load(reader)
                End Using
            End Using
        End Using
        Return dataTable
    End Function

    ' Execute a SQL command (e.g., INSERT, UPDATE, DELETE)
    Public Sub ExecuteCommand(ByVal commandText As String)
        Using connection As New SQLiteConnection(ConnectionString)
            Using command As New SQLiteCommand(commandText, connection)
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    ' Parameterized query to avoid SQL injection
    Public Sub ExecuteCommandWithParameters(ByVal commandText As String, ByVal parameters As List(Of SQLiteParameter))
        Using connection As New SQLiteConnection(ConnectionString)
            Using command As New SQLiteCommand(commandText, connection)
                command.Parameters.AddRange(parameters.ToArray())
                connection.Open()
                command.ExecuteNonQuery()
            End Using
        End Using
    End Sub
End Class