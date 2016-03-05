@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net45
SET VER=v4.5

MD Bam.Net.Messaging\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Messaging.dll Bam.Net.Messaging\lib\%LIB%\Bam.Net.Messaging.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Messaging.xml Bam.Net.Messaging\lib\%LIB%\Bam.Net.Messaging.xml