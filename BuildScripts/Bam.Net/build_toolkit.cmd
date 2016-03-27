rmdir ..\..\Products\BamToolkit\BamToolkit-cache /S /Q
rmdir ..\..\Products\BamToolkit\BamToolkit-SetupFiles /S /Q
"C:\Program Files (x86)\Caphyon\Advanced Installer 12.1\bin\x86\AdvancedInstaller.com" /build ..\..\Products\BamToolkit\BamToolkit.aip
MD .\BuildOutput\msi\
copy ..\..\Products\BamToolkit\BamToolkit-SetupFiles\BamToolkit.msi .\BuildOutput\msi\
MD .\BamToolkit\lib\net45\
copy ..\..\Products\BamToolkit\BamToolkit-SetupFiles\BamToolkit.msi .\BamToolkit\lib\net45\
nuget pack BamToolkit\BamToolkit.nuspec

call move_nuget_packages.cmd