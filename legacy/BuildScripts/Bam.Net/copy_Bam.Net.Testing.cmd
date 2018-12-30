@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Testing\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Testing.dll Bam.Net.Testing\lib\%LIB%\Bam.Net.Testing.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Testing.xml Bam.Net.Testing\lib\%LIB%\Bam.Net.Testing.xml