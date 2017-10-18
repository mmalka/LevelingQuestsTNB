int dynFlag;
uint dynFlags;

foreach (WoWUnit unit in ObjectManager.GetWoWUnitByEntry(questObjective.Entry,questObjective.IsDead))
{

	dynFlag = unit.GetDynamicFlags;
	dynFlags = BitConverter.ToUInt32(BitConverter.GetBytes(dynFlag), 0);

	if ((dynFlags & 0x80) == 0x80)
	{
		nManagerSetting.AddBlackList(unit.Guid, 30 * 1000);
	}

}

return nManager.Wow.Helpers.Quest.IsObjectiveCompleted(questObjective.InternalQuestId, questObjective.InternalIndex, questObjective.Count) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(questObjective.InternalQuestId) || nManager.Wow.Helpers.Quest.IsQuestFlaggedCompletedLUA(questObjective.InternalQuestId);