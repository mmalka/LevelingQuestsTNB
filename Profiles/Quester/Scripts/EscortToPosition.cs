/*
Check code at the end to set where to get the NPC if you die

*/

WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
	questObjective.AllowPlayerControlled);
Point pos = ObjectManager.Me.Position; /* Initialize or getting an error */
int q = QuestID; /* not used but otherwise getting warning QuestID not used */
uint baseAddress = 0;

questObjective.Range = questObjective.Range == 0 ? 5 : questObjective.Range;

/* If Entry found continue*/
if (unit.IsValid)
{

	if(questObjective.DeactivateMount)
		MountTask.DismountMount();
	
	//If Unit too far from us, wait for it
	if(unit.Position.DistanceTo(ObjectManager.Me.Position) >=30) 
	{
		MovementManager.StopMove();
		while(unit.Position.DistanceTo(ObjectManager.Me.Position) >=15 && unit.IsValid && !unit.IsDead && !unit.InCombat)
		{
			if(ObjectManager.Me.IsDeadMe)
				return false;
			if(unit.InCombat)
				break; //Exit loop to kill unit target
			Thread.Sleep(500);
			//Refresh unit
		//	unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
	//questObjective.AllowPlayerControlled);
		}
	}
	
	MovementManager.Go(PathFinder.FindPath(ObjectManager.Me.Position,questObjective.Position));
	
	while(MovementManager.InMovement && questObjective.Position.DistanceTo(ObjectManager.Me.Position) > 5f && unit.IsValid && !unit.IsDead && !unit.InCombat)
	{	
		if(unit.Position.DistanceTo(ObjectManager.Me.Position) >=30)
			return false;
		if (ObjectManager.Me.IsDeadMe)
			return false;
		if(unit.InCombat)
			break; //Exit loop to kill unit target
		if(ObjectManager.Me.InCombat)
		{
			MountTask.DismountMount();
			return false; //Let the bot kill the hostiles!
		}	
		Thread.Sleep(500);
		//Refresh unit
		//unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
	//questObjective.AllowPlayerControlled);
	}
	
	if(unit.InCombat)
	{
		Logging.Write("Defend Unit");
		nManager.Wow.Helpers.Fight.StartFight(unit.Target);
	}

	Thread.Sleep(100 + Usefuls.Latency); /* ZZZzzzZZZzz */

	/* Position Reached */
	MovementManager.StopMove();
	//MountTask.DismountMount();

	Thread.Sleep(Usefuls.Latency + 150);

	/* Wait if necessary */
	if (questObjective.WaitMs > 0)
		Thread.Sleep(questObjective.WaitMs);
	
	return true;
}
else //Npc Dead probably get him back, use ExtraPoint for the position of the NPC and Extra ID to get it back. Gossip too if necessary
{
	if(questObjective.ExtraInt == 0)
		return false;
	
	WoWUnit npc = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.ExtraInt, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
		questObjective.AllowPlayerControlled);
	
	MovementManager.GoToLocationFindTarget(questObjective.ExtraPoint);
	
	if(npc.IsValid && npc.GetDistance <= questObjective.Range)
	{
		/* Target Reached */
		MovementManager.StopMove();	
	}
	else
	{
		if (MovementManager.InMovement)
			return false;
		if (baseAddress <= 0)
			return false;
		if (baseAddress > 0 && (npc.IsValid && npc.GetDistance > questObjective.Range))
			return false;	
	}
	Interact.InteractWith(npc.GetBaseAddress);
	
	Thread.Sleep(500);
	
	nManager.Wow.Helpers.Quest.SelectGossipOption(questObjective.GossipOptionsInteractWith);
}
return true;