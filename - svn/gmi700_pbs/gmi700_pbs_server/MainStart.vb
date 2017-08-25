Public Class MainStart





    Public Sub New()
#If DEBUG Then
        'While debugging this section is used.
        Dim myService As Service1 = New Service1()
        myService.DebugStart()
        System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite)

#Else

        ServiceBase[] ServicesToRun;
        ServicesToRun = new ServiceBase[] 
        { 
            new Service1() 
        };
        ServiceBase.Run(ServicesToRun);
#End If
    End Sub
End Class
