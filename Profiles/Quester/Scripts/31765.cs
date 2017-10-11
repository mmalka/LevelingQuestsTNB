/*
	Cannon script :
	Calculate the angle and adjust AIM to shoot a unit.

*/
    try
            {

                nManager.Wow.ObjectManager.WoWUnit unit = new WoWUnit(0);
                unit = nManager.Wow.ObjectManager.ObjectManager.GetWoWUnitByEntry(questObjective.Entry).Find(x => x.Position.Y <= -748 && !nManagerSetting.IsBlackListed(x.Guid) && x.Position.DistanceTo(ObjectManager.Me.Position) <= 180 && !x.IsDead);	
				
                if (unit != null && unit.IsValid)
                {


                    MovementManager.FaceCTM(unit);
                   // Interact.InteractWith(unit.GetBaseAddress);

                    float zDiff = System.Math.Abs(unit.Position.Z - ObjectManager.Me.Position.Z);
                    float delta = (float)System.Math.Atan(zDiff / unit.GetDistance2D);

                   //Logging.Write("Delta " + delta + "");

                    float currentAngle = 0f;
                    string randomString = Others.GetRandomString(Others.Random(4, 10));

                    currentAngle = Others.ToSingle(Lua.LuaDoString(randomString + "=VehicleAimGetAngle()", randomString));

                    //Logging.Write(currentAngle + " Cur");

                    if (currentAngle > delta)
                    {
                        Lua.LuaDoString("VehicleAimDecrement(" + (currentAngle + delta) + ");");
                    }
                    else
                    {
                        Lua.LuaDoString("VehicleAimIncrement(" + (System.Math.Abs(currentAngle) - delta) + ")");
                    }
					
                    Lua.RunMacroText("/click OverrideActionBarButton1");
					
					Thread.Sleep(1500);
					nManagerSetting.AddBlackList(unit.Guid, 60*1000);
                }


            }
            catch (Exception)
            {
                return false;
            }