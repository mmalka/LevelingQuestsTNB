//CHECK if Chen is there
            Point firstPos = new Point("-748.7717 ; 1324.198 ; 146.7151 ; None");
            Point secondPos = new Point("-804.7104 ; 1265.37 ; 146.6836 ; None");
            Point thirdPos = new Point("-751.927 ; 1334.837 ; 162.6354 ; None");
            int _currentStage = 0;

			bool ElemValid = false;
			bool WukValid = false;
			
            const int _STAGE1 = 1;
            const int _STAGE2 = 2;
            const int _STAGE3 = 3;

			if(questObjective.ExtraObject1 is bool)
			{
				ElemValid = (bool)questObjective.ExtraObject1;
			}
			
			if(questObjective.ExtraObject2 is bool)
			{
				WukValid = (bool)questObjective.ExtraObject2;
			}
			
            if (questObjective.ExtraInt == 0)
                questObjective.ExtraInt = 1;

            _currentStage = questObjective.ExtraInt;


            WoWUnit chen = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(56133, false), false, true, true);
            if (chen.IsValid)
            {
                MovementManager.Face(chen);
                Interact.InteractWith(chen.GetBaseAddress);
                Thread.Sleep(250 + Usefuls.Latency);
                nManager.Wow.Helpers.Quest.SelectGossipOption(1);
                Thread.Sleep(5000);
                return false;
            }

            if (_currentStage == _STAGE1)
            {
                WoWUnit elemental = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(56684, false), false,true,true);
                if (elemental.IsValid)
                {
					questObjective.ExtraObject1 = true;
					
                    if (!elemental.IsDead && !elemental.NotAttackable)
                    {
                        if (elemental.GetDistance > 5)
                        {
							Interact.InteractWith(elemental.GetBaseAddress);
                            MovementManager.GoToLocationFindTarget(elemental.Position, 5);

                            if (MovementManager.InMovement)
                                return false;

                            if (elemental.GetDistance >= 5)
                                return false;
                        }
						MovementManager.Face(elemental);
                        Thread.Sleep(250);
                        Lua.RunMacroText("/click OverrideActionBarButton1");
                        Thread.Sleep(800);
                    }
                }
				else if(ElemValid)
				{
					//Elem Dead, go to next stage
					questObjective.ExtraInt = _STAGE2;
					Logging.Write("Stage 1 done");
				}
                else
                {
                    MovementManager.GoToLocationFindTarget(firstPos, 5);

                    if (MovementManager.InMovement)
                        return false;

                    if (firstPos.DistanceTo(ObjectManager.Me.Position)  >= 5)
                        return false;
                }
            } //End Stage 1

            if (_currentStage == _STAGE2)
            {
                WoWUnit WukWuk = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(56691, false), false, true, true);
                if (WukWuk.IsValid)
                {
					questObjective.ExtraObject2 = true;
					
                    if (!WukWuk.IsDead && !WukWuk.NotAttackable)
                    {
                        if (WukWuk.GetDistance > 5)
                        {
							Interact.InteractWith(WukWuk.GetBaseAddress);
                            MovementManager.GoToLocationFindTarget(WukWuk.Position, 5);

                            if (MovementManager.InMovement)
                                return false;

                            if (WukWuk.GetDistance >= 5)
                                return false;
                        }
						MovementManager.Face(WukWuk);
                        Thread.Sleep(250);
                        Lua.RunMacroText("/click OverrideActionBarButton1");
                        Thread.Sleep(800);
                    }
                }
                else if(WukValid)
				{
					//WukWuk Dead, go to next stage
					questObjective.ExtraInt = _STAGE3;
					Logging.Write("Stage 2 done");
				}
				else
                {
                    MovementManager.GoToLocationFindTarget(secondPos, 5);

                    if (MovementManager.InMovement)
                        return false;

                    if (secondPos.DistanceTo(ObjectManager.Me.Position) >= 5)
                        return false;
                }
            } //End Stage 2

            if (_currentStage == _STAGE3)
            {

                MovementManager.GoToLocationFindTarget(thirdPos, 5);

                if (MovementManager.InMovement)
                    return false;

                if (thirdPos.DistanceTo(ObjectManager.Me.Position) >= 5)
                    return false;


            }