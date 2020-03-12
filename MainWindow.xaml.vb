Imports System.Environment
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic.FileIO.TextFieldParser


Class MainWindow
    Private Sub ExportDhud(sender As Object, e As RoutedEventArgs)
        StatusText1.Text = "Exporting...."
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        'Dim SettingsFile As String = My.Computer.FileSystem.ReadAllText(appData & "\Diabotical\Settings.txt")
        Using SettingsFile As New Microsoft.VisualBasic.FileIO.TextFieldParser(appData & "\Diabotical\Settings.txt")
            SettingsFile.TextFieldType = FileIO.FieldType.Delimited
            SettingsFile.SetDelimiters("=")
            Dim currentRow As String()
            While Not SettingsFile.EndOfData
                Try
                    currentRow = SettingsFile.ReadFields()
                    Dim currentField As String
                    For Each currentField In currentRow
                        If currentField = "hud_definition" Then
                            MsgBox("Found " & currentField)
                        End If
                    Next
                Catch ex As Microsoft.VisualBasic.
                  FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message &
        "is not valid and will be skipped.")
                End Try
            End While
        End Using
    End Sub
End Class
