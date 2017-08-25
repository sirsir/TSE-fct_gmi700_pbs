

Public Class clsPlcWrapper
    Inherits OMRON.Compolet.SYSMAC.SysmacCJ

#Region "Attributes"
    Private m_Mutex As Threading.Mutex
    Private m_strMutexName As String
#End Region

#Region "Properties"

#End Region

#Region "Constructor"
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region "Method"
    Public Sub InitializeAndConnect(ByVal net As Integer, ByVal node As Integer, ByVal unit As Integer, Optional ByVal mutexName As String = "")
        Me.NetworkAddress = CShort(net)
        Me.NodeAddress = CShort(node)
        Me.UnitAddress = CShort(unit)
        'Me.Active() = True
        'Me.Update()

        m_strMutexName = IIf(mutexName = String.Empty, "OMRON.Compolet.SYSMAC.SysmacCJ", mutexName)
        m_Mutex = New Threading.Mutex(False, m_strMutexName)
    End Sub
#End Region

    Public Shadows Function ReadMemoryWordIntegerDMBCD0(ByVal offset As Long) As Integer
        ReadMemoryWordIntegerDMBCD0 = Me.ReadMemoryWordInteger(OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM, offset, 1, OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)(0)
    End Function

    Public Shadows Function ReadMemoryWordIntegerDMBIN0(ByVal offset As Long) As Integer
        ReadMemoryWordIntegerDMBIN0 = Me.ReadMemoryWordInteger(OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM, offset, 1, OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BIN)(0)
    End Function

    Public Shadows Function ReadMemoryWordInteger(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal count As Long, ByVal dataType As OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes) As Integer()
        Me.GetLock()
        Try
            ReadMemoryWordInteger = MyBase.ReadMemoryWordInteger(memoryType, offset, count, dataType)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Function
    Public Shadows Function ReadMemoryString(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal count As Long) As String
        Me.GetLock()
        Try
            ReadMemoryString = MyBase.ReadMemoryString(memoryType, offset, count)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Function

    Public Shadows Function ReadMemoryDwordSingle(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal count As Long) As Single()
        Me.GetLock()
        Try
            ReadMemoryDwordSingle = MyBase.ReadMemoryDwordSingle(memoryType, offset, count)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Function
    Public Shadows Sub WriteMemoryWordIntegerDMBCD0(ByVal offset As Long, ByVal writeData As Integer)
        Dim ArrWriteData As Integer()
        ReDim ArrWriteData(0)
        If writeData < 0 Then
            writeData = 0
        End If
        ArrWriteData(0) = writeData
        Me.WriteMemoryWordInteger(OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM, offset, ArrWriteData, OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BCD)
    End Sub
    Public Shadows Sub WriteMemoryWordIntegerDMBIN0(ByVal offset As Long, ByVal writeData As Integer)
        Dim ArrWriteData As Integer()
        ReDim ArrWriteData(0)
        If writeData < 0 Then
            writeData = 0
        End If
        ArrWriteData(0) = writeData
        Me.WriteMemoryWordInteger(OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM, offset, ArrWriteData, OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes.BIN)
    End Sub
    Public Shadows Sub WriteMemoryWordInteger(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal writeData() As Integer, ByVal dataType As OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes)
        Me.GetLock()
        Try
            MyBase.WriteMemoryWordInteger(memoryType, offset, writeData, dataType)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Sub

    Public Shadows Sub WriteMemoryString(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal writeData As String)
        Me.GetLock()
        Try
            MyBase.WriteMemoryString(memoryType, offset, writeData)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Sub

    Public Shadows Sub WriteMemoryDwordSingle(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal writeData As Single())
        Me.GetLock()
        Try
            MyBase.WriteMemoryDwordSingle(memoryType, offset, writeData)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Sub

    Public Shadows Sub WriteMemoryDwordSingle(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal writeData As Single)
        Me.GetLock()
        Try
            MyBase.WriteMemoryDwordSingle(memoryType, offset, writeData)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Sub

    Public Shadows Sub WriteMemoryWordSyncDateTime(ByVal memoryType As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes, ByVal offset As Long, ByVal writeFormat() As String, ByVal dataType As OMRON.Compolet.SYSMAC.SysmacPlc.DataTypes)
        Me.GetLock()
        Try
            Dim aintDateTime(writeFormat.Length - 1) As Integer
            Dim datNow As DateTime = Now
            For i As Integer = 0 To writeFormat.Length - 1
                aintDateTime(i) = CInt(datNow.ToString(writeFormat(i)))
            Next

            MyBase.WriteMemoryWordInteger(memoryType, offset, aintDateTime, dataType)
        Catch ex As Exception
            Throw ex
        Finally
            Me.ReleaseLock()
        End Try
    End Sub
    Private Sub GetLock()
        m_Mutex.WaitOne()
    End Sub

    Private Sub ReleaseLock()
        m_Mutex.ReleaseMutex()
    End Sub
End Class
