rmdir ..\..\Products\BamToolkit\BamToolkit-debug-cache /S /Q
rmdir ..\..\Products\BamToolkit\BamToolkit-debug-SetupFiles /S /Q
"C:\Program Files (x86)\Caphyon\Advanced Installer 12.1\bin\x86\AdvancedInstaller.com" /build ..\..\Products\BamToolkit\BamToolkit-debug.aip
MD .\BuildOutput\msi-debug\
copy ..\..\Products\BamToolkit\BamToolkit-SetupFiles\BamToolkit.msi .\BuildOutput\Msi-debug\
MD .\BamToolkit-debug\lib\net45\
copy ..\..\Products\BamToolkit\BamToolkit-debug-SetupFiles\BamToolkit-debug.msi .\BamToolkit-debug\lib\net45\
nuget pack BamToolkit-debug\BamToolkit-debug.nuspec

call move_nuget_packages_debug.cmd