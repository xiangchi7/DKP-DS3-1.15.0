Dynamic Key Prompts 0.1.4 — ported to Dark Souls III 1.15.0 (drop-in build)
============================================================================

Replaces gamepad button prompts with the keyboard/mouse keys you actually bound.
This build is recompiled for the 1.15.0 executable and carries two adaptations so
it can be installed the same way as the original mod.

Installation (same as the original mod)
---------------------------------------
Copy these two items next to DarkSoulsIII.exe:

    xinput1_3.dll
    DynamicKeyPrompts\   (contains DynamicKeyPrompts.dll + DynamicKeyPrompts.ini)

i.e.:

    ...\DARK SOULS III\Game\xinput1_3.dll
    ...\DARK SOULS III\Game\DynamicKeyPrompts\DynamicKeyPrompts.dll
    ...\DARK SOULS III\Game\DynamicKeyPrompts\DynamicKeyPrompts.ini

Then start the game from Steam as usual. No launcher script is needed.
Uninstall: delete xinput1_3.dll and the DynamicKeyPrompts folder.

Note: if another mod already uses xinput1_3.dll, rename it and use a dinput8.dll
variant instead. This package's loader is an xinput1_3 proxy.

Two key changes vs. the official 0.1.4
--------------------------------------
1. Loader (xinput1_3.dll, src/Loader/dllmain.cpp):
   Detects the 1.15.0 executable (PE TimeDateStamp 0x598A43EF) and skips dearxan's
   entry-point blocking (dearxan uses 1.15.2-specific addresses and crashes 1.15.0),
   starting the core once the game's own code runs instead. So 1.15.0 no longer crashes.

2. Icon-mode keycap atlas (src/Games/Ds3/KeyIcons.cs):
   The carrier was changed from menu/05_dummy.tpf.dcx to menu/01_common.tpf.dcx.
   Reason: the gamepad button icons (KG_OK etc.) live in 01_common (the always-loaded
   HUD atlas), while 05_dummy is only loaded while a menu is open. With the keycaps in
   01_common, the centered interaction prompt ("Rest at bonfire" / "Talk") now shows
   the E keycap too.

All 1.15.0 addresses (src/Games/Ds3/Rva.cs)
-------------------------------------------
    ExpectedTimeDateStamp 0x598A43EF   (official 1.15.2: 0x639C4DDD)
    KeyConfig             0x4740090
    GetMsg                0x23458A0
    ReplaceTag            0xA5CEB0
    MarkupBuilderReturn   0xA5CD4C
    MenuTagsBegin/End     0xAB75C0 / 0xAB8518
    ActionListPrompt      0xA5B9B3   (estimated)
    KeyBindingsPadIcon1/2/3  0xA37525 / 0xAAFF7E / 0xAB3378
    PathReplace           0x7D5E0
    LooseFileCheckCall    0xE93D33

Configuration
-------------
DynamicKeyPrompts\DynamicKeyPrompts.ini:
    Style=icons        key/mouse keycap icons (default, recommended)
    Style=text         text labels such as [E]
    IconTheme=minimal  keycap look: dark / minimal / silver
    Diagnostics=0      0 = quiet; 1 = verbose DynamicKeyPrompts.log

Files
-----
    xinput1_3.dll                              loader (skips dearxan on 1.15.0)
    DynamicKeyPrompts\DynamicKeyPrompts.dll    core (11 relocated addresses + 01_common atlas)
    DynamicKeyPrompts\DynamicKeyPrompts.ini    configuration
    source-changes\Rva.cs / KeyIcons.cs / Prompts.cs / dllmain.cpp   modified sources
    LICENSE                                    MIT license of Dynamic Key Prompts (c) 2026 HappyEntity
    LICENSE-MinHook.txt                         BSD 2-Clause licenses of MinHook and the Hacker Disassembler Engine
