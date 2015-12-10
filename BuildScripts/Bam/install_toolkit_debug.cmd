del /F /Q %temp%\BamToolkit-debug.msi
copy %1\Bam.Net\BuildScripts\Bam\BamToolkit-debug\lib\net45\BamToolkit-debug.msi %temp%\BamToolkit-debug.msi
msiexec /i %temp%\BamToolkit-debug.msi /qb 
