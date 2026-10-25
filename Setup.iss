[Setup]
AppName=LockNotch
AppVersion=1.0
DefaultDirName={autopf}\LockNotch
DefaultGroupName=LockNotch
UninstallDisplayIcon={app}\LockNotch.exe
Compression=lzma2
SolidCompression=yes
OutputDir=.\Instalador
OutputBaseFilename=LockNotch_Setup
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=lowest

[Tasks]
Name: "desktopicon"; Description: "Crear acceso directo en el escritorio"; GroupDescription: "Iconos adicionales:"
Name: "startup"; Description: "Iniciar LockNotch automáticamente al encender Windows"; GroupDescription: "Inicio automático:"

[Files]
; Ejecutable principal auto-contenido
Source: "src\LockNotch\bin\x64\Release\net8.0-windows10.0.19041.0\win-x64\publish\LockNotch.exe"; DestDir: "{app}"; Flags: ignoreversion
; Extensión de Chrome
Source: "lockNotch_chrome\*"; DestDir: "{app}\Extension"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{group}\LockNotch"; Filename: "{app}\LockNotch.exe"
Name: "{autodesktop}\LockNotch"; Filename: "{app}\LockNotch.exe"; Tasks: desktopicon
Name: "{userstartup}\LockNotch"; Filename: "{app}\LockNotch.exe"; Tasks: startup

[Run]
Filename: "{app}\LockNotch.exe"; Description: "Lanzar LockNotch ahora"; Flags: nowait postinstall skipifsilent
Filename: "chrome.exe"; Parameters: "chrome://extensions/"; Description: "Abrir Extensiones de Chrome (Para instalar la extensión)"; Flags: postinstall shellexec runasoriginaluser

[Code]
procedure CurStepChanged(CurStep: TSetupStep);
var
  ExtPath: String;
begin
  if CurStep = ssPostInstall then
  begin
    ExtPath := ExpandConstant('{app}\Extension');
    MsgBox('¡LockNotch se ha instalado correctamente!' + #13#10 + #13#10 + 
           'Para que funcione la música con Chrome, se abrirá la pestaña de Extensiones.' + #13#10 + #13#10 +
           '1. Activa el "Modo Desarrollador" (arriba a la derecha).' + #13#10 +
           '2. Haz clic en "Cargar Descomprimida".' + #13#10 +
           '3. Selecciona esta carpeta: ' + ExtPath, mbInformation, MB_OK);
  end;
end;
