Logging.Write("Quest 27789 - Starting cannon on troggs");
  
nManager.Wow.Helpers.Quest.GetSetIgnoreFight = true;

uint i = 1;

while(!nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(27789))
{
  WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead));

	if (unit.IsValid)
	{
    Logging.Write("Target is "+ unit.Name + " @ range:" +
                      nManager.Wow.ObjectManager.ObjectManager.Me.Position.DistanceTo(unit.Position) );

		MovementManager.FaceCTM(unit);
		Thread.Sleep(500 + Usefuls.Latency);
		Lua.RunMacroText("/click OverrideActionBarButton1");
   	
   	if (  nManager.Wow.Helpers.Quest.IsObjectiveCompleted(27789, i, 1) )
   	{
        Logging.Write("Quest 27789 - trogg wave " + i + " completed.");
        i++;
   	}
		Thread.Sleep(1000);
		if (unit.IsValid)
		{
        nManagerSetting.AddBlackList(unit.Guid, 5*1000);
		}
		
	}
}
Logging.Write("Quest 27789 - quest marked completed, event done.");
nManager.Wow.Helpers.Quest.GetSetIgnoreFight = false;
return true;
