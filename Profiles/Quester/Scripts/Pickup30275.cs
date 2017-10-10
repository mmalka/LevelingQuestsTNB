if(!nManager.Wow.Helpers.Quest.GetLogQuestId().Contains(30275) && !(nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(30275) || nManager.Wow.Helpers.Quest.GetQuestCompleted(30275)))
{
  Lua.RunMacroText("/click QuestFrameAcceptButton");
}

return nManager.Wow.Helpers.Quest.GetLogQuestId().Contains(30275) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(30275) || nManager.Wow.Helpers.Quest.GetQuestCompleted(30275);
  