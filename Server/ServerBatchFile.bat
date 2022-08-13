set pathMSBuild="%PROGRAMFILES(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"
@ECHO OFF

%pathMSBuild%\msbuild.exe ".\Server.sln" /p:Configuration=Debug /p:Platform="Any CPU" /t:Restore;Build

cd ".\bin\Debug\net5.0\"
start Server.exe
