echo %1 > major.ver
echo %2 > minor.ver
echo %3 > patch.ver

assinf /si /v:"%1.%2.%3" /root:..\..\ /nuspecRoot:"."