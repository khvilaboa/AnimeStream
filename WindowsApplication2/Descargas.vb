Imports System.Net

Public Class Descargas

    Private handler As DownloadHandler

    Private Sub Descargas_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        Me.Hide()
        e.Cancel = True
    End Sub

    Private Sub Descargas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        pnlDownloads.AutoSize = True
        pnlDownloads.MaximumSize = New Size(pnlDownloads.Width, pnlDownloads.Height)
    End Sub

    Private Sub GroupBox_Highlight(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim gb As GroupBox = CType(sender, GroupBox)

        gb.BackColor = Color.Blue


    End Sub

    Private Sub GroupBox_Lowlight(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim gb As GroupBox = CType(sender, GroupBox)

        gb.BackColor = Color.White

    End Sub

    Private Sub Descargas_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        pnlDownloads.MaximumSize = New Size(Me.ClientRectangle.Size.Width, Me.ClientRectangle.Size.Height)

        pnlDownloads.Width = Me.ClientRectangle.Size.Width
        pnlDownloads.Height = Me.ClientRectangle.Size.Height

        If Not handler Is Nothing Then handler.resizeItems()
    End Sub

    Public Sub setHandler(dh As DownloadHandler)
        handler = dh
    End Sub


    Private Sub LimpiarToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LimpiarToolStripMenuItem.Click
        handler.clean()
    End Sub
End Class