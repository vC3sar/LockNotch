@echo off
color 0b
echo ===================================================
echo              Instalador de LockNotch
echo ===================================================
echo.
echo Iniciando el proceso de instalacion...

powershell -NoProfile -ExecutionPolicy Bypass -File "%~dp0Instalar_LockNotch.ps1"

pause
