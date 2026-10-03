# LockNotch

Una "Dynamic Island" / notch estilo iPhone para Windows 11, anclada en el borde superior central de la pantalla.

## Requisitos

- Windows 10 2004 (19041) o superior; diseñado para Windows 11
- .NET 8 SDK x64

## Ejecutar

```powershell
dotnet run --project src/LockNotch
```

Compilar:

```powershell
dotnet build
```

## Uso

| Acción | Resultado |
|---|---|
| Pasar el ratón por encima / clic | Expande la isla |
| Sacar el ratón | Se contrae tras ~400 ms |
| Clic derecho | Menú → **Salir de LockNotch** |
| App en pantalla completa (monitor principal) | La isla se oculta |

La ventana no roba el foco, no aparece en la barra de tareas ni en Alt+Tab, y se recoloca automáticamente si cambia la resolución o la escala (DPI).

## Estado

- [x] Fase 1: notch, hora, animación expandir/contraer, pantalla completa, DPI
- [x] Fase 2: música (GlobalSystemMediaTransportControls) con portada, controles y auto-expansión al cambiar de canción
- [ ] Fase 3: batería y clima (Open-Meteo)
- [ ] Fase 4: temperaturas CPU/GPU (requiere administrador)

## Estructura

```
src/LockNotch/
├─ Interop/       P/Invoke y helpers de ventana
├─ Services/      Reloj, pantalla completa (y más en próximas fases)
├─ Models/
├─ ViewModels/    IslandViewModel
├─ Helpers/       Propiedad adjunta animable para el radio del notch
├─ Converters/
└─ Resources/     Estilos
```
