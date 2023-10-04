using System.Collections.Generic;
using UserInterface;

public static class GameSettings
{
    private static readonly Dictionary<UIMenuType, bool> HoldMenu = new ();

    public static void SetUpSettingsDictionaries()
    {
        HoldMenu.Add(UIMenuType.Map, false);
        HoldMenu.Add(UIMenuType.Inventory, false);
        HoldMenu.Add(UIMenuType.QuestLog, false);
        HoldMenu.Add(UIMenuType.ActionBindings, false);
        HoldMenu.Add(UIMenuType.CharacterSheet, false);
    }

    public static bool GetHoldMenu(UIMenuType menuType) => HoldMenu[menuType];

    public static void SetHoldMenu(bool value, UIMenuType menuType) => HoldMenu[menuType] = value;
}