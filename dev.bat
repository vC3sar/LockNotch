@echo off
title LockNotch Dev Manager

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
