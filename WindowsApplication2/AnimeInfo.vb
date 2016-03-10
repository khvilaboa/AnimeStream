Public Class AnimeInfo
    Private id As String
    Private name As String
    Private url As String
    Private type As String
    Private genres As String
    Private summary As String
    Private lastEp As Integer
    Private numEps As Integer

    Public Sub New(id As String, name As String, url As String, type As String, genres As String, summary As String, lastEp As Integer, numEps As Integer)
        Me.id = id
        Me.name = name
        Me.url = url
        Me.type = type
        Me.genres = genres
        Me.summary = summary
        Me.lastEp = lastEp
        Me.numEps = numEps
    End Sub

    Public Function getId() As String
        Return id
    End Function

    Public Function getName() As String
        Return name
    End Function

    Public Function getUrl() As String
        Return url
    End Function

    Public Function getLastEp() As Integer
        Return lastEp
    End Function

    Public Function getNumEps() As Integer
        Return numEps
    End Function

    Public Function getGenres() As String
        Return genres
    End Function

    Public Function getSummary() As String
        Return summary
    End Function

    Public Function getAnimeType() As String
        Return type
    End Function

End Class
