Public Class Config

    Private downloadsDir As String
    Private prefs As Preferences

    Public Sub New(prefs As Preferences)

        ' Llamada necesaria para el diseñador.
        InitializeComponent()

        cmbServer.SelectedIndex = prefs.getServerName()
        cmbExit.SelectedIndex = prefs.getExitAction()
        cmbFinish.SelectedIndex = prefs.getFinishAction()
        txtFormat.Text = prefs.getNameFormat()
        txtLocalFile.Text = prefs.getDownloadsDir()

        btnConfigAcept.Select()

        Me.prefs = prefs

    End Sub

    Private Sub btnLocalFile_Click(sender As Object, e As EventArgs) Handles btnLocalFile.Click
        If (fbd.ShowDialog() = DialogResult.OK) Then
            txtLocalFile.Text = fbd.SelectedPath + "\"
            downloadsDir = fbd.SelectedPath + "\"
        End If
    End Sub

    Private Sub btnFormatHelp_Click(sender As Object, e As EventArgs) Handles btnFormatHelp.Click

        Dim helpMessage As String = "Formato del nombre de los capítulos:" + vbCrLf +
                             "%s   Nombre de la serie" + vbCrLf +
                             "%c   Número de capítulo" + vbCrLf + vbCrLf +
                             "Ejemplos:" + vbCrLf +
                             "%s %c           =>   Death Note 1.mp4" + vbCrLf +
                             "%s Cap %c   =>   Death Note Cap 1.mp4" + vbCrLf

        MsgBox(helpMessage, MsgBoxStyle.Information, Me.Text)
    End Sub

    Private Sub btnConfigCancel_Click(sender As Object, e As EventArgs) Handles btnConfigCancel.Click

        Me.Hide()

        txtFormat.Text = prefs.getNameFormat()
        txtLocalFile.Text = prefs.getDownloadsDir()

        cmbServer.SelectedIndex = prefs.getServerName()
        cmbFinish.SelectedIndex = prefs.getFinishAction()
        cmbExit.SelectedIndex = prefs.getExitAction()
    End Sub

    Private Sub btnConfigAcept_Click(sender As Object, e As EventArgs) Handles btnConfigAcept.Click

        Me.Hide()

        prefs.setNameFormat(txtFormat.Text)
        prefs.setDownloadsDir(txtLocalFile.Text)

        prefs.setServerName(cmbServer.SelectedIndex)
        prefs.setFinishAction(cmbFinish.SelectedIndex)
        prefs.setExitAction(cmbExit.SelectedIndex)

        prefs.toFile()
    End Sub

    Private Sub Config_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        e.Cancel = True
    End Sub


End Class