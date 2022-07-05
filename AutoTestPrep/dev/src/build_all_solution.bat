@echo off

rem 開発者用コマンドプロンプト起動
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

rem .slnファイルを指定、順番にビルドを実施する。
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\TestParser.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\CodeGenerator.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\PluginManager.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\PluginRegister.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\StubDriverPlugin.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\AutoTestPrep.sln"
pause:
