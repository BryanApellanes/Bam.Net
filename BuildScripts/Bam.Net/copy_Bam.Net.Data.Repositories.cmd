@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Data.Repositories\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Repositories.dll Bam.Net.Data.Repositories\lib\%LIB%\Bam.Net.Data.Repositories.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Repositories.xml Bam.Net.Data.Repositories\lib\%LIB%\Bam.Net.Data.Repositories.xml