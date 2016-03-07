@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Html\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Html.dll Bam.Net.Html\lib\%LIB%\Bam.Net.Html.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Html.xml Bam.Net.Html\lib\%LIB%\Bam.Net.Html.xml