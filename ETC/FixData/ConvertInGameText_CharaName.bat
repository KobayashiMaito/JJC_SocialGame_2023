@ECHO OFF
SET BAT_PATH=%~dp0
SET POWERSHELL_PATH=%BAT_PATH%ConvertInGameText_CharaName.ps1
PowerShell -Command "Set-ExecutionPolicy RemoteSigned -Scope CurrentUser;"
ECHO %POWERSHELL_PATH%を実行します.
PowerShell -File %POWERSHELL_PATH%