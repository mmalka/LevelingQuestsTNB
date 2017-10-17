string randomString = Others.GetRandomString(Others.Random(4, 10));
int stageCheck = questObjective.ExtraInt;

int currentStage = Others.ToInt32( Lua.LuaDoString(" _," + randomString + ",_ = C_Scenario.GetInfo();", randomString));

return currentStage <= stageCheck;