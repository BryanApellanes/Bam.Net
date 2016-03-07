@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Documentation\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Documentation.dll Bam.Net.Documentation\lib\%LIB%\Bam.Net.Documentation.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Documentation.xml Bam.Net.Documentation\lib\%LIB%\Bam.Net.Documentation.xml