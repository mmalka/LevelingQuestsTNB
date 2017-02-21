//_, _, standingID, _, _, _, _, _, _, _, _, _, _, _, _, _= GetFactionInfoByID(970);
string neutral;
string randomString = Others.GetRandomString(Others.Random(4, 10));
neutral = Lua.LuaDoString("_, _," + randomString + ", _, _, _, _, _, _, _, _, _, _, _, _, _= GetFactionInfoByID(970)",randomString);

if(neutral == "4")
	return true;
	
return false;