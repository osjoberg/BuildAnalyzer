# So what is MSBuild doing? #
A lot i turns out. Ever wondered what part of the build process is slow? This tool may help you.

# Example output #
![Screenshot](/Screenshot.png?raw=true)

# Usage instructions #

  1. Copy the BuildAnalyzer.dll to the project directory or add it to the path environment variable
  1. Build with MSBuild mysolution.sln /l:BuildAnalyzer.dll to enable analytics, add other switches as you like
  1. Open the report file, mysolution.svg in your favourite svg viewer, your web browser for example
