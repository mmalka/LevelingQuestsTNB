
return ObjectManager.Me.Position.DistanceTo(questObjective.Position) <= 5f || nManager.Wow.Helpers.Quest.GetLogQuestId().Contains(9817) ||
 nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(9817) 
 || nManager.Wow.Helpers.Quest.GetQuestCompleted(9817);