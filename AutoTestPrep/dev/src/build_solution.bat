@echo off
setlocal

set SOLUTIN_FILE=%1
rem MSBuild�Ńr���h���s
cd %~dp0

echo %SOLUTIN_FILE%

MSBuild %SOLUTIN_FILE% /t:clean;rebuild /p:Configuration=Release;Platform="Any CPU"

endlocal
