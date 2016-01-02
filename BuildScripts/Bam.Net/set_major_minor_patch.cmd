set /p major=<major.ver
call set_nuspec_version /major:%major%

set /p minor=<minor.ver
call set_nuspec_version /minor:%minor%

set /p patch=<patch.ver
call set_nuspec_version /patch:%patch%