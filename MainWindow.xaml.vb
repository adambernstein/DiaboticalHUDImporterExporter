Imports System.Environment
Imports System.IO
Imports Microsoft.Win32

Class MainWindow
    Private Sub ExportDhud(sender As Object, e As RoutedEventArgs)
        StatusText1.Text = "Exporting...."
        Dim SenderName As String = DirectCast(sender, System.Windows.FrameworkElement).Name
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        'Dim SettingsFile As String = My.Computer.FileSystem.ReadAllText(appData & "\Diabotical\Settings.txt")
        Dim HudSettings As String = ""
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
            If SenderName = "ExportToFile" Then
                WriteHudFile(HudSettings)
            Else
                ShowHudJSON(HudSettings)
            End If
        Else
            StatusText1.Text = "Could not find Diabolical settings."
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
            StatusText1.Text = "Saved HUD settings to " & saveFileDialog1.FileName
        End If
    End Sub
    Private Sub ShowHudJSON(contents As String)
        OutputDisplayBox.Text = contents
        OutputDisplayBox.Focus()
        OutputDisplayBox.SelectAll()
    End Sub
    Private Sub CopyDhudJSON()
        OutputDisplayBox.Focus()
        OutputDisplayBox.SelectAll()
        OutputDisplayBox.Copy()
        StatusText1.Text = "Copied HUD settings to clipboard."
    End Sub
    Private Sub ShowExportPanel(sender As Object, e As RoutedEventArgs)
        BackupPanel.Visibility = Visibility.Collapsed
        ExportPanel.Visibility = Visibility.Visible
        ExportDhud(sender, e)
    End Sub
    Private Sub ShowBackupPanel(sender As Object, e As RoutedEventArgs)
        ExportPanel.Visibility = Visibility.Collapsed
        BackupPanel.Visibility = Visibility.Visible
    End Sub
    Private Sub BackupAllSettings(sender As Object, e As RoutedEventArgs)
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        Dim sourcePath = appData & "\Diabotical\Settings.txt"
        Dim saveFileDialog2 As New SaveFileDialog With {
            .Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            .FilterIndex = 1,
            .RestoreDirectory = True
        }

        If saveFileDialog2.ShowDialog() = True Then
            If File.Exists(sourcePath) = True Then
                File.Copy(sourcePath, saveFileDialog2.FileName)
                StatusText1.Text = "Backed up Settings.txt to: " & saveFileDialog2.FileName
            Else
                StatusText1.Text = "Could not find Diabotical Settings."
            End If
        End If
    End Sub
End Class
