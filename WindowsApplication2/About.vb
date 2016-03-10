Public Class About

    Private Sub pcbAbout_Click(sender As Object, e As EventArgs) Handles pcbAbout.Click
        Me.Hide()
    End Sub

    Private Sub pcbAbout_LostFocus(sender As Object, e As EventArgs) Handles pcbAbout.LostFocus
        Me.Hide()
    End Sub

    Private Sub About_Load(sender As Object, e As EventArgs) Handles Me.Load
        Me.Width = pcbAbout.Width
        Me.Height = pcbAbout.Height
    End Sub

    Private Sub About_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus
        Me.Hide()
    End Sub
End Class