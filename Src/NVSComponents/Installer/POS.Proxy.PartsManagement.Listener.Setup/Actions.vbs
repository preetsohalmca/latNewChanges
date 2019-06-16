Option Explicit

Const msiMessageTypeFatalExit = &H00000000 'Premature termination, possibly fatal out of memory.
Const msiMessageTypeError = &H01000000 'Formatted error message, [1] is message number in Error table.
Const msiMessageTypeWarning = &H02000000 'Formatted warning message, [1] is message number in Error table.
Const msiMessageTypeUser = &H03000000 'User request message, [1] is message number in Error table.
Const msiMessageTypeInfo = &H04000000 'Informative message for log, not to be displayed.
Const msiMessageTypeFilesInUse = &H05000000 'List of files in use that need to be replaced.
Const msiMessageTypeResolveSource = &H06000000 'Request to determine a valid source location.
Const msiMessageTypeOutOfDiskSpace = &H07000000 'Insufficient disk space message.
Const msiMessageTypeActionStart = &H08000000 'Start of action, [1] action name, [2] description, [3] template for ACTIONDATA messages.
Const msiMessageTypeActionData = &H09000000 'Action data. Record fields correspond to the template of ACTIONSTART message.
Const msiMessageTypeProgress = &H0A000000 'Progress bar information. See the description of record fields below.
Const msiMessageTypeCommonData = &H0B000000 'To enable the Cancel button set [1] to 2 and [2] to 1. To disable the Cancel button set [1] to 2 and [2] to 0
 

Const msiDoActionStatusNoAction = 0 'Action not executed. 
Const msiDoActionStatusSuccess = 1 'Action completed successfully. 
Const msiDoActionStatusUserExit = 2 'Premature termination by user. 
Const msiDoActionStatusFailure = 3 'Unrecoverable error. Returned if there is an error during parsing or execution of the Jscript or VBScript. 
Const msiDoActionStatusSuspend = 4 'Suspended sequence to be resumed later. 
Const msiDoActionStatusFinished = 5 'Skip remaining actions. Not an error. 

Public Function SetDefaultRootDir()
'Modifies:   Property DEFAULTROOTDIR, must exist!
'Depends on: Nothing
'Action:     
'If APPROOT, COMPONENTS or APPS environment variable doesn't exist then use the default value set in Defines.wxi
	On Error Resume Next
	Log "Executing SetDefaultRootDir"
	If Session Is Nothing Then
		SetDefaultRootDir = msiDoActionStatusFailure
		Exit Function
	Else
        Dim DefaultRootDir
        DefaultRootDir = Session.Property("%APPROOT")
        If DefaultRootDir = "" Then
            DefaultRootDir = Session.Property("%COMPONENTS")
        End If
        If DefaultRootDir = "" Then
            DefaultRootDir = Session.Property("%APPS")
        End If
        If DefaultRootDir = "" Then
            DefaultRootDir = Session.Property("DEFAULTROOTDIR")
        End If
        If DefaultRootDir = "" Then
            DefaultRootDir = Session.Property("%PROGRAMFILES")
        End If
        Log "DEFAULTROOTDIR After Environment test: " & DefaultRootDir
        Session.Property("DEFAULTROOTDIR") = DefaultRootDir & "\"
        Log "Finally setting property DEFAULTROOTDIR to " & DefaultRootDir
        SetDefaultRootDir = msiDoActionStatusSuccess
	End If
End Function

Public Sub Log(aMsg)
    If Not Session Is Nothing Then
        Dim msgrec
        Set msgrec = Installer.CreateRecord(1)
        msgrec.StringData(0) = "Actions.vbs: [1]"
        msgrec.StringData(1) = aMsg
        Session.Message msiMessageTypeInfo, msgrec
    End If
End Sub
