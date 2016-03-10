Imports System.IO

Public Class Preferences
    Private downloadsDir As String   ' Para guardar los animes
    Private nameFormat As String     ' Formato del nombre de los archivos

    Private serverName As Integer
    Private finishAction As Integer
    Private exitAction As Integer
    Private colDims As String

    Private path As String

    Public Sub New(filePath As String)
        path = filePath

        If File.Exists(path) Then
            fromFile()
        Else
            downloadsDir = Environment.GetEnvironmentVariable("userprofile") + "\AnimeStream\" 'MOD
            nameFormat = "%s %c"
            serverName = 0
            exitAction = 0
            finishAction = 0
            colDims = "40,15,45"
            toFile()
        End If

    End Sub

    Public Sub toFile()
        Dim prefsFile As New StreamWriter(path) ' dataDir + "prefs.cfg"
        prefsFile.WriteLine("DownloadsDir=" + downloadsDir)
        prefsFile.WriteLine("Format=" + nameFormat)
        prefsFile.WriteLine("ColDims=" + colDims)
        prefsFile.WriteLine("ServerName=" + serverName.ToString())
        prefsFile.WriteLine("FinishAction=" + finishAction.ToString())
        prefsFile.WriteLine("ExitAction=" + exitAction.ToString())
        prefsFile.Close()
    End Sub

    Private Sub fromFile()
        Dim prefsFile As New StreamReader(path)
        Dim line As String = prefsFile.ReadLine()

        While Not line Is Nothing
            Dim attrib As String = Mid(line, 1, InStr(line, "=") - 1)
            Dim value As String = Mid(line, InStr(line, "=") + 1)

            Select Case attrib
                Case "DownloadsDir"
                    If Directory.Exists(value) Then
                        downloadsDir = value
                    End If
                Case "Format"
                    If Not InStr(value, "%c") = 0 Then
                        nameFormat = value
                    End If
                Case "ServerName"
                    If IsNumeric(value) AndAlso InStr(value, ".") = 0 AndAlso Integer.Parse(value) <= 4 Then
                        serverName = Integer.Parse(value)
                    End If
                Case "ExitAction"
                    If IsNumeric(value) AndAlso InStr(value, ".") = 0 AndAlso Integer.Parse(value) <= 1 Then
                        exitAction = Integer.Parse(value)
                    End If
                Case "FinishAction"
                    If IsNumeric(value) AndAlso InStr(value, ".") = 0 AndAlso Integer.Parse(value) <= 2 Then
                        finishAction = Integer.Parse(value)
                    End If
                Case "ColDims"
                    If validColDims(value) Then
                        colDims = value
                    End If
            End Select

            line = prefsFile.ReadLine()
        End While

        prefsFile.Close()

        If downloadsDir = "" Then
            downloadsDir = Environment.GetEnvironmentVariable("userprofile") + "\AnimeStream\"
        End If

        If nameFormat = "" Then
            nameFormat = "%s %c"
        End If

        If colDims = "" Then
            colDims = "40,15,45"
        End If

    End Sub

    Private Function validColDims(strDims As String) As Boolean
        Dim dims As String() = strDims.Split(",")

        If dims.Count <> 3 Then Return False

        For Each d In dims
            If Not IsNumeric(d) Then Return False
        Next

        Return True
    End Function

    Public Function getDownloadsDir() As String
        Return downloadsDir
    End Function

    Public Function getNameFormat() As String
        Return nameFormat
    End Function

    Public Function getServerName() As String
        Return serverName
    End Function

    Public Function getExitAction() As String
        Return exitAction
    End Function

    Public Function getFinishAction() As String
        Return finishAction
    End Function

    Public Function getColDims() As String
        Return colDims
    End Function

    Public Sub setDownloadsDir(downloadsDir As String)
        Me.downloadsDir = downloadsDir
    End Sub

    Public Sub setNameFormat(nameFormat As String)
        Me.nameFormat = nameFormat
    End Sub

    Public Sub setServerName(serverName As String)
        Me.serverName = serverName
    End Sub

    Public Sub setExitAction(action As String)
        Me.exitAction = action
    End Sub

    Public Sub setFinishAction(action As String)
        Me.finishAction = action
    End Sub

    Public Sub setColDims(colDims As String)
        Me.colDims = colDims
    End Sub

End Class