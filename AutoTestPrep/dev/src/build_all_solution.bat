@echo off

rem �J���җp�R�}���h�v�����v�g�N��
call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat"

rem .sln�t�@�C�����w��A���ԂɃr���h�����{����B
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\TestParser.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\CodeGenerator.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\PluginManager.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\PluginRegister.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\StubDriverPlugin.sln"
call .\build_solution.bat "E:\development\TestSupportTools_0_3\AutoTestPrep\dev\src\AutoTestPrep.sln"
pause:
