call .\build_solution.cmd Debug v4.5 %1
call .\build_toolkit_debug.cmd
call .\uninstall_toolkit_debug.cmd
call .\install_toolkit_debug.cmd %1