  string randomString = Others.GetRandomString(Others.Random(4, 10));
               
	Lua.LuaDoString("desc, type, done = GetQuestLogLeaderBoard(2," + 34461 + ") if done then " + randomString + "= 1 else " + randomString + " = 0 end");
	string result = Lua.GetLocalizedText(randomString);
	return result == 1;