if(nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(31777) || nManager.Wow.Helpers.Quest.GetQuestCompleted(31777))
	return true;
else if(nManager.Wow.Helpers.ItemsManager.GetItemCount(89163) == 0)
{
	Logging.Write("REQUEST");
	nManager.Wow.Helpers.Quest.RequestResetObjectives = true;
questObjective.IsObjectiveCompleted = true;
	}
return false;
