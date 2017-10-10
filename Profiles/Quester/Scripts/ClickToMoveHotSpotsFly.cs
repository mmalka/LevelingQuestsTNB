if (questObjective.Hotspots[questObjective.Hotspots.Count - 1].DistanceTo(ObjectManager.Me.Position) > 5f)
	// if qObj.Position is valid => use it as reference, else, last point in list.
{
	if (questObjective.IgnoreFight) //Dont ignore fight if we are too far from the mob...
		nManager.Wow.Helpers.Quest.GetSetIgnoreFight = true;

	if (!Usefuls.IsFlying && !MountTask.OnFlyMount())
	{
		Logging.Write("Not flying and not on fly mount.");
		MountTask.Mount(true, true, true);
		Thread.Sleep(200);
	}

	if (!Usefuls.IsFlying)
	{
		Logging.Write("Not flying.");
		MountTask.Takeoff();
		Thread.Sleep(200);
	}

	if (MovementManager.PointId >= questObjective.Hotspots.Count-1)
	{
		MovementManager.MoveTo(questObjective.Hotspots[questObjective.Hotspots.Count - 1]);
		return false;
	}
	MovementManager.GoLoop(questObjective.Hotspots);
	return false;
}
questObjective.IsObjectiveCompleted = true;
Logging.Write("Position Reached");
MovementManager.StopMove();
return true;