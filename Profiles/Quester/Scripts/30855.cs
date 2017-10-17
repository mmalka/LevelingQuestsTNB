		if (questObjective.Hotspots.Count <= 0)
		{
			/* Objective CSharpScript with script UseItemWithHotSpots requires valid "Hotspots" */
			Logging.Write("UseItemWithHotSpots requires valid 'HotSpots'.");
			questObjective.IsObjectiveCompleted = true;
			return false;
		}

		if(questObjective.Range == 0)
			questObjective.Range = 5;
		
		int _shaiHuId = 61069;
		uint _shaiHuAuraShieldId = 118633;
		int _explosiveHatredId = 61070;
		
		WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(_shaiHuId, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
			questObjective.AllowPlayerControlled);
		Point pos = ObjectManager.Me.Position; /* Initialize or getting an error */
		uint baseAddress = 0;
		
		if(unit.UnitAura(_shaiHuAuraShieldId).IsValid)
		{
			GetExplovieHatred(questObjective);
		}
		
		/* If Entry found continue, otherwise continue checking around HotSpots */
		if (unit.IsValid && !unit.UnitAura(_shaiHuAuraShieldId).IsValid)
		{

			System.Threading.Thread _worker2;
		
			//Pre Select Target
			if (unit.IsValid && unit.Position.DistanceTo(ObjectManager.Me.Position) <= 60 && ObjectManager.Target.Guid != unit.Guid)
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
			
			
			baseAddress = MovementManager.FindTarget(unit, questObjective.Range);
			
		
			if(unit.IsValid && unit.GetDistance <= questObjective.Range)
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
					if (unit.IsValid && unit.GetDistance > questObjective.Range)
						return false;
				}
				else
				{
					if (baseAddress <= 0)
						return false;
					if (baseAddress > 0 && (unit.IsValid && unit.GetDistance > questObjective.Range))
						return false;
					
				}
			}
			
			/* Target Reached */
			MovementManager.StopMove();
			MountTask.DismountMount();
			
			
			
			_worker2 = new System.Threading.Thread(() => nManager.Wow.Helpers.Fight.StartFight(unit.Guid));
			Thread.Sleep(500);

			_worker2.Start();
		
			while (!unit.UnitAura(_shaiHuAuraShieldId).IsValid && !unit.IsDead)
			{
				if(!ObjectManager.Me.InCombat || ObjectManager.Me.IsDeadMe)
				{
					Fight.StopFight();
					_worker2 = null;
					return false;
				}
				Thread.Sleep(50);
			}
		
			Fight.StopFight();
			_worker2 = null;
			Thread.Sleep(100 + Usefuls.Latency); /* ZZZzzzZZZzz */

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
	} //Close the try here
	catch (Exception e)
	{
		Logging.WriteError("Script: " + e);
	}
	return false;
} 

const int _shaiHuId = 61069;
const int _shaiHuAuraShieldId = 118633;
const int _explosiveHatredId = 61070;


		
private void GetExplovieHatred(QuestObjective questObjective)
{	
	WoWUnit explosiveHatred = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(_explosiveHatredId, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
	questObjective.AllowPlayerControlled);
	
	uint baseAddress = 0;
	
	if(explosiveHatred.IsValid && !IsAggro(explosiveHatred.GetBaseAddress))
	{
		
		baseAddress = MovementManager.FindTarget(explosiveHatred, questObjective.Range);
		
		if(explosiveHatred.IsValid && explosiveHatred.GetDistance <= questObjective.Range )
		{
			/* Target Reached */
			MovementManager.StopMove();
		}
		else
		{
			if (MovementManager.InMovement)
				return;
			if (baseAddress <= 0)
				return;
			if (baseAddress > 0 && (explosiveHatred.IsValid && explosiveHatred.GetDistance > questObjective.Range))
				return;
		}
	}
	else
	{
		WoWUnit shaiHu = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(_shaiHuId, questObjective.IsDead), questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList,
	questObjective.AllowPlayerControlled);
		//Get back to Shai Hu
		
		baseAddress = MovementManager.FindTarget(shaiHu, questObjective.Range);
		
		if(shaiHu.IsValid && shaiHu.GetDistance <= questObjective.Range )
		{
			/* Target Reached */
			MovementManager.StopMove();

		}
		else
		{
			if (MovementManager.InMovement)
				return;
			if (baseAddress <= 0)
				return;
			if (baseAddress > 0 && (shaiHu.IsValid && shaiHu.GetDistance > questObjective.Range))
				return;
		}
		Fight.StartFight(explosiveHatred.Guid);
	}
	return;		
	
}

private bool IsAggro(uint baseAddress)
{
	WoWUnit Aggro =  ObjectManager.GetHostileUnitAttackingPlayer().Find(x => x.GetBaseAddress == baseAddress);
	return (Aggro != null && Aggro.IsValid);
}

 public static bool random() //not used, just to close the script
        {
	        try //Bim Try open random !!!
	        {