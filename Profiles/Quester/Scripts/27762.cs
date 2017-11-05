
/* moving from quest npc to the rocket, and interact to mount it and fly to Badlands.
   Normally, would think an interact would do it.
   
   No, not this time.
   It pops up a dialogue frame that asks if sure,  yes or no.
   and so need to 'click' that.
   
   Plus, rogue needs to be out of stealth too.
 */


if ( nManager.Wow.Helpers.Usefuls.MapZoneName == "Badlands" )
{
    return true;
}


WoWUnit unit = ObjectManager.GetNearestWoWUnit(ObjectManager.GetWoWUnitByEntry(questObjective.Entry, questObjective.IsDead),
                    questObjective.IgnoreNotSelectable, questObjective.IgnoreBlackList, questObjective.AllowPlayerControlled);

if (unit.IsValid)
{
    while (nManager.Wow.ObjectManager.ObjectManager.Me.Position.DistanceTo(unit.Position) >= questObjective.Range)
    {
        if (nManager.Wow.ObjectManager.ObjectManager.Me.InCombat)
        {
            Logging.Write("Eeek combat detected - returning control for now...");
            return false;
        }
        MovementManager.FindTarget(unit, questObjective.Range);
        Thread.Sleep(500);
    }

    Thread.Sleep(200 + Usefuls.Latency);
    MovementManager.Face(unit);

    
    if ( ObjectManager.Me.WowClass == WoWClass.Rogue )
    {
        // after quest pickup, the comabt code may have loaded stealth
        // give it a moment to settle down - then react.
        Thread.Sleep(1000 + Usefuls.Latency);
        if ( ObjectManager.Me.HaveBuff(1784) )
        {
            while( nManager.Wow.Helpers.SpellManager.IsSpellOnCooldown(1784) )
            {
                Thread.Sleep(200 + Usefuls.Latency);
            }
            nManager.Wow.Helpers.SpellManager.CastSpellByIdLUA(1784);
            Thread.Sleep(500 + Usefuls.Latency);
        }
    }
    
    if ( ObjectManager.Me.WowClass == WoWClass.Druid)
    {
        // after quest pickup, the comabt code may have loaded prowl
        // give it a moment to settle down - then react.
        Thread.Sleep(1000 + Usefuls.Latency);
        if ( ObjectManager.Me.HaveBuff(5215) )
        {
            while( nManager.Wow.Helpers.SpellManager.IsSpellOnCooldown(5215) )
            {
                Thread.Sleep(200 + Usefuls.Latency);
            }
            nManager.Wow.Helpers.SpellManager.CastSpellByIdLUA(5215);
            Thread.Sleep(500 + Usefuls.Latency);
        }
        
        // dumping forms as well
        // bear?
        if ( ObjectManager.Me.HaveBuff(5487) )
        {
            while( nManager.Wow.Helpers.SpellManager.IsSpellOnCooldown(5487) )
            {
                Thread.Sleep(200 + Usefuls.Latency);
            }
            nManager.Wow.Helpers.SpellManager.CastSpellByIdLUA(5487);
            Thread.Sleep(500 + Usefuls.Latency);
        }

        // travel?
        if ( ObjectManager.Me.HaveBuff(783) )
        {
            while( nManager.Wow.Helpers.SpellManager.IsSpellOnCooldown(783) )
            {
                Thread.Sleep(200 + Usefuls.Latency);
            }
            nManager.Wow.Helpers.SpellManager.CastSpellByIdLUA(783);
            Thread.Sleep(500 + Usefuls.Latency);
        }

        // cat?
        if ( ObjectManager.Me.HaveBuff(768) )
        {
            while( nManager.Wow.Helpers.SpellManager.IsSpellOnCooldown(768) )
            {
                Thread.Sleep(200 + Usefuls.Latency);
            }
            nManager.Wow.Helpers.SpellManager.CastSpellByIdLUA(768);
            Thread.Sleep(500 + Usefuls.Latency);
        }
    }
        
    
    
    Interact.InteractWith(unit.GetBaseAddress);	
		Thread.Sleep(1000 + Usefuls.Latency);

    if (Others.IsFrameVisible("StaticPopup1Button1"))
    {
        Lua.RunMacroText("/click StaticPopup1Button1");
    }
    else
    {
        return false;
    }
    	
    /* flight to Badlands */
    if(questObjective.WaitMs > 0)
      Thread.Sleep(questObjective.WaitMs);


    return true;
}

return false;





