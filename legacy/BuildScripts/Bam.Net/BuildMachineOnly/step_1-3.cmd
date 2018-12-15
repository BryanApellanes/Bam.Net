rem %1 root, %2 branch
SET ROOT=%1
IF [%1]==[] SET ROOT=C:\src
SET BRANCH=%2
IF [%2]==[] SET BRANCH=master 
call step_1-clone.cmd %ROOT% %BRANCH%
call step_2-nugetrestore.cmd %ROOT%
call step_3-build_and_install_toolkit.cmd %ROOT%