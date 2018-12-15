@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD BamCore\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\BamCore.dll BamCore\lib\%LIB%\BamCore.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\BamCore.xml BamCore\lib\%LIB%\BamCore.xml