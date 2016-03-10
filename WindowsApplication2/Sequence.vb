
Public Class Sequence

    Dim elems As New List(Of Integer)
    Dim pointer As Integer = 0
    Dim max As Integer = Integer.MaxValue

    Public Sub New(max As Integer)
        Me.max = max
    End Sub

    Public Sub New(seq As String)
        Me.New(seq, Integer.MaxValue)
    End Sub

    Public Sub New(seq As String, max As Integer)

        Dim ranges() As String = seq.Split(",")

        For Each range As String In ranges
            If range = vbNullString Then Continue For

            If InStr(range, "-") = 0 Then
                If Not elems.Contains(Integer.Parse(range)) And Integer.Parse(range) <= max Then
                    elems.Add(Integer.Parse(range))
                End If
            Else
                Dim ini As Integer = Mid(range, 1, InStr(range, "-") - 1)
                Dim fin As Integer = If(Mid(range, InStr(range, "-") + 1) = "", ini, Mid(range, InStr(range, "-") + 1))

                If ini > fin Then Continue For

                Dim i As Integer = ini

                While i <= Math.Min(fin, max)
                    If Not elems.Contains(i) Then
                        elems.Add(i)
                    End If

                    i += 1
                End While
            End If
        Next

        elems.Sort()
        Me.max = max
    End Sub

    Public Sub add(elem As Integer)
        If elem <= max Then elems.Add(elem)
    End Sub

    Public Sub incrementPointer()
        pointer = pointer + 1
    End Sub

    Public Function length() As Integer
        Return elems.Count
    End Function

    Public Function index() As Integer
        Return pointer
    End Function

    Public Function value() As Integer
        Return elems.Item(pointer)
    End Function

    Public Function finished() As Boolean
        Return pointer >= elems.Count
    End Function

    Public Overrides Function toString() As String
        Dim buffer As New List(Of Integer)
        Dim res As String = ""
        Dim last As Integer = Integer.MaxValue

        For Each item As Integer In elems
            If (buffer.Count > 0 And last < item - 1) Then
                res += bufferToString(buffer)
                buffer.Clear()
            End If

            buffer.Add(item)
            last = item
        Next

        If buffer.Count > 0 Then
            res += bufferToString(buffer)
        End If

        If res <> "" Then
            res = Mid(res, 1, Len(res) - 2)
        End If

        Return res
    End Function

    Private Function bufferToString(buffer As List(Of Integer)) As String
        Dim res As String = vbNullString

        If buffer.Count <= 2 Then
            For Each bItem As Integer In buffer
                res += bItem.ToString() + ", "
            Next
        Else
            res += buffer.Item(0).ToString() + "-" + buffer.Item(buffer.Count - 1).ToString() + ", "
        End If

        Return res
    End Function

    Public Sub reset()
        pointer = 0
    End Sub

    Public Sub remove(val As Integer)
        elems.Remove(val)
    End Sub

    Public Function contains(val As Integer) As Boolean
        Return elems.Contains(val)
    End Function
End Class
