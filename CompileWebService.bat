REM Compile all generated C# class files and infrastructure files. 
REM Create [BundleName] web service code behind dll.
REM set Csc_Path=%WinDir%\Microsoft.NET\Framework\v2.0.50727
REM set PATH=%PATH%;%Csc_Path%
csc /t:library /out:bin\wssfbt.dll *.cs /r:C:\NGEN_CE\bin\CEWindowsAPI.dll /r:C:\NGEN_CE\bin\CEWindowsAPIPool.dll
pause