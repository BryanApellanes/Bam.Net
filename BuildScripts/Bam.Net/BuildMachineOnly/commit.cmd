xcopy z:\Git_Repos\%1 c:\src\ /E /Y /H
C:
CD \src\%1
git add --all
git commit -m 'v%2.%3.%4'
git tag -a v%2.%3.%4 -m "%5"
git push
CD \
z:
xcopy c:\src\%1\.git z:\Git_Repos\%1\ /E /Y /H
