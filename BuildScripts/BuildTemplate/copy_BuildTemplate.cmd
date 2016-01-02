@echo off

SET LIB=net40
SET VER=v4.0
SET NEXT=NEXT
GOTO COPY

:NEXT
SET LIB=net45
SET VER=v4.5
SET NEXT=END
GOTO COPY

:COPY
MD BuildTemplate\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\BuildTemplate.dll BuildTemplate\lib\%LIB%\BuildTemplate.dll
copy /Y .\BuildOutput\Release\%VER%\BuildTemplate.xml BuildTemplate\lib\%LIB%\BuildTemplate.xml
GOTO %NEXT%

:END


