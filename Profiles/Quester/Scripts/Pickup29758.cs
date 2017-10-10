if(!nManager.Wow.Helpers.Quest.GetLogQuestId().Contains(29758) && !(nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(29758) || nManager.Wow.Helpers.Quest.GetQuestCompleted(29758)))
{
  Lua.RunMacroText("/click QuestFrameAcceptButton");
}

return nManager.Wow.Helpers.Quest.GetLogQuestId().Contains(29758) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(29758) || nManager.Wow.Helpers.Quest.GetQuestCompleted(29758);
  