Imports System.IO
Imports System.Globalization
Imports System
Imports System.Timers


Public Class Service1


    Private m_objJob As clsStartAllWorker

    Public Sub DebugStart()
        Me.OnStart(Nothing)
        'MsgBox("debug mode")


        While True
            Threading.Thread.Sleep(1000)
        End While
    End Sub

    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.
        My.Application.ChangeCulture("en-US")
        My.Application.Culture.DateTimeFormat.ShortDatePattern = "yyyy/MM/dd"
        My.Application.Culture.DateTimeFormat.LongDatePattern = "yyyy/MM/dd"



        modGlobalVariable.Initialize()



        m_objJob = New clsStartAllWorker
        m_objJob.StartAll()
    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.
        m_objJob.StopAll()
    End Sub

End Class
