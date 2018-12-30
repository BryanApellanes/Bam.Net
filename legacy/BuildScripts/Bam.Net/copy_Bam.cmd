@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.exe Bam\lib\%LIB%\Bam.exe
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.xml Bam\lib\%LIB%\Bam.xml