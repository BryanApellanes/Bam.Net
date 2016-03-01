SET ROOT=%1
IF [%1]==[] SET ROOT=C:\src
SET BRANCH=%2
IF [%2]==[] SET BRANCH=master
call step_1-3.cmd %ROOT% %BRANCH%
call test_step_1-run_tests_w_coverage.cmd %ROOT%
call test_step_2-generate_coverage_report.cmd %ROOT%