@echo on
rem %1 - path to root (c:\src)
SET ROOT=%1
IF [%1]==[] SET ROOT=C:\src
call .\build_solution.cmd Release %ROOT%
call .\build_bamcore.cmd Release %ROOT%
call .\build_toolkit.cmd
call .\uninstall_toolkit.cmd
call .\install_toolkit.cmd %ROOT%