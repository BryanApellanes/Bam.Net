@echo on
rem %1 - path to root (c:\src)
call .\build_solution.cmd Debug %1
call .\build_bamcore.cmd Debug %1
call .\build_toolkit_debug.cmd
call .\uninstall_toolkit_debug.cmd
call .\install_toolkit_debug.cmd %1