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
MD Bam.Net.Data\lib\%LIB%
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Model.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Model.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Model.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Model.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.MsSql.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MsSql.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.MsSql.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MsSql.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Repositories.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Repositories.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Repositories.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Repositories.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Schema.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Schema.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Schema.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Schema.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.SQLite.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.SQLite.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.SQLite.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.SQLite.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Oracle.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Oracle.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.Oracle.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Oracle.xml
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.MySql.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MySql.dll
copy /Y .\BuildOutput\Release\%VER%\Bam.Net.Data.MySql.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MySqle.xml
copy /Y .\BuildOutput\Release\%VER%\System.Data.SQLite.dll Bam.Net.Data\lib\%LIB%\System.Data.SQLite.dll
GOTO %NEXT%

:END


