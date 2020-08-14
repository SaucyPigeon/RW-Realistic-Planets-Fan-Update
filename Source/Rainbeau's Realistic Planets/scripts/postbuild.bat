:: copy v1.1 output to v1.2 folder
echo off

:: source location
set arg1=%1

:: output folder
set arg2=%2

xcopy /s /y %arg1% %arg2%