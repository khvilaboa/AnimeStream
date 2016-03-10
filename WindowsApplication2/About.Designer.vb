<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class About
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(About))
        Me.pcbAbout = New System.Windows.Forms.PictureBox()
        CType(Me.pcbAbout, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pcbAbout
        '
        Me.pcbAbout.Image = CType(resources.GetObject("pcbAbout.Image"), System.Drawing.Image)
        Me.pcbAbout.Location = New System.Drawing.Point(0, 0)
        Me.pcbAbout.Name = "pcbAbout"
        Me.pcbAbout.Size = New System.Drawing.Size(400, 180)
        Me.pcbAbout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.pcbAbout.TabIndex = 0
        Me.pcbAbout.TabStop = False
        '
        'About
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(400, 178)
        Me.Controls.Add(Me.pcbAbout)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "About"
        Me.Text = "About"
        CType(Me.pcbAbout, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pcbAbout As System.Windows.Forms.PictureBox
End Class
