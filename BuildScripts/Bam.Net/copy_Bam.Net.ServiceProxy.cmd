@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.ServiceProxy\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.ServiceProxy.dll Bam.Net.ServiceProxy\lib\%LIB%\Bam.Net.ServiceProxy.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.ServiceProxy.xml Bam.Net.ServiceProxy\lib\%LIB%\Bam.Net.ServiceProxy.xml