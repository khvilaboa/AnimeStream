<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Config
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Config))
        Me.cmbServer = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cmbExit = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmbFinish = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnFormatHelp = New System.Windows.Forms.Button()
        Me.txtFormat = New System.Windows.Forms.TextBox()
        Me.txtLocalFile = New System.Windows.Forms.TextBox()
        Me.btnLocalFile = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.btnConfigCancel = New System.Windows.Forms.Button()
        Me.btnConfigAcept = New System.Windows.Forms.Button()
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'cmbServer
        '
        Me.cmbServer.FormattingEnabled = True
        Me.cmbServer.Items.AddRange(New Object() {"Hyperion", "Videobam", "Novamov", "Nowvideo", "VideoWeed"})
        Me.cmbServer.Location = New System.Drawing.Point(77, 70)
        Me.cmbServer.Name = "cmbServer"
        Me.cmbServer.Size = New System.Drawing.Size(182, 21)
        Me.cmbServer.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 73)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(49, 13)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Servidor:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cmbExit)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Controls.Add(Me.cmbServer)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.cmbFinish)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.btnFormatHelp)
        Me.GroupBox2.Controls.Add(Me.txtFormat)
        Me.GroupBox2.Controls.Add(Me.txtLocalFile)
        Me.GroupBox2.Controls.Add(Me.btnLocalFile)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.Label3)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(276, 161)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Descargas"
        '
        'cmbExit
        '
        Me.cmbExit.FormattingEnabled = True
        Me.cmbExit.Items.AddRange(New Object() {"Guardar descargas activas", "Preguntar siempre", "No guardar descagas activas"})
        Me.cmbExit.Location = New System.Drawing.Point(77, 124)
        Me.cmbExit.Name = "cmbExit"
        Me.cmbExit.Size = New System.Drawing.Size(182, 21)
        Me.cmbExit.TabIndex = 33
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(13, 127)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(40, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Al salir:"
        '
        'cmbFinish
        '
        Me.cmbFinish.FormattingEnabled = True
        Me.cmbFinish.Items.AddRange(New Object() {"No hacer nada", "Apagar el equipo"})
        Me.cmbFinish.Location = New System.Drawing.Point(77, 97)
        Me.cmbFinish.Name = "cmbFinish"
        Me.cmbFinish.Size = New System.Drawing.Size(182, 21)
        Me.cmbFinish.TabIndex = 29
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(13, 100)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(59, 13)
        Me.Label4.TabIndex = 32
        Me.Label4.Text = "Al terminar:"
        '
        'btnFormatHelp
        '
        Me.btnFormatHelp.Location = New System.Drawing.Point(230, 44)
        Me.btnFormatHelp.Name = "btnFormatHelp"
        Me.btnFormatHelp.Size = New System.Drawing.Size(29, 23)
        Me.btnFormatHelp.TabIndex = 31
        Me.btnFormatHelp.Text = "?"
        Me.btnFormatHelp.UseVisualStyleBackColor = True
        '
        'txtFormat
        '
        Me.txtFormat.Location = New System.Drawing.Point(77, 44)
        Me.txtFormat.Name = "txtFormat"
        Me.txtFormat.Size = New System.Drawing.Size(147, 20)
        Me.txtFormat.TabIndex = 30
        Me.txtFormat.Text = "%s %c"
        '
        'txtLocalFile
        '
        Me.txtLocalFile.Location = New System.Drawing.Point(77, 18)
        Me.txtLocalFile.Name = "txtLocalFile"
        Me.txtLocalFile.Size = New System.Drawing.Size(147, 20)
        Me.txtLocalFile.TabIndex = 28
        '
        'btnLocalFile
        '
        Me.btnLocalFile.Location = New System.Drawing.Point(230, 18)
        Me.btnLocalFile.Name = "btnLocalFile"
        Me.btnLocalFile.Size = New System.Drawing.Size(29, 23)
        Me.btnLocalFile.TabIndex = 29
        Me.btnLocalFile.Text = "..."
        Me.btnLocalFile.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(14, 47)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 13)
        Me.Label2.TabIndex = 27
        Me.Label2.Text = "Formato:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(13, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Destino:"
        '
        'btnConfigCancel
        '
        Me.btnConfigCancel.Location = New System.Drawing.Point(224, 179)
        Me.btnConfigCancel.Name = "btnConfigCancel"
        Me.btnConfigCancel.Size = New System.Drawing.Size(64, 23)
        Me.btnConfigCancel.TabIndex = 31
        Me.btnConfigCancel.Text = "Cancelar"
        Me.btnConfigCancel.UseVisualStyleBackColor = True
        '
        'btnConfigAcept
        '
        Me.btnConfigAcept.Location = New System.Drawing.Point(154, 179)
        Me.btnConfigAcept.Name = "btnConfigAcept"
        Me.btnConfigAcept.Size = New System.Drawing.Size(64, 23)
        Me.btnConfigAcept.TabIndex = 32
        Me.btnConfigAcept.Text = "Aceptar"
        Me.btnConfigAcept.UseVisualStyleBackColor = True
        '
        'Config
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(303, 212)
        Me.Controls.Add(Me.btnConfigAcept)
        Me.Controls.Add(Me.btnConfigCancel)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Config"
        Me.Text = "Configuración"
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents cmbServer As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnConfigCancel As System.Windows.Forms.Button
    Friend WithEvents btnConfigAcept As System.Windows.Forms.Button
    Friend WithEvents btnFormatHelp As System.Windows.Forms.Button
    Friend WithEvents txtFormat As System.Windows.Forms.TextBox
    Friend WithEvents txtLocalFile As System.Windows.Forms.TextBox
    Friend WithEvents btnLocalFile As System.Windows.Forms.Button
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents cmbFinish As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cmbExit As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
