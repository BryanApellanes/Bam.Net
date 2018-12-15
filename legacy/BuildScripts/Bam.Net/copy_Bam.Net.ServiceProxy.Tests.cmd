@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.ServiceProxy.Tests\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.ServiceProxy.Tests.exe Bam.Net.ServiceProxy.Tests\lib\%LIB%\Bam.Net.ServiceProxy.Tests.exe
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.ServiceProxy.Tests.xml Bam.Net.ServiceProxy.Tests\lib\%LIB%\Bam.Net.ServiceProxy.Tests.xml