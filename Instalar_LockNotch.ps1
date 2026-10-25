$ErrorActionPreference = "Stop"

$installDir = "$env:LOCALAPPDATA\LockNotch"
$sourceExe = "src\LockNotch\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\publish\LockNotch.exe"
$sourceExt = "lockNotch_chrome"

Write-Host "Instalando LockNotch..." -ForegroundColor Cyan

# 1. Crear carpeta
if (!(Test-Path $installDir)) {
    New-Item -ItemType Directory -Path $installDir | Out-Null
}

# 2. Cerrar si está ejecutándose
Get-Process "LockNotch" -ErrorAction SilentlyContinue | Stop-Process -Force

# 3. Copiar ejecutable
Write-Host "Copiando aplicación..."
Copy-Item -Path $sourceExe -Destination "$installDir\LockNotch.exe" -Force

# 4. Copiar extensión
Write-Host "Copiando extensión de Chrome..."
$extDir = "$installDir\Extension"
if (!(Test-Path $extDir)) {
    New-Item -ItemType Directory -Path $extDir | Out-Null
}
Copy-Item -Path "$sourceExt\*" -Destination $extDir -Recurse -Force

# 5. Crear accesos directos
Write-Host "Creando accesos directos..."
$WshShell = New-Object -comObject WScript.Shell

# Desktop
$desktopShortcut = $WshShell.CreateShortcut("$env:USERPROFILE\Desktop\LockNotch.lnk")
$desktopShortcut.TargetPath = "$installDir\LockNotch.exe"
$desktopShortcut.WorkingDirectory = $installDir
$desktopShortcut.Save()

# Start Menu
$startMenuDir = "$env:APPDATA\Microsoft\Windows\Start Menu\Programs\LockNotch"
if (!(Test-Path $startMenuDir)) {
    New-Item -ItemType Directory -Path $startMenuDir | Out-Null
}
$startMenuShortcut = $WshShell.CreateShortcut("$startMenuDir\LockNotch.lnk")
$startMenuShortcut.TargetPath = "$installDir\LockNotch.exe"
$startMenuShortcut.WorkingDirectory = $installDir
$startMenuShortcut.Save()

# 6. Auto-Inicio (Startup)
$registryPath = "HKCU:\Software\Microsoft\Windows\CurrentVersion\Run"
Set-ItemProperty -Path $registryPath -Name "LockNotch" -Value "$installDir\LockNotch.exe"

# 7. Ejecutar LockNotch
Start-Process "$installDir\LockNotch.exe"

# 8. Mensaje y abrir Chrome
Add-Type -AssemblyName PresentationFramework
$msg = "¡LockNotch se ha instalado correctamente!`n`nSe ha creado un icono en tu Escritorio y se abrirá automáticamente al iniciar Windows.`n`nPara activar la sincronización con Spotify/YouTube:`n1. Se abrirá la pestaña de Extensiones de Chrome.`n2. Activa el 'Modo Desarrollador' (arriba a la derecha).`n3. Dale a 'Cargar Descomprimida'.`n4. Selecciona la carpeta que hemos preparado en:`n   $extDir"
[System.Windows.MessageBox]::Show($msg, "LockNotch - Instalación Completa", "OK", "Information")

Start-Process "chrome.exe" "chrome://extensions/"

Write-Host "¡Instalación Completada!" -ForegroundColor Green
