@echo on
SET CONFIG=%1
IF [%1]==[] SET CONFIG=Release
SET LIB=net462
SET VER=v4.6.2

MD Bam.Net.Data\lib\%LIB%
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Model.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Model.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Model.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Model.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.MsSql.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MsSql.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.MsSql.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MsSql.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Schema.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Schema.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Schema.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Schema.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.SQLite.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.SQLite.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.SQLite.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.SQLite.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Npgsql.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Npgsql.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Npgsql.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Npgsql.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.SQLite.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.SQLite.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Oracle.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Oracle.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Oracle.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Oracle.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.MySql.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MySql.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.MySql.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.MySql.xml
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Protobuf.dll Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Protobuf.dll
copy /Y .\BuildOutput\%CONFIG%\%VER%\Bam.Net.Data.Protobuf.xml Bam.Net.Data\lib\%LIB%\Bam.Net.Data.Protobuf.xml



