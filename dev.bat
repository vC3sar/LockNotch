@echo off
title LockNotch Dev Manager

:: Solicitar elevacion a Administrador si no se tiene
net session >nul 2>&1
if %errorLevel% neq 0 (
    echo Solicitando privilegios de administrador...
    powershell -Command "Start-Process -FilePath '%0' -Verb RunAs"
    exit /b
)

:: Asegurar que el directorio de trabajo sea la carpeta del script (soluciona error de ruta en modo admin)
cd /d "%~dp0"

:loop
cls
echo ====================================
echo   Control de Desarrollo - LockNotch
echo ====================================
echo.
echo  [ENTER] o cualquier tecla para compilar/reiniciar.
echo  [CTRL + C] para salir.
echo.
pause >nul

echo [1/2] Cerrando instancia previa...
taskkill /F /IM LockNotch.exe /T >nul 2>&1
:: Pequeña pausa para asegurar que el archivo se libere
timeout /t 1 /nobreak >nul

echo [2/2] Lanzando LockNotch en un subproceso...
:: 'start' abre un subproceso independiente y no bloquea este .bat
start "LockNotch Background" /MIN dotnet run --project src/LockNotch

goto loop
