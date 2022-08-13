set pathMSBuild="%PROGRAMFILES(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"
@ECHO OFF

%pathMSBuild%\msbuild.exe ".\Server\Server.sln" /p:Configuration=Debug /p:Platform="Any CPU" /t:Restore;Build

%pathMSBuild%\msbuild.exe ".\Cache\Cache.sln" /p:Configuration=Debug /p:Platform="Any CPU" /t:Restore;Build

%pathMSBuild%\msbuild.exe ".\Client\Client.sln" /p:Configuration=Debug /p:Platform="Any CPU" /t:Restore;Build

cd ".\Server\bin\Debug\net5.0\"
start Server.exe

cd /D "%~dp0"
cd ".\Cache\bin\Debug\net5.0-windows\"
start Cache.exe

cd /D "%~dp0"
cd "".\Client\bin\Debug\net5.0-windows\""
start Client.exe