@echo off
setlocal

:: Obtener la ruta absoluta del LockNotch.exe
set "EXE_PATH=%~dp0..\src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe"
:: Reemplazar slashes para JSON
set "EXE_PATH=%EXE_PATH:\=\\%"

:: Generar el JSON dinamicamente
echo {> "%~dp0com.locknotch.downloads.json"
echo   "name": "com.locknotch.downloads",>> "%~dp0com.locknotch.downloads.json"
echo   "description": "LockNotch Native Messaging Host",>> "%~dp0com.locknotch.downloads.json"
echo   "path": "%EXE_PATH%",>> "%~dp0com.locknotch.downloads.json"
echo   "type": "stdio",>> "%~dp0com.locknotch.downloads.json"
echo   "allowed_origins": [>> "%~dp0com.locknotch.downloads.json"
echo     "chrome-extension://*/">> "%~dp0com.locknotch.downloads.json"
echo   ]>> "%~dp0com.locknotch.downloads.json"
echo }>> "%~dp0com.locknotch.downloads.json"

:: Wait, wildcards in allowed_origins are NOT supported by Chrome for extensions, only for apps (chrome-extension://* is sometimes invalid). 
:: The user MUST replace the extension ID in the file manually or we must know it. 
:: Actually, let's keep the user replacement for now.

echo Registro del host de Chrome Native Messaging...
reg add "HKCU\Software\Google\Chrome\NativeMessagingHosts\com.locknotch.downloads" /ve /t REG_SZ /d "%~dp0com.locknotch.downloads.json" /f

echo.
echo Listo. No olvides cargar la extension sin empaquetar en Chrome,
echo copiar el ID de la extension, y pegarlo en el archivo:
echo com.locknotch.downloads.json
echo en la propiedad "allowed_origins" (por ejemplo: "chrome-extension://abcdefghijklmnop/").
echo Despues reinicia Chrome o la extension.
pause
