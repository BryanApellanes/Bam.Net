SET ROOT=%1
IF [%1]==[] SET ROOT=C:\src
SET BRANCH=%2
IF [%2]==[] SET BRANCH=master
C:
CD \
RMDIR /S /Q %ROOT%
MKDIR %ROOT%
CD %ROOT%
git clone -b %BRANCH% --recursive git@github.com:BryanApellanes/Bam.Net.git