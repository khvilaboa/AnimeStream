<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Main
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Main))
        Me.fbd = New System.Windows.Forms.FolderBrowserDialog()
        Me.lblEpisode = New System.Windows.Forms.Label()
        Me.lblInfo = New System.Windows.Forms.Label()
        Me.cmsAnime = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DescargarTodoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnShowConfig = New System.Windows.Forms.Button()
        Me.txtEpisode = New System.Windows.Forms.TextBox()
        Me.AToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnsToolbar = New System.Windows.Forms.MenuStrip()
        Me.tsmConfig = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmDownloads = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmFolder = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsmInfo = New System.Windows.Forms.ToolStripMenuItem()
        Me.lvwAnimeInfo = New System.Windows.Forms.ListView()
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader2 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.txtSummary = New System.Windows.Forms.TextBox()
        Me.ntiTray = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.cmsTray = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ExitToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnSearch = New System.Windows.Forms.Button()
        Me.btnDownload = New System.Windows.Forms.Button()
        Me.imgFront = New System.Windows.Forms.PictureBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.cmsAnime.SuspendLayout()
        Me.mnsToolbar.SuspendLayout()
        Me.cmsTray.SuspendLayout()
        CType(Me.imgFront, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblEpisode
        '
        Me.lblEpisode.AutoSize = True
        Me.lblEpisode.Location = New System.Drawing.Point(414, 449)
        Me.lblEpisode.Name = "lblEpisode"
        Me.lblEpisode.Size = New System.Drawing.Size(55, 13)
        Me.lblEpisode.TabIndex = 6
        Me.lblEpisode.Text = "Episodios:"
        '
        'lblInfo
        '
        Me.lblInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblInfo.Location = New System.Drawing.Point(416, 289)
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(166, 53)
        Me.lblInfo.TabIndex = 17
        Me.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cmsAnime
        '
        Me.cmsAnime.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DescargarTodoToolStripMenuItem})
        Me.cmsAnime.Name = "cmsAnime"
        Me.cmsAnime.Size = New System.Drawing.Size(155, 26)
        '
        'DescargarTodoToolStripMenuItem
        '
        Me.DescargarTodoToolStripMenuItem.Image = Global.AnimeStream.My.Resources.Resources.downloadArrow
        Me.DescargarTodoToolStripMenuItem.Name = "DescargarTodoToolStripMenuItem"
        Me.DescargarTodoToolStripMenuItem.Size = New System.Drawing.Size(154, 22)
        Me.DescargarTodoToolStripMenuItem.Text = "Descargar todo"
        '
        'btnShowConfig
        '
        Me.btnShowConfig.Location = New System.Drawing.Point(487, 4)
        Me.btnShowConfig.Name = "btnShowConfig"
        Me.btnShowConfig.Size = New System.Drawing.Size(26, 23)
        Me.btnShowConfig.TabIndex = 23
        Me.btnShowConfig.Text = "C"
        Me.btnShowConfig.UseVisualStyleBackColor = True
        '
        'txtEpisode
        '
        Me.txtEpisode.Location = New System.Drawing.Point(469, 446)
        Me.txtEpisode.Name = "txtEpisode"
        Me.txtEpisode.Size = New System.Drawing.Size(81, 20)
        Me.txtEpisode.TabIndex = 5
        '
        'AToolStripMenuItem
        '
        Me.AToolStripMenuItem.Name = "AToolStripMenuItem"
        Me.AToolStripMenuItem.Size = New System.Drawing.Size(25, 20)
        Me.AToolStripMenuItem.Text = "a"
        '
        'mnsToolbar
        '
        Me.mnsToolbar.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.mnsToolbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsmConfig, Me.tsmDownloads, Me.tsmFolder, Me.tsmInfo})
        Me.mnsToolbar.Location = New System.Drawing.Point(0, 0)
        Me.mnsToolbar.Name = "mnsToolbar"
        Me.mnsToolbar.Size = New System.Drawing.Size(744, 32)
        Me.mnsToolbar.TabIndex = 24
        Me.mnsToolbar.Text = "MenuStrip1"
        '
        'tsmConfig
        '
        Me.tsmConfig.Image = Global.AnimeStream.My.Resources.Resources.config
        Me.tsmConfig.Name = "tsmConfig"
        Me.tsmConfig.Size = New System.Drawing.Size(36, 28)
        '
        'tsmDownloads
        '
        Me.tsmDownloads.Image = Global.AnimeStream.My.Resources.Resources.downloadArrow2
        Me.tsmDownloads.Name = "tsmDownloads"
        Me.tsmDownloads.Size = New System.Drawing.Size(36, 28)
        '
        'tsmFolder
        '
        Me.tsmFolder.Image = Global.AnimeStream.My.Resources.Resources.folder5
        Me.tsmFolder.Name = "tsmFolder"
        Me.tsmFolder.Size = New System.Drawing.Size(36, 28)
        '
        'tsmInfo
        '
        Me.tsmInfo.Image = Global.AnimeStream.My.Resources.Resources.info4
        Me.tsmInfo.Name = "tsmInfo"
        Me.tsmInfo.Size = New System.Drawing.Size(36, 28)
        '
        'lvwAnimeInfo
        '
        Me.lvwAnimeInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3})
        Me.lvwAnimeInfo.FullRowSelect = True
        Me.lvwAnimeInfo.Location = New System.Drawing.Point(12, 35)
        Me.lvwAnimeInfo.Name = "lvwAnimeInfo"
        Me.lvwAnimeInfo.Size = New System.Drawing.Size(392, 439)
        Me.lvwAnimeInfo.TabIndex = 25
        Me.lvwAnimeInfo.UseCompatibleStateImageBehavior = False
        Me.lvwAnimeInfo.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Nombre"
        Me.ColumnHeader1.Width = 119
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Tipo"
        Me.ColumnHeader2.Width = 84
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Géneros"
        Me.ColumnHeader3.Width = 185
        '
        'txtSearch
        '
        Me.txtSearch.Location = New System.Drawing.Point(417, 7)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(126, 20)
        Me.txtSearch.TabIndex = 3
        '
        'txtSummary
        '
        Me.txtSummary.Location = New System.Drawing.Point(416, 347)
        Me.txtSummary.Multiline = True
        Me.txtSummary.Name = "txtSummary"
        Me.txtSummary.Size = New System.Drawing.Size(166, 92)
        Me.txtSummary.TabIndex = 26
        '
        'ntiTray
        '
        Me.ntiTray.ContextMenuStrip = Me.cmsTray
        Me.ntiTray.Icon = CType(resources.GetObject("ntiTray.Icon"), System.Drawing.Icon)
        Me.ntiTray.Text = "AnimeStream"
        '
        'cmsTray
        '
        Me.cmsTray.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem, Me.ExitToolStripMenuItem1})
        Me.cmsTray.Name = "ContextMenuStrip1"
        Me.cmsTray.Size = New System.Drawing.Size(104, 48)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem.Text = "Open"
        '
        'ExitToolStripMenuItem1
        '
        Me.ExitToolStripMenuItem1.Name = "ExitToolStripMenuItem1"
        Me.ExitToolStripMenuItem1.Size = New System.Drawing.Size(103, 22)
        Me.ExitToolStripMenuItem1.Text = "Exit"
        '
        'btnSearch
        '
        Me.btnSearch.Image = Global.AnimeStream.My.Resources.Resources.search
        Me.btnSearch.Location = New System.Drawing.Point(549, 5)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(34, 23)
        Me.btnSearch.TabIndex = 4
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'btnDownload
        '
        Me.btnDownload.Image = Global.AnimeStream.My.Resources.Resources.play
        Me.btnDownload.Location = New System.Drawing.Point(554, 445)
        Me.btnDownload.Name = "btnDownload"
        Me.btnDownload.Size = New System.Drawing.Size(29, 21)
        Me.btnDownload.TabIndex = 7
        Me.btnDownload.UseVisualStyleBackColor = True
        '
        'imgFront
        '
        Me.imgFront.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.imgFront.ContextMenuStrip = Me.cmsAnime
        Me.imgFront.Location = New System.Drawing.Point(416, 35)
        Me.imgFront.Name = "imgFront"
        Me.imgFront.Size = New System.Drawing.Size(166, 250)
        Me.imgFront.TabIndex = 5
        Me.imgFront.TabStop = False
        '
        'Main
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(744, 491)
        Me.Controls.Add(Me.txtSummary)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.btnSearch)
        Me.Controls.Add(Me.lvwAnimeInfo)
        Me.Controls.Add(Me.mnsToolbar)
        Me.Controls.Add(Me.btnShowConfig)
        Me.Controls.Add(Me.lblInfo)
        Me.Controls.Add(Me.btnDownload)
        Me.Controls.Add(Me.txtEpisode)
        Me.Controls.Add(Me.lblEpisode)
        Me.Controls.Add(Me.imgFront)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.mnsToolbar
        Me.MinimumSize = New System.Drawing.Size(760, 530)
        Me.Name = "Main"
        Me.Text = "AnimeStream"
        Me.cmsAnime.ResumeLayout(False)
        Me.mnsToolbar.ResumeLayout(False)
        Me.mnsToolbar.PerformLayout()
        Me.cmsTray.ResumeLayout(False)
        CType(Me.imgFront, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents fbd As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents imgFront As System.Windows.Forms.PictureBox
    Friend WithEvents lblEpisode As System.Windows.Forms.Label
    Friend WithEvents btnDownload As System.Windows.Forms.Button
    Friend WithEvents lblInfo As System.Windows.Forms.Label
    Friend WithEvents cmsAnime As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DescargarTodoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnShowConfig As System.Windows.Forms.Button
    Friend WithEvents txtEpisode As System.Windows.Forms.TextBox
    Friend WithEvents AToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnsToolbar As System.Windows.Forms.MenuStrip
    Friend WithEvents tsmInfo As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmConfig As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsmDownloads As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents lvwAnimeInfo As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents txtSearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents txtSummary As System.Windows.Forms.TextBox
    Friend WithEvents ntiTray As System.Windows.Forms.NotifyIcon
    Friend WithEvents tsmFolder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTray As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ExitToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ExitToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker

End Class
