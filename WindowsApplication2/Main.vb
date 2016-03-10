Imports System.Text.RegularExpressions
Imports System.Net
Imports System.IO
Imports System.Deployment
Imports System.Threading
Imports System.ComponentModel


Public Class Main

    Class Search
        Dim key As String
        Dim results As New Dictionary(Of Integer, List(Of AnimeInfo))
        Dim numPags As Integer
        Dim index As Integer

        Dim running As Boolean

        Dim lock As New Object()

        Dim form As Main

        Private Delegate Sub Deleg(res As List(Of AnimeInfo))

        Public Sub New(form As Main)
            Me.form = form
        End Sub

        Public Sub search(key As String, numPags As Integer)
            Me.key = key
            Me.numPags = numPags
            index = 1
            results.Clear()
            running = True


            For i = 1 To numPags
                Dim hilo = New Thread(AddressOf searchThread)
                hilo.Start(i)
            Next
        End Sub

        Private Sub searchThread(i As Integer)
            Try
                Dim searchKey = Me.key
                Dim code As String = getSourceCode("http://www.animeflv.net/animes/?buscar=" + key + "&p=" + i.ToString())
                Dim resPage As List(Of AnimeInfo) = getAnimeList(code)

                If searchKey = Me.key Then
                    processResults(i, resPage)
                End If
            Catch e As Exception
                If Me.running Then
                    running = False
                    MsgBox("Error al conectar con el servidor. Comprueba tu conexión a internet y vuelve a intentarlo.", MsgBoxStyle.Exclamation, "AnimeStream")
                End If
            End Try
        End Sub

        Private Sub processResults(i As Integer, res As List(Of AnimeInfo))

            If i = index Then
                addResult(res)

                SyncLock lock
                    index += 1
                End SyncLock

                If i <> numPags Then
                    Dim j As Integer = index

                    While results.Keys.Contains(j) And j <= numPags
                        addResult(results.Item(j))
                        index += 1
                        j += 1
                    End While
                End If
            Else
                results.Add(i, res)
            End If

        End Sub


        Delegate Sub addResultCallback(res As List(Of AnimeInfo))

        Public Sub addResult(res As List(Of AnimeInfo))

            If form.lvwAnimeInfo.InvokeRequired Then
                Dim d As New addResultCallback(AddressOf addResult)
                form.Invoke(d, New Object() {res})
            Else
                For Each item As AnimeInfo In res
                    Dim itemInfo As String() = New String() {item.getName(), item.getAnimeType(), item.getGenres()}
                    form.lvwAnimeInfo.Items.Add(New ListViewItem(itemInfo))
                    form.results.Add(item)
                Next
            End If
        End Sub

        Private Function getAnimeList(code As String) As List(Of AnimeInfo)
            Dim pattern As String = "href=""(?<url>.*)"" title=""(?<title>.*)""><img class=.*>\n.*class=""tipo_(?<type>.*)""><.*\n.*\n.*Generos:</b> <a (?<genres>.*)</div>\n.*sinopsis"">(?<summary>(.*\n)?(.*\n)?(.*\n)?(.*\n)?(.*\n)?(.*\n)?)</div>" ' TODO
            Dim expr As New Regex(pattern, RegexOptions.IgnoreCase)
            Dim dic As New List(Of AnimeInfo)
            Dim genresList As String
            Dim type As String = "-"

            code = Mid(code, 1, InStr(code, "<div class=""clear""></div>"))

            For Each Coincidencia As Match In expr.Matches(code)
                'MsgBox(Coincidencia.Groups("genres").Value)
                genresList = vbNullString
                For Each item In Coincidencia.Groups("genres").Value.Split("</a>, <a ")
                    If InStr(item, "href") Then
                        genresList += item.Substring(InStrRev(item, ">")) + ", "
                    End If
                Next
                genresList = Mid(genresList, 1, Len(genresList) - 2)
                If genresList = vbNullString Then genresList = "---"

                Select Case Coincidencia.Groups("type").Value
                    Case "0"
                        type = "Anime"
                    Case "1"
                        type = "OVA"
                    Case "2"
                        type = "Película"
                End Select

                dic.Add(New AnimeInfo("", removeHTMLCodes(Coincidencia.Groups("title").Value), Coincidencia.Groups("url").Value, type, removeHTMLCodes(genresList), removeHTMLCodes(Coincidencia.Groups("summary").Value.Replace("</div>", "")), -1, -1))
            Next

            Return dic
        End Function

        Private Function removeHTMLCodes(str As String) As String
            str = str.Replace("&aacute;", "á")
            str = str.Replace("&eacute;", "é")
            str = str.Replace("&iacute;", "í")
            str = str.Replace("&oacute;", "ó")
            str = str.Replace("&uacute;", "ú")
            str = str.Replace("&ntilde;", "ñ")
            str = str.Replace("&quot;", """")
            str = str.Replace("&ldquo;", """")
            str = str.Replace("&rdquo;", """")
            str = str.Replace("&lsquo;", "'")
            str = str.Replace("&rsquo;", "'")
            str = str.Replace("&iexcl;", "¡")
            str = str.Replace("&iquest;", "¿")
            str = str.Replace("&hellip;", "...")
            str = str.Replace("&dagger;", " ")

            str = str.Replace("<br/>", "")
            str = str.Replace("<b>", "")
            str = str.Replace("</b>", "")

            Return str
        End Function

        Function getSourceCode(url As String) As String

            Dim code As String = vbNullString

            Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
            Dim response As System.Net.HttpWebResponse = request.GetResponse()
            Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
            code = sr.ReadToEnd()

            Return code
        End Function
    End Class

#Region "VARIABLES"

    ' Información del programa
    Const AppTitle As String = "AnimeStream"
    Const Version As String = "1.0"

    Const CapsHelpText As String = "Ejemplo: 1,5-8"

    ' Directorios
    Dim dataDir As String        ' Para guardar los datos del programa

    Dim results As New List(Of AnimeInfo)  ' Para almacenar resultados de búsqueda
    Dim videoOptions As New List(Of VideoOption)  ' Para almacenar opciones de servidores

    Dim currentAnime As AnimeInfo

    ' Control de la descarga activa
    Dim downloadTotalBytes As Long = 0
    Dim lastBytes As Long
    Dim lastUpdate As Date

    Dim preferences As Preferences
    Dim configForm As Config
    Dim downloadsForm As Descargas
    Dim aboutForm As About

    Dim dh As DownloadHandler

    Dim closeOrder As Boolean = False

    ' CONCURRENT SEARCH
    Dim searchKey As String
    Dim searchTmpResults As Dictionary(Of Integer, List(Of AnimeInfo))
    Dim searchIndex As Integer

    Delegate Sub reactivateDownloadsCallback()

    Dim searcher As New Search(Me)
#End Region

#Region "EVENT HANDLERS"

    Private Sub Main_Activated(sender As Object, e As EventArgs) Handles Me.Activated
        dh.fromXML(dataDir)
    End Sub

    Private Sub Main_Click(sender As Object, e As EventArgs) Handles Me.Click
        dh.fromXML(dataDir)
    End Sub

    Private Sub Main_DoubleClick(sender As Object, e As EventArgs) Handles Me.DoubleClick
        dh.toXML(dataDir)
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'checkUpdates()

        Me.Text = AppTitle

        Dim appdata As String = Environment.GetEnvironmentVariable("appdata")
        dataDir = appdata + "\AnimeStream\"

        If Not Directory.Exists(dataDir) Then
            Directory.CreateDirectory(dataDir)
        End If

        Dim configFile As String = dataDir + "prefs.cfg"
        preferences = New Preferences(configFile)
        configForm = New Config(preferences)

        aboutForm = New About()
        downloadsForm = New Descargas()
        dh = New DownloadHandler(downloadsForm, Me, preferences)
        downloadsForm.setHandler(dh)
        'downloadsForm.Show()

        If Not Directory.Exists(preferences.getDownloadsDir()) Then
            Directory.CreateDirectory(preferences.getDownloadsDir())
        End If

        txtEpisode.Text = CapsHelpText
        txtEpisode.ForeColor = Color.Gray

        setColDims(preferences.getColDims())

        txtSearch.Select() ' focus



    End Sub



    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing

        Me.Hide()

        'ntiTray.BalloonTipText = "Aplicación en segundo plano"
        ntiTray.Visible = True
        ntiTray.ShowBalloonTip(500, "AnimeStream", "La ejecución continúa en segundo plano", ToolTipIcon.Info)

        If Not closeOrder Then e.Cancel = True
    End Sub

    Private Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        lvwAnimeInfo.Items.Clear()
        results.Clear()

        searcher.search(txtSearch.Text, 3)
    End Sub

    Private Sub btnDownload_Click(sender As Object, e As EventArgs) Handles btnDownload.Click

        If lblInfo.Text = "" Then
            MsgBox("Debe seleccionarse el anime que se desea descargar.", MsgBoxStyle.Information, "AnimeStream")
            txtSearch.Focus()
            Return
        End If

        If txtEpisode.Text = "" OrElse txtEpisode.Text = CapsHelpText Then
            MsgBox("Deben seleccionarse los episodios a descargar.", MsgBoxStyle.Information, "AnimeStream")
            txtEpisode.Focus()
            Return
        End If

        Dim seq As Sequence = New Sequence(txtEpisode.Text, currentAnime.getLastEp())
        If seq.toString() = "" Then
            MsgBox("Los episodios seleccionados no son válidos.", MsgBoxStyle.Information, "AnimeStream")
            Return
        End If

        seq = dh.checkCollisions(currentAnime, seq)

        If seq.toString() = "" Then
            MsgBox("Los episodios seleccionados ya pertenecen a una descarga activa", MsgBoxStyle.Information, "AnimeStream")
            Return
        End If


        animInitDownload()
        dh.add(currentAnime, seq)

        'downloadsForm.Show()

    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = 13 Then
            e.Handled = True
            btnSearch_Click(btnSearch, Nothing)
        End If

        e.Handled = True
    End Sub

    Private Sub txtEpisode_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtEpisode.KeyPress

        Dim lastChar = Mid(txtEpisode.Text, IIf(Len(txtEpisode.Text) > 0, Len(txtEpisode.Text), 1), 1)

        If Not (Char.IsNumber(e.KeyChar) Or
                Char.IsControl(e.KeyChar) Or
                lastChar <> "-" And lastChar <> "," And lastChar <> "" And (e.KeyChar = "-" Or e.KeyChar = ",")) Then
            e.Handled = True
        End If
    End Sub

    'Private Sub txtEpisodeFin_KeyPress(sender As Object, e As KeyPressEventArgs)
    '    If Not (Char.IsNumber(e.KeyChar) Or Char.IsControl(e.KeyChar)) Then
    '        If (e.KeyChar = "l" Or e.KeyChar = "L") Then
    '            txtEpisodeFin.Text = listLastEp
    '        End If

    '        e.Handled = True
    '    End If/
    'End Sub

    Private Sub DescargarTodoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DescargarTodoToolStripMenuItem.Click
        If lblInfo.Text = "" Then
            MsgBox("Debes seleccionar la serie a descargar.", MsgBoxStyle.Information, AppTitle)
        Else
            txtEpisode.Text = "1-" + Convert.ToString(currentAnime.getLastEp())
            btnDownload_Click(btnDownload, Nothing)
        End If
    End Sub

#End Region


#Region "SPECIFIC METHODS"

    Function getAnimeList(code As String) As List(Of AnimeInfo)
        Dim pattern As String = "href=""(?<url>.*)"" title=""(?<title>.*)""><img class=.*>\n.*class=""tipo_(?<type>.*)""><.*\n.*\n.*Generos:</b> <a (?<genres>.*)</div>\n.*sinopsis"">(?<summary>.*)</div>"
        Dim expr As New Regex(pattern, RegexOptions.IgnoreCase)
        Dim dic As New List(Of AnimeInfo)
        Dim genresList As String
        Dim type As String = "-"

        For Each Coincidencia As Match In expr.Matches(code)
            'MsgBox(Coincidencia.Groups("genres").Value)
            genresList = vbNullString
            For Each item In Coincidencia.Groups("genres").Value.Split("</a>, <a ")
                If InStr(item, "href") Then
                    genresList += item.Substring(InStrRev(item, ">")) + ", "
                End If
            Next
            'MsgBox(genresList)

            Select Case Coincidencia.Groups("type").Value
                Case "0"
                    type = "Anime"
                Case "1"
                    type = "OVA"
                Case "2"
                    type = "Película"
            End Select

            dic.Add(New AnimeInfo("", removeHTMLCodes(Coincidencia.Groups("title").Value), Coincidencia.Groups("url").Value, type, removeHTMLCodes(genresList), removeHTMLCodes(Coincidencia.Groups("summary").Value), -1, -1))
        Next

        Return dic
    End Function

    Private Function validInput() As Boolean
        If lblInfo.Text = "" Then
            MsgBox("Debes seleccionar la serie a descargar.", MsgBoxStyle.Information, AppTitle)
            Return False
        ElseIf txtEpisode.Text = "" Then
            MsgBox("Debes especificar los capítulos a descargar.", MsgBoxStyle.Information, AppTitle)
            Return False
            'ElseIf Not Directory.Exists(txtLocalFile.Text) Then
            '    MsgBox("La carpeta de descargas seleccionada no es válida.", MsgBoxStyle.Information, Me.Text)
            '    Return False
            'ElseIf InStr(txtFormat.Text, "%c") = 0 Then
            '    MsgBox("El formato del nombre de archivos debe contener el identificador de número de capítulo (%s)", MsgBoxStyle.Information, Me.Text)
            '    Return False
        End If

        Return True
    End Function

#End Region

#Region "AUXILIAR METHODS"
    Function getSourceCode(url As String) As String
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
        Dim response As System.Net.HttpWebResponse = request.GetResponse()
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
        Dim code As String = sr.ReadToEnd()

        Return code
    End Function

    Function getFirstOcurrence(code As String, pattern As String, group As String) As String
        Dim expr As New Regex(pattern, RegexOptions.IgnoreCase)
        Return expr.Matches(code).Item(0).Groups(group).Value
    End Function

    Private Function removeHTMLCodes(str As String) As String
        str = str.Replace("&aacute;", "á")
        str = str.Replace("&eacute;", "é")
        str = str.Replace("&iacute;", "í")
        str = str.Replace("&oacute;", "ó")
        str = str.Replace("&uacute;", "ú")
        str = str.Replace("&ntilde;", "ñ")
        str = str.Replace("&quot;", """")
        str = str.Replace("&ldquo;", """")
        str = str.Replace("&rdquo;", """")
        str = str.Replace("&lsquo;", "'")
        str = str.Replace("&rsquo;", "'")
        str = str.Replace("&iexcl;", "¡")
        str = str.Replace("&iquest;", "¿")
        str = str.Replace("&hellip;", "...")
        str = str.Replace("&dagger;", " ")

        str = str.Replace("<br/>", "")
        str = str.Replace("<b>", "")
        str = str.Replace("</b>", "")

        Return str
    End Function


    Private Function vScrollNeeded(txtBox As TextBox) As Boolean

        Dim f As Font = txtBox.Font
        Dim rect As Rectangle = txtBox.ClientRectangle
        Dim charFitted As Integer
        Dim linesFitted As Integer
        Using g As Graphics = txtBox.CreateGraphics()
            Dim sf As New StringFormat(StringFormatFlags.NoWrap)
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = StringAlignment.Near
            sf.Trimming = StringTrimming.EllipsisCharacter
            sf.FormatFlags = StringFormatFlags.DirectionVertical
            g.MeasureString(txtBox.Text, f, rect.Size, sf, charFitted, linesFitted)
        End Using

        Return charFitted < txtBox.Text.Length

    End Function
#End Region

#Region "ACCESS METHODS"

#End Region

#Region "UPDATE"

    Public Sub checkUpdates()

        'ApplicationDeployment updateCheck = ApplicationDeployment.CurrentDeployment;
        'UpdateCheckInfo info = updateCheck.CheckForDetailedUpdate();
        '//
        'If (info.UpdateAvailable) Then
        '{
        '  updateCheck.Update();
        '  MessageBox.Show("The application has been upgraded, and will now restart.");
        '  Application.Restart();
        '}


        Dim info As Application.UpdateCheckInfo = Nothing

        If (Application.ApplicationDeployment.IsNetworkDeployed) Then

            Dim AD As Application.ApplicationDeployment = Application.ApplicationDeployment.CurrentDeployment

            Try
                info = AD.CheckForDetailedUpdate()
            Catch dde As Application.DeploymentDownloadException

            Catch ioe As InvalidOperationException

            End Try

            'MsgBox(info.UpdateAvailable)
            Return

            If (info.UpdateAvailable) Then

                Try
                    AD.Update()
                Catch dde As Application.DeploymentDownloadException
                    MsgBox(dde)
                End Try

            End If

        End If
    End Sub

#End Region


    Private Sub txtEpisode_GotFocus(sender As Object, e As EventArgs) Handles txtEpisode.GotFocus
        If txtEpisode.Text = CapsHelpText Then
            txtEpisode.Text = ""
        End If

        txtEpisode.ForeColor = Color.Gray
    End Sub

    Private Sub txtEpisode_LostFocus(sender As Object, e As EventArgs) Handles txtEpisode.LostFocus
        If txtEpisode.Text = "" Then
            txtEpisode.Text = CapsHelpText
        End If
    End Sub

    Private Sub tsmConfig_Click(sender As Object, e As EventArgs) Handles tsmConfig.Click
        configForm.ShowDialog()
        configForm.BringToFront()
    End Sub

    Private Sub lvwAnimeInfo_MouseUp(sender As Object, e As MouseEventArgs) Handles lvwAnimeInfo.MouseUp

        If e.Button = Windows.Forms.MouseButtons.Right And lvwAnimeInfo.SelectedItems.Count > 0 Then
            cmsAnime.Show(sender, New Point(e.X, e.Y))
        End If
    End Sub


    Private Sub lvwAnimeInfo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvwAnimeInfo.SelectedIndexChanged
        If (lvwAnimeInfo.SelectedItems.Count = 0) Then
            Exit Sub
        End If

        Dim index As Integer = lvwAnimeInfo.Items.IndexOf(lvwAnimeInfo.SelectedItems.Item(0))
        txtSummary.Text = results(index).getSummary()

        Dim url As String = "http://animeflv.net" + results.Item(index).getUrl()
        Dim code As String = getSourceCode(url)


        Dim patternSummary As String = "<div class=""sinopsis"">(?<summary>(.*\n)?(.*\n)?(.*\n)?(.*\n)?(.*\n)?(.*\n)(.*\n)?(.*\n)?(.*\n)?(.*\n)?(.*\n)?(.*\n)?)([ \t]*\n)?([ \t]*)?<ul class=""ainfo"">"
        Dim exprSummary As New Regex(patternSummary, RegexOptions.IgnoreCase)
        Dim summary As String = removeHTMLCodes(exprSummary.Matches(code).Item(0).Groups("summary").Value.Replace("</div>", ""))
        txtSummary.Text = summary
        txtSummary_TextChanged(Nothing, Nothing)

        Dim epUrl As String = getFirstOcurrence(code, ";""><li><a href=""(?<id>[./a-zA-Z0-9-]*)"">", "id")
        Dim lastEp As String = Mid(epUrl, InStrRev(epUrl, "-") + 1, InStrRev(epUrl, ".html") - (InStrRev(epUrl, "-") + 1))
        Dim id As String = Mid(epUrl, 1, InStrRev(epUrl, "-"))
        Dim name As String = lvwAnimeInfo.SelectedItems.Item(0).SubItems.Item(0).Text.Replace(":", " ") ' MOD
        'Dim url As String = results.Item(lstResults.Items.Item(lstResults.SelectedIndex))
        Dim numEps As Integer


        Dim animeState As String = getFirstOcurrence(code, "serie_estado_."">(?<state>.*)</span>", "state").Replace("&oacute;", "ó") 'MOD
        lblInfo.Text = "Estado: " + animeState


        If animeState = "Finalizada" Then
            lblInfo.Text += vbCrLf + "Número de capitulos: " + lastEp
            numEps = lastEp
        Else
            lblInfo.Text += vbCrLf + "Último capítulo: " + lastEp
            epUrl = getFirstOcurrence(code, "Proximos Episodios.*href=""(?<id>[./a-zA-Z0-9-]*)"">[^<>]*</a></li></ul>", "id")
            numEps = Mid(epUrl, InStrRev(epUrl, "-") + 1, InStrRev(epUrl, ".html") - (InStrRev(epUrl, "-") + 1))
            lblInfo.Text += vbCrLf + "Número de capitulos: " + numEps.ToString()
        End If

        'videoOptions.Clear()
        'Dim hilo As New Thread(AddressOf getOptionsList)
        'hilo.Start()

        ' Descargar la imagen si no está ya descargada
        Dim patternImg As String = "<img.*src=""(?<image>.*)"" w.*class=""portada"".?/>"
        Dim exprImg As New Regex(patternImg, RegexOptions.IgnoreCase)
        Dim imgUrl As String = exprImg.Matches(code).Item(0).Groups("image").Value
        Dim imgName As String = Mid(imgUrl, InStrRev(imgUrl, "/") + 1)  ' Basename

        If Not File.Exists(dataDir + imgName) Then
            My.Computer.Network.DownloadFile(imgUrl, dataDir + imgName)
        End If

        imgFront.ImageLocation = dataDir + imgName

        currentAnime = New AnimeInfo(id, name, epUrl, "", "", summary, Integer.Parse(lastEp), Integer.Parse(numEps))


    End Sub

    Private Sub Main_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        imgFront.Left = Me.Width - imgFront.Width - 30
        lblInfo.Left = Me.Width - lblInfo.Width - 30
        btnSearch.Left = Me.Width - btnSearch.Width - 30
        txtSearch.Left = imgFront.Left
        txtSearch.Width = imgFront.Width - btnSearch.Width - 5
        lvwAnimeInfo.Width = Me.Width - imgFront.Width - lvwAnimeInfo.Left - 30 - 10
        btnDownload.Left = Me.Width - btnDownload.Width - 30
        lblEpisode.Left = imgFront.Left
        txtEpisode.Left = lblEpisode.Left + lblEpisode.Width
        txtEpisode.Width = imgFront.Width - btnDownload.Width - lblEpisode.Width - 4
        txtSummary.Left = imgFront.Left

        lvwAnimeInfo.Height = Me.Height - lvwAnimeInfo.Top - 50
        Dim dimSummary As Integer = (lvwAnimeInfo.Height - imgFront.Height - 5 - lblInfo.Height - 5 - txtEpisode.Height - 8) * 0.75
        txtSummary.Height = dimSummary

        'MsgBox(porc)
        'MsgBox(txtSummary.Text)

        Dim offset = (lvwAnimeInfo.Height - imgFront.Height - 5 - lblInfo.Height - 5 - txtEpisode.Height - 8 - dimSummary) * 0.25
        imgFront.Top = lvwAnimeInfo.Top + offset  ' espacio sobrante * 1/4

        lblInfo.Top = imgFront.Top + imgFront.Height + 5
        txtSummary.Top = lblInfo.Top + lblInfo.Height + 5

        txtEpisode.Top = txtSummary.Top + txtSummary.Height + 8
        btnDownload.Top = txtSummary.Top + txtSummary.Height + 7
        lblEpisode.Top = txtSummary.Top + txtSummary.Height + 11

        If Not preferences Is Nothing Then setColDims(preferences.getColDims()) ' resize < load
        txtSummary_TextChanged(Nothing, Nothing) ' scrollbar
    End Sub


    Private Sub tsmDownloads_Click(sender As Object, e As EventArgs) Handles tsmDownloads.Click
        downloadsForm.Show()
        downloadsForm.BringToFront()
    End Sub

    Private Sub ntiTray_BalloonTipClicked(sender As Object, e As EventArgs) Handles ntiTray.BalloonTipClicked
        dh.cancelShutDown()
    End Sub

    Private Sub ntiTray_BalloonTipClosed(sender As Object, e As EventArgs) Handles ntiTray.BalloonTipClosed
        If Me.Visible = True Then
            ntiTray.Visible = False
        End If
    End Sub

    Private Sub ntiTray_MouseDoubleClick(sender As Object, e As MouseEventArgs) Handles ntiTray.MouseDoubleClick
        Me.Show()
        ntiTray.Visible = False
    End Sub

    Public Sub notifyDownloadFinished(name As String)
        ntiTray.Visible = True
        ntiTray.ShowBalloonTip(500, "AnimeStream", "Descarga Finalizada: " + name + ".", ToolTipIcon.Info)
    End Sub

    Public Sub notifyShutDown()
        ntiTray.Visible = True
        ntiTray.ShowBalloonTip(1000, "AnimeStream", "Descargas terminadas. El equipo se apagará en 20 segundos (click aqui para cancelar).", ToolTipIcon.Info)
    End Sub

    Public Sub notifyCancelShutDown()
        ntiTray.Visible = True
        ntiTray.ShowBalloonTip(500, "AnimeStream", "El apagado del equipo se ha cancelado correctamente.", ToolTipIcon.Info)
    End Sub

    Private Function rotateBitmap(bm_in As Bitmap) As Bitmap

        ' Make an array of points defining the
        ' image's corners.
        Dim wid As Single = bm_in.Width
        Dim hgt As Single = bm_in.Height
        Dim corners As Point() = { _
            New Point(0, 0), _
            New Point(wid, 0), _
            New Point(0, hgt), _
            New Point(wid, hgt)}

        ' Translate to center the bounding box at the origin.
        Dim cx As Single = wid / 2
        Dim cy As Single = hgt / 2
        Dim i As Long
        For i = 0 To 3
            corners(i).X -= cx
            corners(i).Y -= cy
        Next i

        ' Rotate.
        Dim theta As Single = Single.Parse(45) * Math.PI / 180.0
        Dim sin_theta As Single = Math.Sin(theta)
        Dim cos_theta As Single = Math.Cos(theta)
        Dim X As Single
        Dim Y As Single
        For i = 0 To 3
            X = corners(i).X
            Y = corners(i).Y
            corners(i).X = X * cos_theta + Y * sin_theta
            corners(i).Y = -X * sin_theta + Y * cos_theta
        Next i

        ' Translate so X >= 0 and Y >=0 for all corners.
        Dim xmin As Single = corners(0).X
        Dim ymin As Single = corners(0).Y
        For i = 1 To 3
            If xmin > corners(i).X Then xmin = corners(i).X
            If ymin > corners(i).Y Then ymin = corners(i).Y
        Next i
        For i = 0 To 3
            corners(i).X -= xmin
            corners(i).Y -= ymin
        Next i

        ' Create an output Bitmap and Graphics object.
        Dim bm_out As New Bitmap(CInt(-2 * xmin), CInt(-2 * ymin))
        Dim gr_out As Graphics = Graphics.FromImage(bm_out)

        ' Drop the last corner lest we confuse DrawImage, 
        ' which expects an array of three corners.
        ReDim Preserve corners(2)

        ' Draw the result onto the output Bitmap.
        gr_out.DrawImage(bm_in, corners)

        ' Display the result.
        Return bm_out
    End Function


    Private Sub animInitDownload()
        Dim pcbAnim As New PictureBox()
        pcbAnim.Visible = True
        pcbAnim.SizeMode = PictureBoxSizeMode.Zoom
        pcbAnim.BackColor = Color.Transparent
        pcbAnim.Parent = Me
        pcbAnim.ImageLocation = imgFront.ImageLocation
        pcbAnim.Top = imgFront.Top
        pcbAnim.Left = imgFront.Left
        pcbAnim.Width = imgFront.Width
        pcbAnim.Height = imgFront.Height
        Me.Controls.Add(pcbAnim)
        pcbAnim.BringToFront()

        Dim i As Integer, incL As Integer, incT As Integer, incW As Integer, incH As Integer
        Dim nIter As Integer = 50
        incL = imgFront.Left - mnsToolbar.Items(0).Size.Width - 14
        incT = imgFront.Top - 5
        incH = imgFront.Height - mnsToolbar.Items(0).Size.Height
        incW = imgFront.Width - imgFront.Width * (mnsToolbar.Items(0).Size.Height / imgFront.Height)

        For i = 0 To nIter
            pcbAnim.Top = imgFront.Top - (incT * i) / nIter
            pcbAnim.Left = imgFront.Left - (incL * i) / nIter
            pcbAnim.Width = imgFront.Width - (incW * i) / nIter
            pcbAnim.Height = imgFront.Height - (incH * i) / nIter
            System.Windows.Forms.Application.DoEvents()
        Next

        Me.Controls.Remove(pcbAnim)
    End Sub

    Private Sub tsmInfo_Click(sender As Object, e As EventArgs) Handles tsmInfo.Click

        About.Show()
        About.Top = Me.Top + Me.Height / 2 - About.Height / 2
        About.Left = Me.Left + Me.Width / 2 - About.Width / 2
        About.BringToFront()
    End Sub

    Private Sub openDownloadsFolder()
        Shell("explorer.exe root = " + preferences.getDownloadsDir(), vbNormalFocus)
    End Sub

    Private Sub tsmFolder_Click(sender As Object, e As EventArgs) Handles tsmFolder.Click
        Dim hilo As New Thread(AddressOf openDownloadsFolder)
        hilo.Start()
    End Sub


    Private Sub ExitToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem1.Click
        preferences.setColDims(getColDims())
        preferences.toFile()

        If preferences.getExitAction() = 0 And dh.numActiveDownloads() > 0 Then
            dh.toXML(dataDir)
        End If

        closeOrder = True
        Me.Close()
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Show()
        ntiTray.Visible = False
    End Sub

    Private Sub setColDims(wStr As String)
        Dim widths As String() = wStr.Split(",")
        Dim i As Integer

        For i = 0 To lvwAnimeInfo.Columns.Count - 1
            lvwAnimeInfo.Columns(i).Width = (lvwAnimeInfo.Width * Integer.Parse(widths(i))) / 100
        Next

    End Sub

    Private Function getColDims() As String

        Dim toret As String = vbNullString
        Dim dims(lvwAnimeInfo.Columns.Count - 1) As Integer
        Dim lastDim As Integer = 100

        For i = 0 To lvwAnimeInfo.Columns.Count - 2
            dims(i) = (lvwAnimeInfo.Columns(i).Width / lvwAnimeInfo.Width) * 100
            toret += dims(i).ToString() + ","
        Next

        For i = 0 To lvwAnimeInfo.Columns.Count - 2
            lastDim -= dims(i)
        Next

        Return toret + lastDim.ToString()

    End Function

    Private Sub txtSummary_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtSummary.KeyPress
        e.Handled = True
    End Sub

    Private Sub txtSummary_TextChanged(sender As Object, e As EventArgs) Handles txtSummary.TextChanged
        txtSummary.ScrollBars = IIf(vScrollNeeded(txtSummary) = True, ScrollBars.Vertical, ScrollBars.None)
    End Sub


End Class
