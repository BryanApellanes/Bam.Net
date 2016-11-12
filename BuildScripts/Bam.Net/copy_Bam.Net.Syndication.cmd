@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Syndication\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Syndication.dll Bam.Net.Syndication\lib\%LIB%\Bam.Net.Syndication.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Syndication.xml Bam.Net.Syndication\lib\%LIB%\Bam.Net.Syndication.xml