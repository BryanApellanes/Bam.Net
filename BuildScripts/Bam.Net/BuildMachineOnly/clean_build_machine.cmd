rem cleans temporary files created by tests
TASKKILL /IM MSBuild.exe /F
TASKKill /IM VBCSCompiler.exe /F
C:
cd %AppData%
RMDIR /S /Q UNKNOWN
RMDIR /S /Q TheMonkey
CD \
RMDIR /S /Q temp
MKDIR temp
RMDIR /S C:\Builds
Z:
