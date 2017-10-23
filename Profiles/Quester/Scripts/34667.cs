  // nManager.Wow.ObjectManager.WoWUnit Worgen = nManager.Wow.ObjectManager.ObjectManager.GetNearestWoWUnit(nManager.Wow.ObjectManager.ObjectManager.GetWoWUnitByEntry(45270));
try //Strange problem here, the bot is causing an Error : System.NullReferenceException: Object reference not set to an instance of an object. at Main.Script(QuestObjective& questObjective)
{
              
	WoWUnit unit = nManager.Wow.ObjectManager.ObjectManager.GetWoWUnitByEntry(questObjective.Entry).Find(x => x.Position.DistanceTo(ObjectManager.Me.Position) < 100 && !x.IsDead);
		
	if (unit != null && unit.IsValid)
	{
			
		MovementManager.FaceCTM(unit);
		Interact.InteractWith(unit.GetBaseAddress);
		
		Lua.RunMacroText("/click OverrideActionBarButton1");
		
		nManagerSetting.AddBlackList(unit.Guid, 60*1000);
		
		Thread.Sleep(1000);
	}  

  
}
catch (Exception)
{
	return false;
}