//Ask Arasal if you want to know how to DUMP followers IDs and Mission IDs

string addFollowerToMission = "";

addFollowerToMission = 
	"local followers  = C_Garrison.GetFollowers(1);
	for i = 1, #followers  do
		if (followers[i].garrFollowerID == 34)  then  
			C_Garrison.AddFollowerToMission(2, followers[i].followerID);
			break;
		end
	end ";

Lua.LuaDoString(addFollowerToMission);

Thread.Sleep(1500);

Lua.LuaDoString("C_Garrison.StartMission(2);");

Thread.Sleep(1500);

Lua.LuaDoString("GarrisonMissionFrame.CloseButton:Click();");