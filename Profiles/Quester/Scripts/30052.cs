/* 
ExtraInt = Bomb Id

*/
try
{
	if (questObjective.Hotspots.Count <= 0)
	{
		/* Objective CSharpScript with script UseItemWithHotSpots requires valid "Hotspots" */
		Logging.Write("UseItemWithHotSpots requires valid 'HotSpots'.");
		questObjective.IsObjectiveCompleted = true;
		return false;
	}

	if(questObjective.Range == 0)
		questObjective.Range = 5;
	
	WoWGameObject node = ObjectManager.GetNearestWoWGameObject(ObjectManager.GetWoWGameObjectById(questObjective.ExtraInt));
	WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
		questObjective.AllowPlayerControlled);
	WoWUnit GaiLan = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(57385, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
	questObjective.AllowPlayerControlled);
	Point pos = ObjectManager.Me.Position; /* Initialize or getting an error */
	uint baseAddress = 0;

	if(GaiLan.IsValid && !unit.IsValid) //Event not started
	{
		MovementManager.FindTarget(GaiLan,questObjective.Range);
	
		if(GaiLan.IsValid && GaiLan.GetDistance <= questObjective.Range)
		{
			/* Target Reached */
			MovementManager.StopMove();	
		}
		else
		{
			if (MovementManager.InMovement)
				return false;
			if (baseAddress <= 0)
				return false;
			if (baseAddress > 0 && (GaiLan.IsValid && GaiLan.GetDistance > questObjective.Range))
				return false;	
		}
		Interact.InteractWith(GaiLan.GetBaseAddress);
	
		Thread.Sleep(500);
		if(nManager.Wow.Helpers.Quest.GetNumGossipOptions() == 2)
			nManager.Wow.Helpers.Quest.SelectGossipOption(questObjective.GossipOptionsInteractWith);
	}
	
	/* If Entry found continue, otherwise continue checking around HotSpots */
	if ((unit.IsValid && (!nManagerSetting.IsBlackListedZone(unit.Position) && !nManagerSetting.IsBlackListed(unit.Guid) || questObjective.IgnoreBlackList )) ||
		(node.IsValid && (!nManagerSetting.IsBlackListedZone(node.Position) && !nManagerSetting.IsBlackListed(node.Guid) || questObjective.IgnoreBlackList)))
	{
		if(nManager.Wow.Helpers.PathFinder.FindPath(node.IsValid ? node.Position : unit.Position).Count <= 0)
		{
			nManagerSetting.AddBlackList(node.IsValid ? node.Guid : unit.Guid, 30*1000);
			return false;
		}
		
		if (questObjective.IgnoreFight && unit.GetDistance <= 20) //Dont ignore fight if we are too far from the mob... 
			nManager.Wow.Helpers.Quest.GetSetIgnoreFight = true;
		/* Entry found, GoTo */
	
		//Pre Select Target
		if (node.IsValid && node.Position.DistanceTo(ObjectManager.Me.Position) <= 60 && ObjectManager.Target.Guid != node.Guid)
		{
			Interact.InteractWith(node.GetBaseAddress);
		}
		else if (unit.IsValid && unit.Position.DistanceTo(ObjectManager.Me.Position) <= 60 && ObjectManager.Target.Guid != unit.Guid)
		{	
			Lua.LuaDoString("ClearTarget()");
			
			if(questObjective.ExtraString == "InteractWith")
			{
				Interact.InteractWith(unit.GetBaseAddress);
			}
			else
			{
				ObjectManager.Me.Target = unit.Guid;
			}
		}
		
		if(ObjectManager.Me.UnitAura(108237).IsValid && unit.IsValid)
		{
			Lua.LuaDoString("ExtraActionButton1:Click()");
			Thread.Sleep(500);
			ClickOnTerrain.ClickOnly(unit.Position);
		}
		
		if (node.IsValid)
		{
			Logging.Write("bomb valid");
			unit = new WoWUnit(0);
			baseAddress = MovementManager.FindTarget(node, questObjective.Range);
		}
		else if (unit.IsValid)
		{
			node = new WoWGameObject(0);
			baseAddress = MovementManager.FindTarget(unit, questObjective.Range);
		}	
	
		if((node.IsValid && node.GetDistance < questObjective.Range) || (unit.IsValid && unit.GetDistance <= questObjective.Range))
		{
			/* Target Reached */
			MovementManager.StopMove();
			if(questObjective.ExtraString != "NoDismount")
			{
				MountTask.DismountMount();
			}	
		}
		else
		{
			if (MovementManager.InMovement)
				return false;
			if (questObjective.IgnoreNotSelectable)
			{
				if ((node.IsValid && node.GetDistance > questObjective.Range) || (unit.IsValid && unit.GetDistance > questObjective.Range))
					return false;
			}
			else
			{
				if (baseAddress <= 0)
					return false;
				if (baseAddress > 0 && ((node.IsValid && node.GetDistance > questObjective.Range) || (unit.IsValid && unit.GetDistance > questObjective.Range)))
					return false;
				
			}
		}
		
		/* Target Reached */
		MovementManager.StopMove();
		
		if(questObjective.ExtraString != "NoDismount")
		{
			MountTask.DismountMount();
		}	
		
		if (node.IsValid)
		{
			MovementManager.Face(node);
			Interact.InteractWith(node.GetBaseAddress); //Pickup Bomb
		}
		else if (unit.IsValid)
		{
			MovementManager.Face(unit);
		}
		
		Thread.Sleep(100 + Usefuls.Latency); /* ZZZzzzZZZzz */
		
		Thread.Sleep(Usefuls.Latency + 250);

		/* Wait for the Use Item cast to be finished, if any */
		while (ObjectManager.Me.IsCast)
		{
			Thread.Sleep(Usefuls.Latency);
		}

		if (node.IsValid)
		{
			nManagerSetting.AddBlackList(node.Guid, 60*1000);
		}
		else if (unit.IsValid)
		{
			Interact.InteractWith(unit.GetBaseAddress); //Interact With Unit to Attack it
			nManagerSetting.AddBlackList(unit.Guid, 60*1000);
		}

		/* Wait if necessary */
		if (questObjective.WaitMs > 0)
			Thread.Sleep(questObjective.WaitMs);

		nManager.Wow.Helpers.Quest.GetSetIgnoreFight = false;
		return true;
	}
		/* Move to Zone/Hotspot */
	else if (!MovementManager.InMovement)
	{
		nManager.Wow.Helpers.Quest.GetSetIgnoreFight = false;
		if (questObjective.PathHotspots[nManager.Helpful.Math.NearestPointOfListPoints(questObjective.PathHotspots, ObjectManager.Me.Position)].DistanceTo(ObjectManager.Me.Position) > 5)
		{
			if(nManager.Wow.Helpers.Quest.TravelToQuestZone(questObjective.PathHotspots[nManager.Helpful.Math.NearestPointOfListPoints(questObjective.PathHotspots, ObjectManager.Me.Position)],
				ref questObjective.TravelToQuestZone, questObjective.ContinentId, questObjective.ForceTravelToQuestZone))
				return false;
			MovementManager.Go(PathFinder.FindPath(questObjective.PathHotspots[nManager.Helpful.Math.NearestPointOfListPoints(questObjective.PathHotspots, ObjectManager.Me.Position)]));
		}
		else
		{
			MovementManager.GoLoop(questObjective.PathHotspots);
		}
	}
}
catch (Exception ex)
{
	Logging.Write(ex.Message);
}
finally
{
	/*nManager.Wow.Helpers.Quest.GetSetIgnoreFight = false;*/
}