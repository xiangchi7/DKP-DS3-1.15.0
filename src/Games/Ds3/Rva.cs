namespace DynamicKeyPrompts;

/// Offsets in DarkSoulsIII.exe 1.15.0 (relocated from the 1.15.2 values).
/// Original 1.15.2 values are kept as comments for reference.
static class Rva
{
    public const uint ExpectedTimeDateStamp = 0x598A43EF;   // 1.15.2: 0x639C4DDD

    /// Static pointer to the CSPcKeyConfig singleton.
    public const int KeyConfig = 0x4740090;                 // 1.15.2: 0x47571D0

    /// MsgRepository lookup: (repository, 0, category, id) -> wchar* or null.
    public const int GetMsg = 0x23458A0;                    // 1.15.2: 0x2354090

    /// Replaces one tutorial tag (L"<?kgAttackR?>") in a message: (source, out, tag, replacement markup).
    public const int ReplaceTag = 0xA5CEB0;                 // 1.15.2: 0xA67740

    /// Return address inside the builder of L"<img src='img://%s' vspace='-8'>" after its icon name lookup.
    public const int MarkupBuilderReturn = 0xA5CD4C;        // 1.15.2: 0xA675DC

    /// Registry of the menu tags (conclusion, cancel, ...): button functions called from here are menu buttons.
    public const int MenuTagsBegin = 0xAB75C0, MenuTagsEnd = 0xAB8518;   // 1.15.2: 0xAC1000 / 0xAC3000 (menu-tag registry fn, relocated)

    /// Return address in the builder of the action list shown at objects ("Rest at bonfire" / "Switch action"):
    /// the menu tags it formats mean game actions (Switch action = two-hand), not menu keys.
    public const int ActionListPrompt = 0xA5B9B3;           // 1.15.2: 0xA66233

    /// Return addresses in the Key Bindings screen that draw the Controller column's button icons
    /// (first page, selected row, pages after switching tabs).
    public const int KeyBindingsPadIcon1 = 0xA37525, KeyBindingsPadIcon2 = 0xAAFF7E, KeyBindingsPadIcon3 = 0xAB3378;   // 1.15.2: 0xA41A25 / 0xABA8AE / 0xABDCA8

    /// DLString replace, used by the virtual file system to turn "menu:/x" into "data1:/menu/x"
    /// (ModEngine2's virtual_to_archive_path for Dark Souls III).
    public const int PathReplace = 0x7D5E0;                 // 1.15.2: 0x7D660

    /// A call ModEngine2 removes so that files replaced by loose files load in Dark Souls III.
    public const int LooseFileCheckCall = 0xE93D33;         // 1.15.2: 0xEA1B83
}
