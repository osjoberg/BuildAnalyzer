BuildAnalyzer
=============
Figure out what MSBuild doing and what part of my build process is slow. With this tool you can get a graphical overview of what MSBuild is doing.

![Screenshot](/Screenshot.png?raw=true)

(Example screenshot from the build of BuildAnalyzer)

Usage instructions
------------------
1. Download the latest binary from [here](/releases/download/0.1.0/BuildAnalyzer.dll) or compile it yourself from source
2. Copy the BuildAnalyzer.dll to the project directory or add it to the path environment variable
3. Build with MSBuild mysolution.sln /l:BuildAnalyzer.dll to enable analytics, add other compiler switches as you like
4. Open the report file, mysolution.svg in your favourite svg viewer, your web browser for example
