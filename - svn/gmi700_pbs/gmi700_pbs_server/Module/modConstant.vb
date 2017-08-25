Public Module ModConstant
    'Flag to show MessageBox when Error
    Public Const ERROR_MESSAGE_DISPLAY As Boolean = False
    Public Const OPERATION_FAIL_MESSAGE_DISPLAY As Boolean = True

    'Process status
    Public Const PROCESS_STATUS_ID_WAITING As Integer = 0
    Public Const PROCESS_STATUS_ID_PROCESSING As Integer = 1
    Public Const PROCESS_STATUS_ID_COMPLETE As Integer = 2
    Public Const PROCESS_STATUS_ID_NO_DATA As Integer = 3
    Public Const PROCESS_STATUS_ID_ERROR As Integer = 4

    'PLC Write BCD
    Public Const PLC_WRITE_STATUS_LENGTH As UInteger = 1
    Public Const PLC_WRITE_SERVER_STATUS_LENGTH As UInteger = 1
    Public Const PLC_WRITE_STATUS_WAITING As Integer = 0
    Public Const PLC_WRITE_STATUS_PROCESSING As Integer = 1
    Public Const PLC_WRITE_STATUS_COMPLETE As Integer = 2
    Public Const PLC_WRITE_STATUS_NO_DATA As Integer = 3
    Public Const PLC_WRITE_STATUS_ERROR As Integer = 4
    
    'PLC Read
    Public Const PLC_READ_STATUS_LENGTH As UInteger = 1
    Public Const PLC_READ_STATUS_WAITING As Integer = 0
    Public Const PLC_READ_STATUS_REQUEST As Integer = 1
    Public Const PLC_READ_STATUS_COMPLETE As Integer = 2
    Public Const PLC_READ_STATUS_DUPLICATE_SERIAL As Integer = 3
    Public Const PLC_READ_STATUS_INVALID_QR As Integer = 4
    Public Const PLC_READ_STATUS_ERROR As Integer = 5
    Public Const PLC_READ_STATUS_SAME_STATUS As Integer = -1
    Public Const PLC_READ_STATUS_READ_FAILED As Integer = -3


    '===== 20151120-14_25
    Public Const PLC_SLEEP_INTERNAL As UInteger = 500
    Public Const PLC_SYNC_TIME_WHEN As String = "00:00"

    Public Const PLC_MEMORY_TYPE As OMRON.Compolet.SYSMAC.SysmacCJ.MemoryTypes = OMRON.Compolet.SYSMAC.SysmacCSBase.MemoryTypes.DM


End Module
