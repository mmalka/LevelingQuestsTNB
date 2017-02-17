WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreBlackList);

	
if(unit.IsValid)
{
	System.Threading.Thread _worker;
	nManager.Wow.Helpers.Quest.GetSetIgnoreFight = true;
	MovementManager.FindTarget(unit, CombatClass.GetAggroRange);
	Thread.Sleep(100);
	
	if (MovementManager.InMovement)
	{
		return false;
	}
	
	_worker = new System.Threading.Thread(() => nManager.Wow.Helpers.Fight.StartFight(unit.Guid));
	_worker.Start();
	
	if (unit.HealthPercent <= 2)
	{
		Logging.Write("Unit Below 2% HP");
		Fight.StopFight();
		MovementManager.Face(unit);
		Thread.Sleep(2000);
		Interact.InteractWith(unit.GetBaseAddress);
		Thread.Sleep(1000);
		ItemsManager.UseItem(ItemsManager.GetItemNameById(questObjective.UseItemId));
		nManager.Wow.Helpers.Quest.GetSetIgnoreFight = false;
		questObjective.IsObjectiveCompleted = true;
	}
}
return false;