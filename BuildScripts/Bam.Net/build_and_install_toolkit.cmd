@echo on
rem %1 - path to root (c:\src)
call .\build_solution.cmd Release v4.5 %1
call .\build_toolkit.cmd
call .\uninstall_toolkit.cmd
call .\install_toolkit.cmd %1