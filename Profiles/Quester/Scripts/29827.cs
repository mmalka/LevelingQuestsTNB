WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead));

if(unit.IsValid)
{
nManager.Wow.Helpers.Keybindings.DownKeybindings(nManager.Wow.Enums.Keybindings.ACTIONBUTTON1);
Thread.Sleep(questObjective.WaitMs);
nManager.Wow.Helpers.Keybindings.UpKeybindings(nManager.Wow.Enums.Keybindings.ACTIONBUTTON1);

ClickOnTerrain.ClickOnly(unit.Position);

Thread.Sleep(500);

if(questObjective.InternalIndex == 2)
{
	if(unit.GetDistance <= 80)
		nManager.Wow.Helpers.Keybindings.PressKeybindings(nManager.Wow.Enums.Keybindings.ACTIONBUTTON2);
}
else
{
	nManager.Wow.Helpers.Keybindings.PressKeybindings(nManager.Wow.Enums.Keybindings.ACTIONBUTTON2);
}

Thread.Sleep(550);

}
