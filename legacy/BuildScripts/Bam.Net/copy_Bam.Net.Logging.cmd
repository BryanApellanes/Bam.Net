@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Logging\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Logging.dll Bam.Net.Logging\lib\%LIB%\Bam.Net.Logging.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Logging.xml Bam.Net.Logging\lib\%LIB%\Bam.Net.Logging.xml