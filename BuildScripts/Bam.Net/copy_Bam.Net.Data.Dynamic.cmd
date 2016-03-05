@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Data.Dynamic\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Dynamic.dll Bam.Net.Data.Dynamic\lib\%LIB%\Bam.Net.Data.Dynamic.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Dynamic.xml Bam.Net.Data.Dynamic\lib\%LIB%\Bam.Net.Data.Dynamic.xml