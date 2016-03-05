@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Automation\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Automation.dll Bam.Net.Automation\lib\%LIB%\Bam.Net.Automation.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Automation.xml Bam.Net.Automation\lib\%LIB%\Bam.Net.Automation.xml