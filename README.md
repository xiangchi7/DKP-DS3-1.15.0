# Dynamic Key Prompts 0.1.4 — ported to Dark Souls III 1.15.0 (drop-in build)

Replaces gamepad button prompts with the keyboard/mouse keys you actually bound.
This build is recompiled for the 1.15.0 executable and carries two adaptations so
it can be installed the same way as the original mod.

Versioning: the release tag `v0.1.4-ds3-1.15.0` means **upstream version 0.1.4,
ported to game version 1.15.0**.

## Installation (same as the original mod)

Copy these two items next to `DarkSoulsIII.exe`:

| Item | Destination |
|---|---|
| `xinput1_3.dll` | `...\DARK SOULS III\Game\xinput1_3.dll` |
| `DynamicKeyPrompts\` | `...\DARK SOULS III\Game\DynamicKeyPrompts\` |

i.e.

```
...\DARK SOULS III\Game\xinput1_3.dll
...\DARK SOULS III\Game\DynamicKeyPrompts\DynamicKeyPrompts.dll
...\DARK SOULS III\Game\DynamicKeyPrompts\DynamicKeyPrompts.ini
```

Then start the game from Steam as usual. No launcher script is needed.

**Uninstall:** delete `xinput1_3.dll` and the `DynamicKeyPrompts` folder.

> **Note:** if another mod already uses `xinput1_3.dll`, rename it and use a
> `dinput8.dll` variant instead. This package's loader is an `xinput1_3` proxy.

## Two key changes vs. the official 0.1.4

### 1. Loader (`xinput1_3.dll`, upstream `src/Loader/dllmain.cpp`)

Detects the 1.15.0 executable (PE `TimeDateStamp` `0x598A43EF`) and skips
dearxan's entry-point blocking (dearxan uses 1.15.2-specific addresses and crashes
1.15.0), starting the core once the game's own code runs instead. So 1.15.0 no
longer crashes.

### 2. Icon-mode keycap atlas (upstream `src/Games/Ds3/KeyIcons.cs`)

The carrier was changed from `menu/05_dummy.tpf.dcx` to `menu/01_common.tpf.dcx`.

Reason: the gamepad button icons (`KG_OK` etc.) live in `01_common` (the
always-loaded HUD atlas), while `05_dummy` is only loaded while a menu is open.
With the keycaps in `01_common`, the centered interaction prompt
("Rest at bonfire" / "Talk") now shows the `E` keycap too.

## All 1.15.0 addresses (`src/Games/Ds3/Rva.cs`)

| Symbol | 1.15.0 value | Note |
|---|---|---|
| `ExpectedTimeDateStamp` | `0x598A43EF` | official 1.15.2: `0x639C4DDD` |
| `KeyConfig` | `0x4740090` | static pointer to the `CSPcKeyConfig` singleton |
| `GetMsg` | `0x23458A0` | message repository lookup |
| `ReplaceTag` | `0xA5CEB0` | replaces one tutorial tag in a message |
| `MarkupBuilderReturn` | `0xA5CD4C` | return address inside the icon markup builder |
| `MenuTagsBegin` / `MenuTagsEnd` | `0xAB75C0` / `0xAB8518` | menu-tag registry |
| `ActionListPrompt` | `0xA5B9B3` | estimated; "Rest at bonfire" action list |
| `KeyBindingsPadIcon1/2/3` | `0xA37525` / `0xAAFF7E` / `0xAB3378` | Key Bindings screen, controller column |
| `PathReplace` | `0x7D5E0` | `DLString` replace used by the virtual file system |
| `LooseFileCheckCall` | `0xE93D33` | call removed so loose files can load |

## Configuration

`DynamicKeyPrompts\DynamicKeyPrompts.ini`:

| Key | Values |
|---|---|
| `Enabled` | `0` = the mod does nothing |
| `Diagnostics` | `1` = more detailed `DynamicKeyPrompts.log` |
| `Style` | `icons` — key / mouse icons (written to `DynamicKeyPrompts\cache\ds3` on first use); `text` — key names as text, e.g. `[E]` |
| `IconTheme` | keycap look: `dark` (stone plate, bronze rim) / `minimal` (frame only) / `silver` |
| `Labels` | which binding to show when an action has both a key and a mouse button: `auto` / `keyboard` / `mouse` / `both` |
| `Format` | text style only; `{0}` is the key name, e.g. `[{0}]` → `[E]` |
| `Separator` | between keys of a stick / d-pad prompt when names are longer than one letter |
| `Color` | optional key label colour, `RRGGBB`; empty = normal text colour |

## Files

| File | Purpose |
|---|---|
| `xinput1_3.dll` | loader (skips dearxan on 1.15.0) — ships in the release, not in this repository |
| `DynamicKeyPrompts\DynamicKeyPrompts.dll` | core (11 relocated addresses + `01_common` atlas) — ships in the release |
| `DynamicKeyPrompts\DynamicKeyPrompts.ini` | configuration — ships in the release |
| `src/Loader/dllmain.cpp` | modified loader source |
| `src/Games/Ds3/KeyIcons.cs` | modified core source (keycap atlas carrier) |
| `src/Games/Ds3/Prompts.cs` | modified core source |
| `src/Games/Ds3/Rva.cs` | modified core source (1.15.0 addresses) |
| `LICENSE` | MIT license of Dynamic Key Prompts (c) 2026 HappyEntity |
| `LICENSE-MinHook.txt` | BSD 2-Clause licenses of MinHook and the Hacker Disassembler Engine |

The `src/` tree contains only the **modified** files, laid out with their upstream
paths. The full upstream projects are not included, so these four files cannot be
built on their own.
