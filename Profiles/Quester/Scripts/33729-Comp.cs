if(	nManager.Wow.Helpers.Quest.IsObjectiveCompleted(33729, 1, 100) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(33729) || nManager.Wow.Helpers.Quest.GetQuestCompleted(33729))
{
	Usefuls.EjectVehicle();
	return true;
}
return false;