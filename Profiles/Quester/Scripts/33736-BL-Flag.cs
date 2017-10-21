int dynFlag;
uint dynFlags;

foreach (WoWGameObject node in ObjectManager.GetWoWGameObjectById(questObjective.Entry))
{

	dynFlag = node.GetDynamicFlags;
	dynFlags = BitConverter.ToUInt32(BitConverter.GetBytes(dynFlag), 0);

	if ((dynFlags & 0x4) != 0x4)
	{
		nManagerSetting.AddBlackList(node.Guid, 30 * 1000);
	}

}

return nManager.Wow.Helpers.Quest.IsObjectiveCompleted(questObjective.InternalQuestId, questObjective.InternalIndex, questObjective.Count) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(questObjective.InternalQuestId) || nManager.Wow.Helpers.Quest.IsQuestFlaggedCompletedLUA(questObjective.InternalQuestId);