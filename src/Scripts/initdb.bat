REM "Init DB"
pause

cd C:\Windows\Microsoft.NET\Framework\v2.0.50727

aspnet_regsql -S GECKO\SQLEXPRESS -E -sqlexportonly C:\dev\musiccompanysvn\src\scripts\aspnet.sql -A mr -d MusicCompany

pause