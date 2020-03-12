Imports System.Environment
Imports System.IO
Imports Microsoft.Win32

Class MainWindow
    Private Sub ExportDhud(sender As Object, e As RoutedEventArgs)
        StatusText1.Text = "Exporting...."
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        'Dim SettingsFile As String = My.Computer.FileSystem.ReadAllText(appData & "\Diabotical\Settings.txt")
        Dim HudSettings As String
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
                            HudSettings = currentRow(1)
                            StatusText1.Text = "Found HUD settings."
                        End If
                    Next
                Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                    MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                End Try
            End While
        End Using
        If HudSettings IsNot Nothing Then
            WriteHudFile(HudSettings)
        Else
            StatusText1.Text = "Could not find HUD settings."
        End If
    End Sub
    Private Sub WriteHudFile(contents As String)
        Dim saveFileDialog1 As New SaveFileDialog With {
            .Filter = "dhud files (*.dhud)|*.dhud|All files (*.*)|*.*",
            .FilterIndex = 1,
            .RestoreDirectory = True
        }

        If saveFileDialog1.ShowDialog() = True Then
            File.WriteAllText(saveFileDialog1.FileName, contents)
            StatusText1.Text = "Saved HUD settings to" & saveFileDialog1.FileName
        End If
    End Sub


End Class
