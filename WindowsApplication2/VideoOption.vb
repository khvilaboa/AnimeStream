Imports System.Text.RegularExpressions

Public Class VideoOption
    Private audioLanguage As String
    Private subsLanguage As String
    Private uploadDate As String
    Private serverName As String
    Private videoData As String
    Private id As String

    Public Sub New(i As String, al As String, sl As String, ud As String, sn As String, vd As String)
        id = i
        audioLanguage = al
        subsLanguage = sl
        uploadDate = ud
        serverName = sn
        videoData = vd
    End Sub

    Public Function getAudioLanguage() As String
        Return audioLanguage
    End Function

    Public Function getSubsLanguage() As String
        Return subsLanguage
    End Function

    Public Function getUploadDate() As String
        Return uploadDate
    End Function

    Public Function getServerName() As String
        Return serverName
    End Function

    Public Function getVideoData() As String
        Return videoData
    End Function

    Public Function getDownloadUri() As Uri
        Dim uri As String = vbNull

        Select Case serverName
            Case "hyperion"
                Dim pattern = "key=(?<videoId>.*)&provider"
                Dim expr = New Regex(pattern, RegexOptions.IgnoreCase)
                Dim key = expr.Matches(videoData).Item(0).Groups("videoId").Value.Replace("%25", "%")

                uri = "http://animeflv.net/video/hyperion.php?key=" + key
            Case "videobam"
                'http:\/\/videobam.com\/widget\/pKovt\/custom\/773\
                'src=\"http:\/\/videobam.com\/widget\/pKovt\/custom\/773\" allowFullScreen><\/iframe>"]
                'MsgBox(videoData)
                Dim pattern = "src=\\""(?<videoUrl>.*)"".*>"
                Dim expr = New Regex(pattern, RegexOptions.IgnoreCase)
                Dim urlInt As String = expr.Matches(videoData).Item(0).Groups("videoUrl").Value.Replace("\", "")
                Dim code As String = Main.getSourceCode(urlInt)

                '{"scaling":"fit","url":"http:\/\/1.lw3.videobam.com\/storage\/1\/videos\/p\/pk\/pKovt\/low.encoded.mp4\/83a82ecb7303b60e3f63a60123868260\/55748953?ss=92","autoBuffering":false,"autoPlay":false,"bufferLength":"2"}]}
                pattern = ",{.*""url"":""(?<videoUrl>.*ss=[0-9]*)"""
                expr = New Regex(pattern, RegexOptions.IgnoreCase)
                uri = expr.Matches(code).Item(0).Groups("videoUrl").Value.Replace("\", "")
                uri = uri.Substring(0, uri.IndexOf("?"))
            Case "novamov"
                Dim pattern = "src=\\""(?<videoUrl>.*)"".*>"
                Dim expr = New Regex(pattern, RegexOptions.IgnoreCase)
                Dim urlInt As String = expr.Matches(videoData).Item(0).Groups("videoUrl").Value.Replace("\", "")
                Dim code As String = Main.getSourceCode(urlInt)

                Dim patternFile As String = "flashvars.file=""(?<file>.*)"";"
                Dim patternKey As String = "flashvars.filekey=""(?<key>.*)"";"

                Dim exprFile = New Regex(patternFile, RegexOptions.IgnoreCase)
                Dim exprKey = New Regex(patternKey, RegexOptions.IgnoreCase)

                Dim file = exprFile.Matches(code).Item(0).Groups("file").Value
                Dim key = exprKey.Matches(code).Item(0).Groups("key").Value

                Dim urlInt2 As String = "http://www.novamov.com/api/player.api.php?pass=undefined&cid=undefined&key=" + key + "&cid2=undefined&cid3=undefined&file=" + file + "&user=undefined"
                Dim code2 As String = Main.getSourceCode(urlInt2)

                'url=http://s142.mighycdndelivery.com/dl/ed1d14b6e13d4b2a998b71de05cdce08/55a7c655/ff91c752e7643d23f13afbe375dc290e00.flv&title=1318_1%26asdasdas&site_url
                Dim patternUrl As String = "url=(?<url>.*)&title"
                Dim exprUrl = New Regex(patternUrl, RegexOptions.IgnoreCase)
                uri = exprUrl.Matches(code2).Item(0).Groups("url").Value

            Case "nowvideo"
                Dim pattern = "src=\\""(?<videoUrl>.*)"".*>"
                Dim expr = New Regex(pattern, RegexOptions.IgnoreCase)
                Dim urlInt As String = expr.Matches(videoData).Item(0).Groups("videoUrl").Value.Replace("\", "")
                Dim code As String = Main.getSourceCode(urlInt)

                Dim patternFile As String = "flashvars.file=""(?<file>.*)"";"
                Dim patternKey As String = "fkzd=""(?<key>.*)"";"

                Dim exprFile = New Regex(patternFile, RegexOptions.IgnoreCase)
                Dim exprKey = New Regex(patternKey, RegexOptions.IgnoreCase)

                Dim file = exprFile.Matches(code).Item(0).Groups("file").Value
                Dim key = exprKey.Matches(code).Item(0).Groups("key").Value

                Dim urlInt2 As String = "http://www.nowvideo.sx/api/player.api.php?pass=undefined&cid=undefined&key=" + key + "&cid2=undefined&cid3=undefined&file=" + file + "&user=undefined"
                Dim code2 As String = Main.getSourceCode(urlInt2)

                Dim patternUrl As String = "url=(?<url>.*)&title"
                Dim exprUrl = New Regex(patternUrl, RegexOptions.IgnoreCase)
                uri = exprUrl.Matches(code2).Item(0).Groups("url").Value

            Case "videoweed"
                Dim pattern = "src=\\""(?<videoUrl>.*)"".*>"
                Dim expr = New Regex(pattern, RegexOptions.IgnoreCase)
                Dim urlInt As String = expr.Matches(videoData).Item(0).Groups("videoUrl").Value.Replace("\", "")
                Dim code As String = Main.getSourceCode(urlInt)

                Dim patternFile As String = "flashvars.file=""(?<file>.*)"";"
                Dim patternKey As String = "fkz=""(?<key>.*)"";"

                Dim exprFile = New Regex(patternFile, RegexOptions.IgnoreCase)
                Dim exprKey = New Regex(patternKey, RegexOptions.IgnoreCase)

                Dim file = exprFile.Matches(code).Item(0).Groups("file").Value
                Dim key = exprKey.Matches(code).Item(0).Groups("key").Value

                Dim urlInt2 As String = "http://www.videoweed.es/api/player.api.php?pass=undefined&cid=undefined&key=" + key + "&cid2=undefined&cid3=undefined&file=" + file + "&user=undefined"
                Dim code2 As String = Main.getSourceCode(urlInt2)

                Dim patternUrl As String = "url=(?<url>.*)&title"
                Dim exprUrl = New Regex(patternUrl, RegexOptions.IgnoreCase)
                uri = exprUrl.Matches(code2).Item(0).Groups("url").Value
        End Select

        Return New Uri(uri)
    End Function

End Class
