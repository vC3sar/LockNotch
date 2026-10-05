@echo off
:: Pedir permisos de Administrador automáticamente
NET SESSION >nul 2>&1
if %errorLevel% neq 0 (
    echo Solicitando permisos de administrador para poder cerrar LockNotch...
    powershell -Command "Start-Process '%~f0' -Verb RunAs"
    exit /b
)

echo Cerrando LockNotch...
taskkill /IM LockNotch.exe /F >nul 2>&1

echo.
echo Compilando nueva version...
cd "%~dp0src\LockNotch"
dotnet build

echo.
echo Abriendo Chrome e instalando extension automaticamente...
set "EXT_PATH=%~dp0lockNotch_chrome"
start "" "chrome.exe" "--load-extension=%EXT_PATH%"

echo.
echo Esperando a que Chrome registre la extension...
timeout /t 5 /nobreak >nul

echo.
echo Extrayendo ID de Chrome y configurando Native Messaging...
powershell -Command "$prefsPath = \"$env:LOCALAPPDATA\Google\Chrome\User Data\Default\Preferences\"; $extPath = '%EXT_PATH%'; if (Test-Path $prefsPath) { $prefs = Get-Content $prefsPath -Raw | ConvertFrom-Json; $extId = $null; $settings = $prefs.extensions.settings; foreach ($prop in $settings.psobject.properties) { $path = $prop.Value.path; if ($path -ne $null -and $path -replace '\\\\', '\' -eq $extPath) { $extId = $prop.Name; break } }; if ($extId) { Write-Output 'ID de Extension encontrado: ' $extId; $jsonPath = \"$extPath\com.locknotch.downloads.json\"; $exePath = \"$extPath\..\src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe\"; $exePath = (Resolve-Path $exePath).Path -replace '\\', '\\\\'; $jsonObj = @{ name = 'com.locknotch.downloads'; description = 'LockNotch Native Messaging Host'; path = $exePath; type = 'stdio'; allowed_origins = @(\"chrome-extension://$extId/\") }; $jsonObj | ConvertTo-Json -Depth 10 | Set-Content $jsonPath; $regPath = 'HKCU:\Software\Google\Chrome\NativeMessagingHosts\com.locknotch.downloads'; if (!(Test-Path $regPath)) { New-Item -Path $regPath -Force | Out-Null }; Set-ItemProperty -Path $regPath -Name '(default)' -Value $jsonPath; Write-Output 'Host registrado con exito.' } else { Write-Output 'No se pudo detectar el ID. Verifica en Chrome.' } } else { Write-Output 'No se encontro Chrome Preferences.' }"

echo.
echo Iniciando LockNotch...
start "" "%~dp0src\LockNotch\bin\x64\Debug\net8.0-windows10.0.19041.0\win-x64\LockNotch.exe"

echo.
echo Proceso finalizado. Ve a Chrome, haz una descarga y prueba.
pause
