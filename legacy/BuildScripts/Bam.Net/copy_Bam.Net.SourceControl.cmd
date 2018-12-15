@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.SourceControl\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.SourceControl.dll Bam.Net.SourceControl\lib\%LIB%\Bam.Net.SourceControl.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.SourceControl.xml Bam.Net.SourceControl\lib\%LIB%\Bam.Net.SourceControl.xml