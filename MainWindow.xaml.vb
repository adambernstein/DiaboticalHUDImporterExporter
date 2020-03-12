Imports System.Environment
Imports System.Text.RegularExpressions


Class MainWindow
    Private Sub ExportDhud(sender As Object, e As RoutedEventArgs)
        Dim fileReader As String
        Dim appData As String = GetFolderPath(SpecialFolder.ApplicationData)
        'Dim HudDefinitionRegex As New Regex("[\t ](?<w>((774)|(Bos)|(Das))[a-z0-9]*)[\t ]", RegexOptions.ExplicitCapture Or RegexOptions.IgnoreCase Or RegexOptions.Compiled)
        StatusText1.Text = "Exporting...."
        fileReader = My.Computer.FileSystem.ReadAllText(appData & "\Diabotical\Settings.txt")
        TextFileContent.Text = fileReader
    End Sub
End Class
