del /F /Q %temp%\BamToolkit.msi
copy %1\Bam.Net\BuildScripts\Bam\BamToolkit\lib\net45\BamToolkit.msi %temp%\BamToolkit.msi
msiexec /i %temp%\BamToolkit.msi /qb 
