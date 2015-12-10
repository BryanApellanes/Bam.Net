call step_1-clone.cmd
%1
call step_2-nugetrestore.cmd
%1
call step_3-build_and_install_toolkit.cmd