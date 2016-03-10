Imports System.Net
Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Threading
Imports System.Xml

Public Class DownloadHandler

    Public Class Download

        Private Const TOP_MARGIN As Integer = 20
        Private Const LEFT_MARGIN As Integer = 20

        Private Const LINE1_TOP_OFFSET As Integer = 0
        Private Const STATUS_TOP_OFFSET As Integer = 20
        Private Const LINE2_TOP_OFFSET As Integer = 40

        Private prbStatus As New ProgressBar
        Private lblDownloadInfo As New Label
        Private downloadInfo As String ' texto original sin formatear
        Private lblSizeInfo As New Label
        Private lblTimeInfo As New Label
        Private pcbDownActions As New PictureBox

        Private info As AnimeInfo
        Private seq As Sequence

        Private progress As DownloadProgress

        Private index As Integer = -1
        Private downForm As Descargas

        Private status As String
        Private handler As DownloadHandler

        Private noDownloaded As Sequence
        Private videoOption As VideoOption = Nothing

        Public Sub New(form As Descargas, dh As DownloadHandler, index As Integer, ai As AnimeInfo, seq As Sequence)
            Me.info = ai
            Me.seq = seq
            Me.downForm = form
            Me.handler = dh

            'Dim downInfo As String = ai.getName() + " : " + "Cap. " + Convert.ToString(seq.value())
            'If seq.length() > 1 Then downInfo += " (" + (seq.index() + 1).ToString + "/" + (seq.length()).ToString + ")"
            'lblDownloadInfo.Text = downInfo
            'lblSizeInfo.Text = "Iniciando descarga..."

            lblDownloadInfo.Font = New Font("Microsoft Sans Serif", 12, FontStyle.Regular, GraphicsUnit.Pixel)

            lblSizeInfo.ForeColor = Color.DarkSlateGray
            lblSizeInfo.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel)
            lblTimeInfo.ForeColor = Color.DarkSlateGray
            lblTimeInfo.Font = New Font("Microsoft Sans Serif", 10, FontStyle.Regular, GraphicsUnit.Pixel)

            lblDownloadInfo.AutoSize = True
            lblSizeInfo.AutoSize = True
            lblTimeInfo.AutoSize = True

            pcbDownActions.Width = 15
            pcbDownActions.Height = 15
            pcbDownActions.SizeMode = PictureBoxSizeMode.Zoom
            AddHandler pcbDownActions.Click, AddressOf pcbDownActions_Click

            form.pnlDownloads.Controls.Add(lblDownloadInfo)
            form.pnlDownloads.Controls.Add(prbStatus)
            form.pnlDownloads.Controls.Add(lblSizeInfo)
            form.pnlDownloads.Controls.Add(lblTimeInfo)
            form.pnlDownloads.Controls.Add(pcbDownActions)

            setPosition(index)

            noDownloaded = New Sequence(ai.getLastEp())
        End Sub

        Public Sub setPosition(index As Integer)
            lblDownloadInfo.Left = LEFT_MARGIN
            lblDownloadInfo.Top = TOP_MARGIN + index * 70 + LINE1_TOP_OFFSET

            prbStatus.SetBounds(LEFT_MARGIN, TOP_MARGIN + index * 70 + STATUS_TOP_OFFSET, downForm.pnlDownloads.ClientRectangle.Size.Width - 2 * LEFT_MARGIN - 20, 15)

            pcbDownActions.Left = prbStatus.Location.X + prbStatus.Width + 5
            pcbDownActions.Top = TOP_MARGIN + index * 70 + STATUS_TOP_OFFSET

            lblSizeInfo.Left = LEFT_MARGIN
            lblSizeInfo.Top = TOP_MARGIN + index * 70 + LINE2_TOP_OFFSET

            lblTimeInfo.Top = TOP_MARGIN + index * 70 + LINE2_TOP_OFFSET

            formatDownloadText()

            Me.index = index
        End Sub

        Public Sub dispose()
            downForm.pnlDownloads.Controls.Remove(lblDownloadInfo)
            downForm.pnlDownloads.Controls.Remove(prbStatus)
            downForm.pnlDownloads.Controls.Remove(lblSizeInfo)
            downForm.pnlDownloads.Controls.Remove(lblTimeInfo)
            downForm.pnlDownloads.Controls.Remove(pcbDownActions)
        End Sub

        Private Sub pcbDownActions_Click(sender As PictureBox, e As EventArgs)

            Select Case status
                Case "DOWNLOADING"
                    status = "INTERRUPTED"
                    prbStatus.Value = 0
                Case "INTERRUPTED"
                    status = "DOWNLOADING"
                    handler.reactivateDownload(Me)
                Case "COMPLETED"
                    Shell("explorer.exe root = " + handler.getDownDir() + info.getName(), vbNormalFocus)
            End Select

        End Sub

        Public Sub setDownloadText(str As String)
            downloadInfo = str
            formatDownloadText()
        End Sub

        Private Sub formatDownloadText()

            lblDownloadInfo.Text = downloadInfo

            If getTextWidth(lblDownloadInfo) > prbStatus.Width Then
                Dim capInfo As String = Mid(lblDownloadInfo.Text, InStrRev(lblDownloadInfo.Text, "("))
                Dim nameInfo As String = Mid(lblDownloadInfo.Text, 1, InStrRev(lblDownloadInfo.Text, "(") - 2)

                While getTextWidth(lblDownloadInfo) > prbStatus.Width
                    nameInfo = Mid(nameInfo, 1, InStrRev(nameInfo, " ") - 1) + "..."
                    lblDownloadInfo.Text = nameInfo + " " + capInfo
                End While
            End If

        End Sub

        Private Function getTextWidth(lbl As Label) As Long
            Dim f As Font = lbl.Font
            Dim size As SizeF
            Dim g As Graphics = lbl.CreateGraphics()

            size = g.MeasureString(lbl.Text, f)

            Return size.Width
        End Function

        Public Sub setSizeText(str As String)
            lblSizeInfo.Text = str
        End Sub

        Public Sub setTimeText(str As String)
            lblTimeInfo.Text = str
            lblTimeInfo.Left = prbStatus.Left + prbStatus.Width - lblTimeInfo.Width
        End Sub

        Public Sub showInitText()
            Dim downInfo As String = info.getName() + " (" + "Cap. " + Convert.ToString(seq.value()) + ")"
            If seq.length() > 1 Then downInfo += " (" + (seq.index() + 1).ToString + "/" + (seq.length()).ToString + ")"
            setDownloadText(downInfo)
            setTimeText("")
            pcbDownActions.Image = My.Resources.cross

            status = "DOWNLOADING"

            setSizeText("Esperando descarga...")
        End Sub


        Public Sub showFinalText()
            setDownloadText(info.getName() + " (" + seq.toString() + ")")
            setSizeText("Completada")
            setTimeText("")
            pcbDownActions.Image = My.Resources.folder5
            status = "COMPLETED"
        End Sub

        Public Sub showInterruptedText()
            setDownloadText(info.getName() + " (" + seq.toString() + ")")
            setSizeText("Interrumpida")
            setTimeText("")
            pcbDownActions.Image = My.Resources.retry
            status = "INTERRUPTED"
        End Sub

        Public Function getPosition() As Integer
            Return index
        End Function

        Public Sub setPosition(p As DownloadProgress)
            progress = p
        End Sub

        Public Function getSequence() As Sequence
            Return seq
        End Function

        Public Sub setProgressInfo(dp As DownloadProgress)
            progress = dp
        End Sub

        Public Function getProgressInfo() As DownloadProgress
            Return progress
        End Function

        Public Sub setProgress(val As Integer)
            prbStatus.Value = val
        End Sub

        Public Function getAnimeInfo() As AnimeInfo
            Return info
        End Function

        Public Function isInterrupted() As Boolean
            Return status = "INTERRUPTED"
        End Function

        Public Function isFinished() As Boolean
            Return status = "COMPLETED"
        End Function

        Public Sub resize()
            setPosition(index)
        End Sub

        Public Sub addUnsupportedCap()
            noDownloaded.add(seq.value())
        End Sub

        Public Function getUnsupportedCaps() As String
            Return noDownloaded.toString()
        End Function

        Public Sub setVideoOption(vo As VideoOption)
            videoOption = vo
        End Sub

        Public Function getVideoOption() As VideoOption
            Return videoOption
        End Function
    End Class

    '##################################################################

    Class DownloadProgress

        Private lastUpdate As Date
        Private lastBytes As Long
        Private totalBytes As Long

        Private Const NSAMPLES = 5

        Private speeds(NSAMPLES) As Double
        Private pointer As Integer
        Private validAverage As Boolean

        Public Sub New(lu As Date, lb As Long, tb As Long)
            lastUpdate = lu
            lastBytes = lb
            totalBytes = tb
            pointer = 0
            validAverage = False
        End Sub

        Public Function getLastBytes() As Long
            Return lastBytes
        End Function

        Public Sub setLastBytes(lb As Long)
            lastBytes = lb
        End Sub

        Public Function getLastUpdate() As Date
            Return lastUpdate
        End Function

        Public Sub setLastUpdate(d As Date)
            lastUpdate = d
        End Sub

        Public Function getTotalBytes() As Long
            Return totalBytes
        End Function

        Public Sub addSpeed(sp As Double)
            speeds(pointer) = sp
            pointer += 1

            If pointer >= NSAMPLES Then
                pointer = 0
                validAverage = True
            End If
        End Sub

        Public Function getAverageSpeed() As Double
            If validAverage Then
                Dim average As Double = 0
                For Each value As Double In speeds
                    average += value
                Next
                Return average / NSAMPLES
            Else
                Return vbNull
            End If
        End Function
    End Class

    '##################################################################

    Private numDownloads As Integer = 0
    Private downloads As New Dictionary(Of WebClient, Download)
    Private downForm As Descargas
    Private mainForm As Main
    Private prefs As Preferences

    Private shuttingDownThread As Thread = Nothing

    Dim optSupported As New List(Of String) From {"hyperion", "videobam", "novamov", "nowvideo", "videoweed"} ' "hyperion", "videobam", "novamov", "nowvideo", "videoweed"

    Public Sub New(downForm As Descargas, mainForm As Main, prefs As Preferences)
        Me.downForm = downForm
        Me.mainForm = mainForm
        Me.prefs = prefs

        'mainForm.notifyShutdown()
        'Dim hilo As New Thread(AddressOf shutdown)
        'hilo.Start(5)

    End Sub

    Public Sub add(ai As AnimeInfo, seq As Sequence)

        Dim wc As New WebClient
        Dim pb As New ProgressBar

        AddHandler wc.DownloadProgressChanged, AddressOf downloadProgressChanged
        AddHandler wc.DownloadFileCompleted, AddressOf downloadCompleted

        Dim contInactive As Integer = 0
        For Each di As Download In downloads.Values
            If di.isFinished() Or di.isInterrupted() Then
                contInactive += 1
            End If
        Next

        Dim index As Integer = downloads.Count - contInactive

        For Each di As Download In downloads.Values
            If di.getPosition() >= index Then
                di.setPosition(di.getPosition() + 1)
            End If
        Next

        Dim down As Download = New Download(downForm, Me, index, ai, seq)
        downloads.Add(wc, down)

        numDownloads += 1

        initDownload(wc, down)

    End Sub



#Region "WEBCLIENT PROGRESS CONTROL"
    Private Sub downloadProgressChanged(sender As WebClient, e As DownloadProgressChangedEventArgs)

        Dim down As Download = downloads.Item(sender)

        If down.isInterrupted() Then
            sender.CancelAsync()
            Return
        End If

        Dim now As Date = DateTime.Now
        Dim bytesIn As Double = Double.Parse(e.BytesReceived.ToString())
        Dim totalBytes As Double = Double.Parse(e.TotalBytesToReceive.ToString())
        Dim percentage As Double = bytesIn / totalBytes * 100
        Dim percentageStr As String = Int32.Parse(Math.Truncate(percentage)).ToString() + "%"

        Dim progress As DownloadProgress = down.getProgressInfo()

        If progress Is Nothing Then
            Dim pr As New DownloadProgress(now, bytesIn, totalBytes)
            down.setProgressInfo(pr)
            Return
        End If

        Dim timeSpan As TimeSpan = now - progress.getLastUpdate()
        down.setProgress(percentage)

        If Not timeSpan.Seconds = 0 Then

            Dim bytesChange As Long = bytesIn - progress.getLastBytes()
            Dim bytesPerSecond As Double = (bytesChange / (timeSpan.Seconds + timeSpan.Milliseconds / 1000))
            Dim size As String = readableSize(bytesIn) + " de " + readableSize(totalBytes)

            progress.addSpeed(bytesPerSecond)
            Dim avSpeed As Double = progress.getAverageSpeed()
            If avSpeed <> vbNull Then
                size += " (" + readableSize(avSpeed) + "/s)"
                Dim estimatedTime As Integer = (progress.getTotalBytes() - progress.getLastBytes()) / bytesPerSecond
                down.setTimeText(readableTime(estimatedTime))
            End If

            down.setSizeText(size)

            progress.setLastBytes(bytesIn)
            progress.setLastUpdate(now)
        End If


    End Sub


    Private Sub downloadCompleted(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)

        Dim down As Download = downloads.Item(sender)

        Dim downFile As String = prefs.getDownloadsDir() + down.getAnimeInfo().getName() + "\" + getFileName(down)
        Dim downloadedBytes As Long = If(File.Exists(downFile), My.Computer.FileSystem.GetFileInfo(downFile).Length, -1)
        Dim totalBytes As Long = If(Not down.getProgressInfo() Is Nothing, down.getProgressInfo().getTotalBytes(), -1) ' MOD
        down.setProgressInfo(Nothing)

        If down.isInterrupted() Then
            down.showInterruptedText()
            If File.Exists(downFile) Then My.Computer.FileSystem.DeleteFile(downFile)
            pushToBottomInt(down)
        ElseIf Not down.getVideoOption() Is Nothing AndAlso downloadedBytes <> totalBytes Then ' Damaged..
            down.setProgress(0)
            initDownload(sender, down)
        Else
            down.getSequence().incrementPointer()

            If Not down.getSequence().finished() Then
                down.setProgress(0)
                initDownload(sender, down)
            Else

                ' Descarga terminada!!!

                down.showFinalText()
                pushToBottomDown(down)

                If prefs.getFinishAction() = 1 And numActiveDownloads() = 0 Then ' TODO
                    mainForm.notifyShutDown()

                    If Not IsNothing(shuttingDownThread) Then
                        shuttingDownThread.Abort()
                        shuttingDownThread = Nothing
                    End If

                    shuttingDownThread = New Thread(AddressOf shutdown)
                    shuttingDownThread.Start(20)
                ElseIf prefs.getFinishAction() = 2 And numActiveDownloads() = 0 Then
                    Process.Start("shutdown.exe", " -l -t 0")
                Else
                    mainForm.notifyDownloadFinished(down.getAnimeInfo().getName())
                End If


                If Len(down.getUnsupportedCaps()) = 1 Then
                    MsgBox("No se han podido descargar el cap " + down.getUnsupportedCaps() + " de " + down.getAnimeInfo().getName() + "." _
                           , MsgBoxStyle.Exclamation, "AnimeStream")
                ElseIf Len(down.getUnsupportedCaps()) > 2 Then
                    MsgBox("No se han podido descargar los caps " + down.getUnsupportedCaps() + " de " + down.getAnimeInfo().getName() + "." _
                           , MsgBoxStyle.Exclamation, "AnimeStream")
                End If
                End If
        End If
    End Sub

#End Region

    Private Function initDownload(wc As WebClient, down As Download) As Boolean

        down.setVideoOption(Nothing)
        down.showInitText()
        Dim options As List(Of VideoOption)

        'Try
        Dim cap As String = down.getSequence().value().ToString()

        Dim url As String = "http://animeflv.net" + down.getAnimeInfo().getId() + cap + ".html"
        Dim code As String = getSourceCode(url)
        Dim i As Integer

        options = getOptionsList(code)

        If code.Contains("Error 404") Then
            'MsgBox("404")
            down.addUnsupportedCap()
            downloadCompleted(valueToKey(downloads, down), Nothing)
            Return False
        End If

        Dim optChosen As VideoOption = Nothing

        While i < options.Count AndAlso Not optSupported.Item(prefs.getServerName()) = options.Item(i).getServerName()
            If IsNothing(optChosen) AndAlso optSupported.Contains(options.Item(i).getServerName()) Then
                optChosen = options.Item(i)
            End If

            i += 1
        End While

        If i < options.Count Then
            optChosen = options.Item(i)
        End If

        ' Determina el servidor del que se descargará
        'While i < options.Count AndAlso Not optSupported.Contains(options.Item(i).getServerName())
        '    i += 1
        'End While

        If IsNothing(optChosen) Then
            'MsgBox("El capítulo " + cap + " no puede ser descargado (servidor no compatible).", MsgBoxStyle.Information)
            down.addUnsupportedCap()
            downloadCompleted(valueToKey(downloads, down), Nothing)
            Return False
        End If

        'MsgBox(optChosen.getServerName())

        down.setVideoOption(optChosen)
        Dim uri As Uri = optChosen.getDownloadUri()
        'Dim filename As String = prefs.getNameFormat().Replace("%s", down.getAnimeInfo().getName()).Replace("%c", down.getSequence().value()) + ".mp4"

        Dim downDirPath As String = prefs.getDownloadsDir() + down.getAnimeInfo().getName()

        If Not Directory.Exists(downDirPath) Then
            Directory.CreateDirectory(downDirPath)
        End If

        ' TEMP
        Dim downFilePath As String = downDirPath + "\" + getFileName(down)
        If File.Exists(downFilePath) Then File.Delete(downFilePath)

        wc.DownloadFileAsync(uri, downFilePath)

        Return True
    End Function

    Private Sub pushToBottomDown(down As Download)
        For Each di As Download In downloads.Values
            If di.getPosition() > down.getPosition() Then
                di.setPosition(di.getPosition() - 1)
            End If
        Next

        down.setPosition(downloads.Count - 1)
    End Sub

    Private Sub pushToBottomInt(down As Download)
        Dim cont As Integer = 0

        For Each di As Download In downloads.Values
            If di.isFinished() Then
                cont += 1
            End If
        Next

        Dim pos = downloads.Count - 1 - cont
        For Each di As Download In downloads.Values
            If di.getPosition() > down.getPosition() And di.getPosition() <= pos Then
                di.setPosition(di.getPosition() - 1)
            End If
        Next

        down.setPosition(pos)
    End Sub

    Private Function getOptionsList(code As String) As List(Of VideoOption)
        Dim results As New List(Of VideoOption)

        Dim pattern As String = "var videos = {(?<options>.*)}"
        Dim expr As New Regex(pattern, RegexOptions.IgnoreCase)
        Dim options As String = expr.Matches(code).Item(0).Groups("options").Value
        'MsgBox(options)

        Dim patternOp As String = """(?<id>.*)"":\[""(?<audioLan>.*)"",""(?<subsLan>.*)"",.*,.*,.*,.*,.*,.*,""(?<date>.*)"",""(?<server>.*)"",""(?<data>.*)"""
        Dim exprOp As New Regex(patternOp, RegexOptions.IgnoreCase)

        For Each op In options.Split("],")
            If op <> vbNullString Then
                ' i As String, al As String, sl As String, ud As String, sn As String, vd As String
                'MsgBox(exprOp.Matches(op).Item(0).Groups("id").Value + " <-> " + exprOp.Matches(op).Item(0).Groups("audioLan").Value + " <-> " + exprOp.Matches(op).Item(0).Groups("subsLan").Value + " <-> " + exprOp.Matches(op).Item(0).Groups("date").Value + " <-> " + exprOp.Matches(op).Item(0).Groups("server").Value + " <-> " + exprOp.Matches(op).Item(0).Groups("data").Value)
                results.Add(New VideoOption(exprOp.Matches(op).Item(0).Groups("id").Value,
                                            exprOp.Matches(op).Item(0).Groups("audioLan").Value,
                                            exprOp.Matches(op).Item(0).Groups("subsLan").Value,
                                            exprOp.Matches(op).Item(0).Groups("date").Value,
                                            exprOp.Matches(op).Item(0).Groups("server").Value,
                                            exprOp.Matches(op).Item(0).Groups("data").Value))
            End If
        Next

        Return results

    End Function

    Private Function getFileName(down As Download) As String
        Dim ext As String = ""
        If Not down.getVideoOption() Is Nothing Then
            Dim server As String = down.getVideoOption().getServerName()
            ext = If(server = "hyperion" Or server = "videobam", ".mp4", ".flv")
        End If

        Return prefs.getNameFormat().Replace("%s", down.getAnimeInfo().getName()).Replace("%c", down.getSequence().value()) + ext
    End Function

    Public Sub resizeItems()
        For Each item As Download In downloads.Values
            item.resize()
        Next
    End Sub

    Public Function getDownDir() As String
        Return prefs.getDownloadsDir()
    End Function

    Public Sub clean()
        Dim i As Integer = 0

        While i < downloads.Count
            Dim wc As WebClient = downloads.Keys(i)
            Dim d As Download = downloads.Values(i)

            If d.isFinished() Or d.isInterrupted() Then
                d.dispose()
                downloads.Remove(wc)
            Else
                i += 1
            End If

        End While
    End Sub

    Private Sub shutdown(sec As String)
        Thread.Sleep(sec * 1000)
        Process.Start("shutdown.exe", " -s -t 0")
    End Sub

    Public Sub cancelShutDown()
        If Not IsNothing(shuttingDownThread) Then
            shuttingDownThread.Abort()
            shuttingDownThread = Nothing

            Main.notifyCancelShutDown()
        End If
    End Sub

    Public Function numActiveDownloads() As Integer
        Dim cont As Integer = 0

        For Each d As Download In downloads.Values
            If Not d.isFinished() And Not d.isInterrupted() Then
                cont += 1
            End If
        Next

        Return cont
    End Function

    Public Function checkCollisions(ai As AnimeInfo, s As Sequence) As Sequence

        For Each down As Download In downloads.Values
            If ai.getName() = down.getAnimeInfo().getName() And Not down.isInterrupted() And Not down.isFinished() Then
                While Not s.finished()
                    If down.getSequence().contains(s.value()) Then
                        s.remove(s.value())
                    Else
                        s.incrementPointer()
                    End If
                End While
                s.reset()
            End If
        Next

        Return s
    End Function

    Public Sub toXML(path As String)
        Dim writer As New XmlTextWriter(path + "\downloads.xml", System.Text.Encoding.UTF8)
        writer.WriteStartDocument(True)
        writer.Formatting = Formatting.Indented
        writer.Indentation = 2
        writer.WriteStartElement("Downloads")

        For Each down In downloads.Values
            If Not down.isFinished() And Not down.isInterrupted() Then
                createNode(writer, down)
            End If
        Next

        writer.WriteEndElement()
        writer.WriteEndDocument()
        writer.Close()
    End Sub

    Private Sub createNode(ByVal writer As XmlTextWriter, down As Download)
        writer.WriteStartElement("Download")
        writer.WriteStartElement("Id")
        writer.WriteString(down.getAnimeInfo().getId())
        writer.WriteEndElement()
        writer.WriteStartElement("Name")
        writer.WriteString(down.getAnimeInfo().getName())
        writer.WriteEndElement()
        writer.WriteStartElement("Sequence")
        writer.WriteString(down.getSequence().toString())
        writer.WriteEndElement()
        writer.WriteStartElement("Pointer")
        writer.WriteString(down.getSequence().value())
        writer.WriteEndElement()
        writer.WriteEndElement()
    End Sub

    Public Sub fromXML(path As String)

        If File.Exists(path + "/downloads.xml") Then
            Dim reader As XmlReader = XmlReader.Create(path + "/downloads.xml")
            Dim id As String = "", name As String = "", seq As String = "", pointer As String = ""

            While reader.Read()

                If reader.IsStartElement() Then

                    If reader.Name = "Id" Then
                        reader.Read()
                        id = reader.Value.Trim()
                    ElseIf reader.Name = "Name" Then
                        reader.Read()
                        name = reader.Value.Trim()
                    ElseIf reader.Name = "Sequence" Then
                        reader.Read()
                        seq = reader.Value.Trim()
                    ElseIf reader.Name = "Pointer" Then
                        reader.Read()
                        pointer = reader.Value.Trim()

                        'MsgBox("Id: " + id + vbCrLf + "Name: " + name + vbCrLf + "Seq: " + seq + vbCrLf + "Pointer: " + pointer)

                        ' Create download
                        createDownload(id, name, seq, pointer)
                    End If
                End If
            End While

            reader.Close()
        End If
    End Sub

    Private Sub createDownload(id As String, name As String, strSeq As String, pointer As String)
        Dim ai As New AnimeInfo(id, name, "", "", "", "", -1, -1)
        Dim seq As New Sequence(strSeq)

        While seq.value() <> Integer.Parse(pointer)
            seq.incrementPointer()
        End While

        add(ai, seq)
    End Sub

#Region "AUXILIAR METHODS"
    Private Function getSourceCode(url As String) As String
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create(url)
        Dim response As System.Net.HttpWebResponse = request.GetResponse()
        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())
        Dim code As String = sr.ReadToEnd()

        Return code
    End Function

    Private Function readableSize(bytes As Double) As String
        Dim toret As String

        If (bytes < 1024) Then
            toret = (bytes).ToString("#.##") + " B"
        ElseIf (bytes < 1048576) Then
            toret = (bytes / 1024).ToString("#.##") + " kB"
        Else
            toret = (bytes / 1048576).ToString("#.##") + " MB"
        End If

        Return toret
    End Function

    Private Function readableTime(sec As Integer) As String
        Dim toret As String = "Restante: "

        If (sec < 6) Then
            toret = "Terminando"
        ElseIf (sec < 60) Then
            toret += sec.ToString() + " s"
        ElseIf (sec < 3600) Then
            toret += (sec / 60).ToString("#") + " min"
        ElseIf (sec < 86400) Then
            toret += (sec / 3600).ToString("#.#") + " h"
        Else
            toret += (sec / 86400).ToString("#.#") + " d"
        End If

        Return toret
    End Function

    Public Sub reactivateDownload(down As Download)

        Dim wc As WebClient = Nothing

        For Each w In downloads.Keys
            If downloads.Item(w).Equals(down) Then
                wc = w
            End If
        Next

        down.showInitText()
        If Not wc Is Nothing Then initDownload(wc, down)
    End Sub

    Private Function valueToKey(dic As Dictionary(Of WebClient, Download), down As Download) As WebClient
        For Each key In dic.Keys
            If dic.Item(key).Equals(down) Then
                Return key
            End If
        Next

        Return Nothing
    End Function

#End Region
End Class
