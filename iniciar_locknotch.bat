@echo off
cd /d "%~dp0"
title LockNotch Launcher
echo Cerrando instancias previas...
taskkill /F /IM locknotch.exe >nul 2>&1
taskkill /F /IM LockNotch.exe >nul 2>&1
ping 127.0.0.1 -n 2 >nul
echo Iniciando LockNotch (Tauri Release)...
start "" "%~dp0app\src-tauri\target\release\locknotch.exe"
echo LockNotch iniciado correctamente en la parte superior de tu pantalla.
ping 127.0.0.1 -n 3 >nul
