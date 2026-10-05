@echo off
:: Pedir permisos de Administrador para poder matar la app trabada
NET SESSION >nul 2>&1
if %errorLevel% neq 0 (
    echo Solicitando permisos para cerrar LockNotch...
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo Cerrando LockNotch forzosamente...
taskkill /F /IM LockNotch.exe >nul 2>&1

echo.
echo Esperando a que el archivo se libere...
timeout /t 2 /nobreak >nul

echo.
echo Compilando la nueva version con el parche de seguridad...
cd /d "%~dp0src\LockNotch"
dotnet build

echo.
echo Iniciando LockNotch...
start "" "%~dp0src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe"

echo.
echo Listo. Ya puedes probar Chrome.
pause
