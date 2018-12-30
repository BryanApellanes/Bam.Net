@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Presentation\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Presentation.dll Bam.Net.Presentation\lib\%LIB%\Bam.Net.Presentation.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Presentation.xml Bam.Net.Presentation\lib\%LIB%\Bam.Net.Presentation.xml