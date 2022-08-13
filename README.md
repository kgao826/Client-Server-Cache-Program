# Client-Server-Cache-Program
Using C# WPF to create a cache where users don't have to download multiple files

C# GUI was created by using WPF (Windows Presentation Foundation) using ListBox, TextBox and Buttons.
I used TCP to connect the server, cache and client together. The server program uses a listener and allows requests to view the files in the Server_Files folder and requests to download those files. The data was converted into bytes to send via the TCP stream and then converted back into UTF8. The Cache program uses a listener and a client to retrieve items from the Server and sends it to the Client. The Cache listener received any requests from the Client to download the file. The Cache has an if statement that checks if items are in the Cache_Files folder, if not, then it stores the file there after retrieving from the server. Otherwise, it just gives the client the file in the Cache folder without downloading anything from the server. The Client connects to the Cache via TCP and sends requests to view the list or download a file. The Client stores the file in Client_Files.

The Server uses port 8000 and Cache uses 8001.

# Instructions on how to run the program
Each program resides in their respective folders: Client, Cache, Server.

The batch files allow the programs to execute.

The programs should be executed in the following order:
1. Server
2. Cache
3. Client

There are four batch files. One is for launching all the programs at once, however, this can only be done if the items are in the same directory. The other three batch files launch each program respectively. All batch files must not be moved to different folders.

**For each batch file please set your msbuild location for pasthMSBuild to the msbuild.exe location in the Visual Studio installation folder on your computer. The line looks likes this:**

set ```pathMSBuild="%PROGRAMFILES(x86)%\Microsoft Visual Studio\2019\Community\MSBuild\Current\Bin"```

“runAllBatchFile” should only be used to run all the programs at once when they are in the same directory.

“ServerBatchFile” runs the server program.

“CacheBatchFile” runs the cache program.

“ClientBatchFile” run the client program.

In each program folder, they have their own storage folder:

Server stores its files in Server_Files

Cache stores its files in Cache_Files and creates the log in the main directory.

Client stores its files in Client_Files

Please put any Server files in the Server_Files folder.

You may move the folders to any location, but do not move files in the folders to other folders.
