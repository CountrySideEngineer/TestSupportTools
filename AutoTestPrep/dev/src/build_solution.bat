@echo off
setlocal

set SOLUTIN_FILE=%1
rem MSBuildÇ≈ÉrÉãÉhé¿çs
cd %~dp0

echo %SOLUTIN_FILE%

MSBuild %SOLUTIN_FILE% /t:clean;rebuild /p:Configuration=Release;Platform="Any CPU"

endlocal
