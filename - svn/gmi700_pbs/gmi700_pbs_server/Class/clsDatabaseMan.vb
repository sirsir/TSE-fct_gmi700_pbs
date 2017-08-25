Imports System.Globalization



Public Class clsDatabaseMan
    'Dim entity = New fct_gmi700_pbsEntities()

    'Dim m_objPlc As clsPlcWrapper = New clsPlcWrapper()

    'Public dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
    Private m_dbEntities As fct_gmi700_pbsEntities
    Private m_memoryMap As System.Data.Entity.DbSet(Of gmi700_pbs_server.PLC_MEMORY_MAP)
    Private m_plcMaster As System.Data.Entity.DbSet(Of gmi700_pbs_server.PLC_MASTER)

    Protected m_clsLogger As clsLogger = New clsLogger()



    Enum SaveResultFromFile_RETURN_CODE
        COMPLETE
        INVALID
        DATABASE_SAVE_ERROR
        DATABASE_CONNECTION_ERROR
    End Enum

    Public Sub New()
        m_dbEntities = New fct_gmi700_pbsEntities
        m_memoryMap = m_dbEntities.PLC_MEMORY_MAP
        m_plcMaster = m_dbEntities.PLC_MASTER
    End Sub

    Public Function PLC_GetAddress(ByVal plc_id As Integer) As Integer

        Try
            'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
            Dim intReturn As Integer = (From plc In m_plcMaster
                                             Where plc.PLC_ID = plc_id
                                             Select plc.PC_WRITE_ADDRESS).First

            Return intReturn
            'End Using
        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "PLC_GetAddress", "function end with error", "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "PLC_GetAddress", ex)
            Return -1
        End Try


        

    End Function



    Public Sub ClearPreviousProcessTestResult(ByVal plcSetting As PLC_MASTER, ByVal plcObject As clsPlcWrapper)
        Try

            'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
            Dim memoryMap As PLC_MEMORY_MAP = (From mem In m_memoryMap
                                             Where mem.DESCR = "Previous Process Test Result").First


            Dim addressTemp As Integer = CInt(memoryMap.OFFSET) + PLC_GetAddress(plcSetting.PLC_ID)

            plcObject.WriteMemoryString(PLC_MEMORY_TYPE _
                                                            , CInt(memoryMap.OFFSET) + PLC_GetAddress(plcSetting.PLC_ID) _
                                                            , " ")
            'End Using

        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "ClearPreviousProcessTestResult", "function end with error", "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "ClearPreviousProcessTestResult", ex)
        End Try


    End Sub



    Public Function SaveResultFromQRcode(ByVal strQRcode As String, ByVal plcSetting As PLC_MASTER, ByVal plcObject As clsPlcWrapper) As Integer

        m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "function start", "Info")


        Dim processID As Integer = plcSetting.PROCESS_ID
        Dim plcID As Integer = plcSetting.PLC_ID
        Dim intIncomingFlag As Integer = plcSetting.INCOMING_FLAG

        Dim strResult As String
        Dim rowNewResult As RESULT = New RESULT()


        'Dim returnCode As SaveResultFromQRcode_RETURN_CODE
        Dim returnCode As Integer = -1

        Dim strQRcodeSplit As String() = strQRcode.Split(New Char() {"-"c})


        Try

            returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_CONNECTION_ERROR
            Using newContext As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()




                returnCode = SaveResultFromQRcode_RETURN_CODE.INVALID

                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Extracting QR code=" & strQRcode, "Info")

                rowNewResult.CODE_NO = strQRcodeSplit(0)
                rowNewResult.MODEL_TYPE = strQRcodeSplit(1)
                rowNewResult.PROCESS_ID = processID
                rowNewResult.SHOT_NO = strQRcodeSplit(4)
                rowNewResult.PROCESS_CODE = strQRcodeSplit(5)

                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Extracting QR code: OK", "Info")


                'rowNewResult.CODE_NO = strQRcode.Substring(0, 8)
                'rowNewResult.MODEL_TYPE = strQRcode.Substring(9, 2)
                'rowNewResult.PROCESS_ID = processID
                'rowNewResult.SHOT_NO = strQRcode.Substring(30, 3)
                'rowNewResult.PROCESS_CODE = strQRcode.Substring(34, 3)
                'rowNewResult.JUDGE_RESULT = strQRcode.Substring(56, 2)

                'If rowNewResult.JUDGE_RESULT = String.Empty Then
                '    rowNewResult.JUDGE_RESULT = strQRcode.Substring(38, 2)
                'End If


                rowNewResult.JUDGE_WHEN = DateTime.Now
                rowNewResult.PLC_ID = plcID


                Dim memoryMap2 As PLC_MEMORY_MAP
                'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()

                '    memoryMap2 = (From mem In dbEntities.PLC_MEMORY_MAP
                '                         Where mem.DESCR = "Result").First
                'End Using
                memoryMap2 = (From mem In m_memoryMap
                                         Where mem.DESCR = "Result").First



                returnCode = SaveResultFromQRcode_RETURN_CODE.PLC_CONNECTION_ERROR
                Dim addressTemp2 As Integer = CInt(memoryMap2.OFFSET) + PLC_GetAddress(plcSetting.PLC_ID)
                Dim strData1 As String = plcObject.ReadMemoryString(PLC_MEMORY_TYPE _
                          , addressTemp2 _
                           , memoryMap2.NO_OF_WORDS)

                'If plcSetting.PLC_ID = 1 Then
                If plcSetting.PROCESS_ID = 1 Then
                    rowNewResult.JUDGE_RESULT = strQRcode.Substring(38, 2)
                Else
                    If strData1 Like "OK" Then
                        rowNewResult.JUDGE_RESULT = "OK"
                    ElseIf strData1 Like "NG" Then
                        rowNewResult.JUDGE_RESULT = "NG"
                    End If
                End If


                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "write previous result to PLC" & strQRcode, "Info")
                ' === write previous result to PLC >
                If intIncomingFlag = 1 Then


                    Dim strPreviousResult As String


                    returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_CONNECTION_ERROR

                    strPreviousResult = "NoDATA"
                    Try
                        Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
                            strPreviousResult = (From row In dbEntities.RESULTs
                                    Where row.CODE_NO = rowNewResult.CODE_NO And row.MODEL_TYPE = rowNewResult.MODEL_TYPE And row.PROCESS_ID = (rowNewResult.PROCESS_ID - 1)
                                                  ).First.JUDGE_RESULT
                        End Using
                    Catch ex As Exception
                        m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Can not get previous process result", "Info")
                        Return SaveResultFromQRcode_RETURN_CODE.DATABASE_CONNECTION_ERROR
                    End Try
                    


                    If strPreviousResult = "NG" Then
                        rowNewResult.JUDGE_RESULT = strPreviousResult
                    End If

                    If strPreviousResult Is Nothing Then
                        strPreviousResult = ""
                    End If
                    Dim memoryMap As PLC_MEMORY_MAP


                    'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
                    '    memoryMap = (From mem In dbEntities.PLC_MEMORY_MAP
                    '                         Where mem.DESCR = "Previous Process Test Result").First
                    'End Using
                    memoryMap = (From mem In m_memoryMap
                                             Where mem.DESCR = "Previous Process Test Result").First


                    Dim addressTemp As Integer = CInt(memoryMap.OFFSET) + PLC_GetAddress(plcSetting.PLC_ID)



                    plcObject.WriteMemoryString(PLC_MEMORY_TYPE _
                                                                    , CInt(memoryMap.OFFSET) + PLC_GetAddress(plcSetting.PLC_ID) _
                                                                    , strPreviousResult)

                    returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_CONNECTION_ERROR

                    'If strPreviousResult.ToUpper = "NG" Or strPreviousResult = "" Then
                    If strPreviousResult.ToUpper <> "OK" Then
                        m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Previous process result is not OK", "Info")
                        Return SaveResultFromQRcode_RETURN_CODE.DATABASE_CONNECTION_ERROR
                    End If



                    ' === write previous result to PLC <


                    Dim rowOldResult As RESULT = (From row In newContext.RESULTs
                                               Where row.CODE_NO = rowNewResult.CODE_NO And row.MODEL_TYPE = rowNewResult.MODEL_TYPE And
                                                  row.PROCESS_ID = rowNewResult.PROCESS_ID
                                           Select row).FirstOrDefault()

                    If rowOldResult Is Nothing Then
                        newContext.RESULTs.Add(rowNewResult)

                    Else
                        '20160601-15_26
                        'DO NOT SEND ERROR fo duplicate barcode if PLC.intIncomingFlag=1
                        'Return SaveResultFromQRcode_RETURN_CODE.DUPLICATE
                        m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Found duplicate barcode but trying to ignore", "Info")
                    End If



                ElseIf intIncomingFlag = 0 Then
                    'in
                    'Dim rowOldResult As RESULT = (From row In newContext.RESULTs
                    '                           Where row.CODE_NO = rowNewResult.CODE_NO And
                    '                              row.PROCESS_ID = rowNewResult.PROCESS_ID And
                    '                              (row.JUDGE_RESULT Is Nothing Or row.JUDGE_RESULT = "" Or row.JUDGE_RESULT.Trim = String.Empty)
                    '                       Select row).FirstOrDefault()

                    'TODO: add model type
                    Dim rowOldResult As RESULT = (From row In newContext.RESULTs
                                               Where row.CODE_NO = rowNewResult.CODE_NO And row.MODEL_TYPE = rowNewResult.MODEL_TYPE And
                                                  row.PROCESS_ID = rowNewResult.PROCESS_ID
                                           Select row).FirstOrDefault()

                    '.First

                    If rowOldResult Is Nothing Then
                        If processID = 1 Then

                            newContext.RESULTs.Add(rowNewResult)
                        Else
                            'Dim rowOldResult2 As RESULT = (From row In newContext.RESULTs
                            '                   Where row.CODE_NO = rowNewResult.CODE_NO And
                            '                      row.PROCESS_ID = rowNewResult.PROCESS_ID And
                            '                      (row.JUDGE_RESULT Like "OK" Or row.JUDGE_RESULT Like "NG")
                            '               Select row).FirstOrDefault()
                            'If rowOldResult2 Is Nothing Then
                            '    Return SaveResultFromQRcode_RETURN_CODE.INVALID
                            'Else
                            '    Return SaveResultFromQRcode_RETURN_CODE.DUPLICATE
                            'End If


                            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Exit because no scan-IN", "Info")
                            Return SaveResultFromQRcode_RETURN_CODE.INVALID




                        End If




                    Else
                        '=rowOldResult Is NOT NOTHING

                        If processID = 1 Then
                            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Exit because processID=1 has already record", "Info")
                            Return SaveResultFromQRcode_RETURN_CODE.DUPLICATE
                        Else
                            'rowOldResult.JUDGE_RESULT = strQRcode.Substring(56, 2)

                            'If (rowOldResult.JUDGE_RESULT Is Nothing Or rowOldResult.JUDGE_RESULT = "" Or rowOldResult.JUDGE_RESULT.Trim = String.Empty) Then

                            'm_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Check previous result is already set to OK or NG", "Info")

                            If rowOldResult.JUDGE_RESULT Is Nothing Or rowOldResult.JUDGE_RESULT = "" Then
                                rowOldResult.JUDGE_RESULT = ""
                            Else
                                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Exit because already has result", "Info")
                                Return SaveResultFromQRcode_RETURN_CODE.INVALID
                            End If
                            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Check previous result is already set to OK or NG:>" & rowOldResult.JUDGE_RESULT, "Info")

                            If rowOldResult.JUDGE_RESULT.ToUpper <> "OK" And rowOldResult.JUDGE_RESULT.ToUpper <> "NG" Then
                                'Dim memoryMap2 As PLC_MEMORY_MAP
                                'Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()

                                '    memoryMap2 = (From mem In dbEntities.PLC_MEMORY_MAP
                                '                         Where mem.DESCR = "Result").First
                                'End Using

                                'm_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Check previous result is already set to OK or NG:>" & rowOldResult.JUDGE_RESULT, "Info")

                                memoryMap2 = (From mem In m_memoryMap
                                                         Where mem.DESCR = "Result").First


                                returnCode = SaveResultFromQRcode_RETURN_CODE.PLC_CONNECTION_ERROR

                                'Dim addressTemp2 As Integer = CInt(memoryMap2.OFFSET) + PLC_GetAddress(plcSetting.PLC_ID)
                                'Dim strData1 As String
                                strData1 = plcObject.ReadMemoryString(PLC_MEMORY_TYPE _
                                          , addressTemp2 _
                                           , memoryMap2.NO_OF_WORDS)

                                'Add on 20160525-17_10
                                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Prepare NEW Process Code for database", "Info")
                                'rowOldResult.PROCESS_CODE = rowNewResult.PROCESS_CODE
                                Try
                                    rowOldResult.SHOT_NO = strQRcodeSplit(4)
                                    rowOldResult.PROCESS_CODE = strQRcodeSplit(5)
                                Catch ex As Exception
                                    'm_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Cant set new Process code. Keep old value", "Info")
                                    m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Invalid QR Code", "Info")
                                    Return SaveResultFromQRcode_RETURN_CODE.INVALID
                                End Try


                                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Prepare NEW Process Code for database:=" & strQRcodeSplit(5), "Info")

                                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Normalize OK/NG >start for strData1=" & strData1, "Info")

                                rowOldResult.JUDGE_RESULT = ""
                                If strData1.ToUpper = "OK" Then
                                    'rowOldResult.JUDGE_RESULT = CStr(strData1)
                                    rowOldResult.JUDGE_RESULT = "OK"
                                ElseIf strData1.ToUpper = "NG" Then
                                    rowOldResult.JUDGE_RESULT = "NG"
                                End If

                                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Normalize OK/NG > rowOldResult.JUDGE_RESULT=" & rowOldResult.JUDGE_RESULT, "Info")

                                rowOldResult.JUDGE_WHEN = DateTime.Now
                                rowOldResult.PLC_ID = plcSetting.PLC_ID

                            Else
                                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Exit because rowOldResult.JUDGE_RESULT=" & rowOldResult.JUDGE_RESULT, "Info")

                                Return SaveResultFromQRcode_RETURN_CODE.INVALID

                                'Return SaveResultFromQRcode_RETURN_CODE.DUPLICATE

                            End If



                        End If


                    End If


                End If

                '--- save part to database
                'FOR GM=PID5 
                If intIncomingFlag = 0 And rowNewResult.PROCESS_ID = g_GmProcessId Then
                    m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Special operation for PID" & g_GmProcessId, "Info")
                    returnCode = SaveResultFromQRcode_RETURN_CODE.INVALID

                    Dim rowPartNo As PART_NO_MAPPING = New PART_NO_MAPPING()

                    rowPartNo.CODE_NO = strQRcodeSplit(0)
                    rowPartNo.MODEL_TYPE = strQRcodeSplit(1)
                    rowPartNo.PART_NO = strQRcodeSplit(strQRcodeSplit.Length - 2)


                    newContext.PART_NO_MAPPING.Add(rowPartNo)

                End If



                'returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_SAVE_ERROR
                returnCode = SaveResultFromQRcode_RETURN_CODE.INVALID



                Try
                    newContext.SaveChanges()
                Catch ex As Exception
                    m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "ERROR! Unable to commit result to database", "Info")
                End Try








            End Using

            'Dim intReturn As Integer = (From plc In dbEntities.PLC_MASTER
            '                                      Where plc.PLC_ID = plc_id
            '                                      Select plc.PC_WRITE_ADDRESS).First

            'Return intReturn

            'START Write to file
            If intIncomingFlag = 0 And rowNewResult.PROCESS_ID = g_GmProcessId Then
                'GM=PID5
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Prepare to call WriteToFile() for intIncomingFlag = 0 And rowNewResult.PROCESS_ID = 4", "Info")
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Special operation for PID" & g_GmProcessId, "Info")


                returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_SAVE_ERROR
                strResult = rowNewResult.CODE_NO &
                                "-" &
                                rowNewResult.MODEL_TYPE &
                                "-" &
                                Format(rowNewResult.JUDGE_WHEN, "ddMMyyyy-HH:mm:ss") &
                                "-" &
                                rowNewResult.SHOT_NO &
                                "-" &
                                rowNewResult.PROCESS_CODE &
                                 "-" &
                                strQRcodeSplit(strQRcodeSplit.Length - 2) &
                                "-" &
                                rowNewResult.JUDGE_RESULT


                Dim strPathTemp As String = g_strLocalPathForData & "\" & strQRcode.Substring(0, 11) & ".txt"

                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Call WriteToFile()", "Info")
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Parameter strResult=" & strResult, "Info")
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Parameter path=" & strResult, "Info")
                WriteToFile(strPathTemp, strResult)



                'm_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Call WriteToFile()", "Info")
                'WriteToFile(g_strLocalPathForData & "\" & strQRcode.Substring(0, 11) & ".txt", strResult)
            ElseIf intIncomingFlag = 0 Or rowNewResult.PROCESS_ID = 1 Then
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Prepare to call WriteToFile()", "Info")
                strResult = rowNewResult.CODE_NO &
                                "-" &
                                rowNewResult.MODEL_TYPE &
                                "-" &
                                Format(rowNewResult.JUDGE_WHEN, "ddMMyyyy-HH:mm:ss") &
                                "-" &
                                rowNewResult.SHOT_NO &
                                "-" &
                                rowNewResult.PROCESS_CODE &
                                "-" &
                                rowNewResult.JUDGE_RESULT

                Dim strPathTemp As String = g_strLocalPathForData & "\" & strQRcode.Substring(0, 11) & ".txt"

                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Call WriteToFile()", "Info")
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Parameter strResult=" & strResult, "Info")
                m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "Parameter path=" & strResult, "Info")
                WriteToFile(strPathTemp, strResult)

            End If



            Return SaveResultFromQRcode_RETURN_CODE.COMPLETE

        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", "function end with error", "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromQRcode", ex)
            Return returnCode
        End Try

    End Function




    Public Function SaveResultFromFile(ByVal strFilepath As String, ByVal processID As Integer, ByVal intIncomingFlag As Integer) As Integer

        'If intIncomingFlag = 1 And processID <> 1 Then
        '    Return -1
        'End If

        Dim returnCode As SaveResultFromFile_RETURN_CODE = -1

        Dim reader As String
        reader = My.Computer.FileSystem.ReadAllText(strFilepath)

        Dim strFilename As String = My.Computer.FileSystem.GetName(strFilepath)


        Dim strResult As String

        Try
            Dim rowNewResult As RESULT = New RESULT()
            returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_CONNECTION_ERROR



            Using newContext As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()



                returnCode = SaveResultFromQRcode_RETURN_CODE.INVALID

                rowNewResult.CODE_NO = strFilename.Substring(0, 8)
                rowNewResult.MODEL_TYPE = strFilename.Substring(9, 2)
                rowNewResult.PROCESS_ID = processID
                rowNewResult.SHOT_NO = reader.Substring(18, 3)
                rowNewResult.PROCESS_CODE = reader.Substring(22, 3)
                rowNewResult.JUDGE_RESULT = reader.Substring(26, 2)

                'If rowNewResult.JUDGE_RESULT = String.Empty Then
                '    rowNewResult.JUDGE_RESULT = strQRcode.Substring(38, 2)
                'End If


                'rowNewResult.JUDGE_WHEN = DateTime.Now
                rowNewResult.JUDGE_WHEN = DateTime.ParseExact(reader.Substring(0, 17), "ddMMyyyy-HH:mm:ss", CultureInfo.InvariantCulture)
                'rowNewResult.PLC_ID = plcID


                If intIncomingFlag = 1 Then

                    Dim strPreviousResult As String = ""






                    Using dbEntities As fct_gmi700_pbsEntities = New fct_gmi700_pbsEntities()
                        strPreviousResult = (From row In dbEntities.RESULTs
                                              Where row.CODE_NO = rowNewResult.CODE_NO And row.MODEL_TYPE = rowNewResult.MODEL_TYPE And row.PROCESS_ID = (rowNewResult.PROCESS_ID - 1)
                                             ).First.JUDGE_RESULT
                    End Using

                    If strPreviousResult.ToUpper <> "OK" Then
                        m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromFile", "Previous process result is NOT OK", "Info")
                        Return SaveResultFromFile_RETURN_CODE.INVALID
                        'Return SaveResultFromQRcode_RETURN_CODE.INVALID
                    End If




                End If


                'Dim rowOldResult As RESULT = (From row In newContext.RESULTs
                '                                Where row.CODE_NO = rowNewResult.CODE_NO And
                '                                   row.PROCESS_ID = rowNewResult.PROCESS_ID And
                '                                   (row.JUDGE_RESULT Is Nothing Or row.JUDGE_RESULT = "" Or row.JUDGE_RESULT.Trim = String.Empty)
                '                            Select row).FirstOrDefault()

                Dim rowOldResult As RESULT = (From row In newContext.RESULTs
                                                Where row.CODE_NO = rowNewResult.CODE_NO And row.MODEL_TYPE = rowNewResult.MODEL_TYPE And
                                                   row.PROCESS_ID = rowNewResult.PROCESS_ID
                                            Select row).FirstOrDefault()

                '.First

                If rowOldResult Is Nothing Then
                    newContext.RESULTs.Add(rowNewResult)


                Else
                    ' ALLOW OVERWRITE RESULT
                    rowOldResult.JUDGE_RESULT = rowNewResult.JUDGE_RESULT
                    rowOldResult.JUDGE_WHEN = rowNewResult.JUDGE_WHEN

                    ' EXIT IF ALREADY HAS RECORD
                    'm_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromFile", "Previous process result is NOT OK", "Info")
                    'Return SaveResultFromFile_RETURN_CODE.INVALID


                End If




                returnCode = SaveResultFromQRcode_RETURN_CODE.DATABASE_SAVE_ERROR
                newContext.SaveChanges()




            End Using

            'Dim intReturn As Integer = (From plc In dbEntities.PLC_MASTER
            '                                      Where plc.PLC_ID = plc_id
            '                                      Select plc.PC_WRITE_ADDRESS).First

            'Return intReturn


            '--- Append text to files

            strResult = rowNewResult.CODE_NO &
                        "-" &
                        rowNewResult.MODEL_TYPE &
                        "-" &
                        Format(rowNewResult.JUDGE_WHEN, "ddMMyyyy-HH:mm:ss") &
                        "-" &
                        rowNewResult.SHOT_NO &
                        "-" &
                        rowNewResult.PROCESS_CODE &
                        "-" &
                        rowNewResult.JUDGE_RESULT

            WriteToFile(g_strLocalPathForData & "\" & rowNewResult.CODE_NO & "-" & rowNewResult.MODEL_TYPE & ".txt", strResult)
            'WriteToFile(strFilepath, strResult)



            Return SaveResultFromQRcode_RETURN_CODE.COMPLETE

        Catch ex As Exception
            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromFile", "function end with error", "Info")
            m_clsLogger.AppendLog(Me.GetType.Name, "SaveResultFromFile", ex)
            Return returnCode
        End Try

    End Function


End Class
