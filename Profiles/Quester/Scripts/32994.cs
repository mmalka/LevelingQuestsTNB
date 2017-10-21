/* Check if there is HotSpots in the objective */
if (questObjective.Hotspots.Count <= 0)
{
	/* Objective CSharpScript with script InteractWithHotSpots requires valid "Hotspots" */
	Logging.Write("InteractWithHotSpots requires valid 'HotSpots'.");
	questObjective.IsObjectiveCompleted = true;
	return false;
}



if(questObjective.Range == 0)
	questObjective.Range = 5;
	
/* Search for Entry */
WoWGameObject node = ObjectManager.GetNearestWoWGameObject(ObjectManager.GetWoWGameObjectById(questObjective.Entry));
WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList, questObjective.AllowPlayerControlled);
Point pos = ObjectManager.Me.Position; /* Initialize or getting an error */;
int q = QuestID; /* not used but otherwise getting warning QuestID not used */
uint baseAddress = 0;

/* If Entry found continue, otherwise continue checking around HotSpots */
if (node.IsValid && (!nManagerSetting.IsBlackListedZone(node.Position) && !nManagerSetting.IsBlackListed(node.Guid) || questObjective.IgnoreBlackList))
{
	
	
	int dynFlag = node.GetDynamicFlags;
	uint dynFlags = BitConverter.ToUInt32(BitConverter.GetBytes(dynFlag),0);

	if (!((dynFlags & 0x4) != 0))
	{
		nManagerSetting.AddBlackList(node.Guid, 30*1000);
		return false;
	}
	
	

	if (questObjective.IgnoreFight && unit.GetDistance <= 20) //Dont ignore fight if we are too far from the mob... 
			nManager.Wow.Helpers.Quest.GetSetIgnoreFight = true;
	
	/* Entry found, GoTo */
	
	Point p0 = new Point(node.Position);
	float angle = node.Orientation + (float)(System.Math.PI ) ;Point p = nManager.Helpful.Math.GetPosition2DOfAngleAndDistance(p0, angle, 3.75f);
	p.Z = PathFinder.GetZPosition(p, true);

	MovementManager.GoToLocationFindTarget(p, 3.75f, false);
	
	Thread.Sleep(500);

	if(p.DistanceTo(ObjectManager.Me.Position) < questObjective.Range)
	{
		/* Target Reached */
		MovementManager.StopMove();
		MountTask.DismountMount();
	}
	else
	{
		if (MovementManager.InMovement)
			return false;
		if(baseAddress <= 0)
			return false;
		if (baseAddress > 0 && (node.IsValid && node.GetDistance > questObjective.Range))
			return false;
	}
  
	Thread.Sleep(100 + Usefuls.Latency); /* ZZZzzzZZZzz */

	/* Entry reached, dismount */
	MovementManager.StopMove();
	MountTask.DismountMount();
	
	/* Interact With Entry */
	
	MovementManager.Face(node);
	Interact.InteractWith(node.GetBaseAddress);
	nManagerSetting.AddBlackList(node.Guid, 30*1000);
		
	Thread.Sleep(Usefuls.Latency); 

	/* Wait for the interact cast to be finished, if any */
	while (ObjectManager.Me.IsCast)
	{
		Thread.Sleep(Usefuls.Latency);
	}

	

	if (ObjectManager.Me.InCombat && !questObjective.IgnoreFight)
		return false;
	
	/* Wait if necessary */
	if(questObjective.WaitMs > 0)
		Thread.Sleep(questObjective.WaitMs);

	
}else if (!MovementManager.InMovement)
{
	/* Move to Zone/Hotspot */
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

nManager.Wow.Helpers.Quest.GetSetIgnoreFight = false;