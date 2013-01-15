Imports System.Collections.Generic
Imports System.Text

Public Class Main
    Public Shared Sub Main()
        'Inkorrekte Argumente
        If My.Application.CommandLineArgs.Count <> 1 Then
            'Falsche Anzahl der Argumente
            Console.WriteLine("Benutzung: dhltrack [Delivery-ID]")
        Else
            'The joy of working with modern languages...
            Dim id As String = My.Application.CommandLineArgs(0)
            'Building up the URL...
            Dim url As String = "http://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=de&idc=" & id

            Dim wc As New System.Net.WebClient()
            wc.Encoding = UTF8Encoding.UTF8
            Dim htmldata As String = wc.DownloadString(url)

            If htmldata.Contains("<div class=""error"">") Then
                'DHL gibt bei nicht vorhandener ID den Error in dieser CSS-Klasse heraus.
                'Leider nicht vorhanden.
                Console.WriteLine("Es ist keine Sendung mit der ID " & id & " bekannt!")
            Else
                'Status der Sendung extrahieren -- evtl. wäre hier ein RegExp besser... 
                Dim status As String = Split(Split(htmldata, "<td class=""status"">")(1), "</td>")(0).Replace("<div class=""statusZugestellt"">", "").Replace("</div>", "").Trim()
                Console.WriteLine("Status der Sendung mit ID: " & id)
                Console.WriteLine(status)
            End If
        End If

    End Sub
End Class
