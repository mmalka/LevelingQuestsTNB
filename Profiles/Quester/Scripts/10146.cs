	WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,questObjective.AllowPlayerControlled);

if(unit != null && unit.IsValid && unit.Position.DistanceTo(ObjectManager.Me.Position) <= questObjective.Range)
{
	if (ItemsManager.GetItemCount(questObjective.UseItemId) <= 0 || ItemsManager.IsItemOnCooldown(questObjective.UseItemId) || !ItemsManager.IsItemUsable(questObjective.UseItemId))
	return false;

	Logging.Write("Use Bomb!");
	ItemsManager.UseItem(questObjective.UseItemId, unit.Position);
	if(questObjective.WaitMs > 0)
		Thread.Sleep(questObjective.WaitMs);
		
	questObjective.IsObjectiveCompleted = true;
	
}     