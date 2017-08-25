Imports System.ServiceProcess

Public Class Form1

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
#If DEBUG Then
        'While debugging this section is used.
        Dim myService As Service1 = New Service1()
        myService.DebugStart()
        System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite)

#Else
        Dim myService As Service1 = New Service1()

        Dim ServicesToRun As ServiceBase()
        ServicesToRun = New ServiceBase() {New Service1()}
        ServiceBase.Run(ServicesToRun)
#End If






    End Sub
End Class