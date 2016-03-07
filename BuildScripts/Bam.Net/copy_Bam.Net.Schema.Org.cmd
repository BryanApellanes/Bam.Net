@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Schema.Org\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Schema.Org.dll Bam.Net.Schema.Org\lib\%LIB%\Bam.Net.Schema.Org.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Schema.Org.xml Bam.Net.Schema.Org\lib\%LIB%\Bam.Net.Schema.Org.xml