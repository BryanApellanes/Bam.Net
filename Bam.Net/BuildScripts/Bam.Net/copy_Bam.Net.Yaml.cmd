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
MD Bam.Net.Yaml\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Yaml.dll Bam.Net.Yaml\lib\%LIB%\Bam.Net.Yaml.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Yaml.xml Bam.Net.Yaml\lib\%LIB%\Bam.Net.Yaml.xml
GOTO %NEXT%

:END


