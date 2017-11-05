

if(nManager.Wow.Helpers.Quest.GetQuestCompleted(27693) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(27693))
{
    return true;
}


if (nManager.Wow.ObjectManager.ObjectManager.Me.Position.DistanceTo(questObjective.Position) > questObjective.Range)
{
    Npc questEventPos = new Npc();
    questEventPos = new Npc
    {
        Entry = 0,
        Position = questObjective.Position,
        Name = "Wardens Pawns event",
        ContinentIdInt = Usefuls.ContinentId,
        Faction = nManager.Wow.ObjectManager.ObjectManager.Me.PlayerFaction.ToLower() == "horde" ? Npc.FactionType.Horde : Npc.FactionType.Alliance,
    };

    // going to move to quest event position
    while (nManager.Wow.ObjectManager.ObjectManager.Me.Position.DistanceTo(questObjective.Position) >= questObjective.Range)
    {
        if (nManager.Wow.ObjectManager.ObjectManager.Me.InCombat)
        {
            Logging.Write("Eeek combat detected - returning control for now...");
            return false;
        }

        MovementManager.FindTarget(ref questEventPos, questObjective.Range);
        Thread.Sleep(500);
    }

}


System.Collections.Generic.List<WoWUnit> unitlist = ObjectManager.GetWoWUnitByEntry(46344, false);
if (unitlist.Count < 8)
{
    Logging.Write("should be 8 pawns, but there isn't."+ unitlist.Count);
    Thread.Sleep(1200 + Usefuls.Latency);

    Logging.Write("Abandon and Pickup quest again");
    nManager.Wow.Helpers.Quest.AbandonQuest(27693);
    Thread.Sleep(2000 + Usefuls.Latency);

    Point npcp = new Point((float)-6964.62,(float)-3445.19,(float)200.632);
    Npc qnpc = new Npc();
    qnpc = new Npc
    {
        Entry = 206335,
        Position = npcp,
        Name = "Stone Slab",
        ContinentIdInt = Usefuls.ContinentId,
        Faction = nManager.Wow.ObjectManager.ObjectManager.Me.PlayerFaction.ToLower() == "horde" ? Npc.FactionType.Horde : Npc.FactionType.Alliance,
    };
    
    nManager.Wow.Helpers.Quest.QuestPickUp( ref qnpc ,"The Warden's Game", 27693, false, false);
    Thread.Sleep(3000);

    unitlist = ObjectManager.GetWoWUnitByEntry(46344, false);
    if (unitlist.Count == 8)
    {
        Logging.Write("now have 8 pawns, can get on with quest.");
    }
    else
    {
        Logging.Write("Problems in quest ");
        nManager.Wow.Helpers.Quest.AbandonQuest(27693);
        return false;
    }
}


WoWUnit WardenPawn1 = unitlist[0];
WoWUnit WardenPawn4 = unitlist[1];
WoWUnit WardenPawn7 = unitlist[2];
WoWUnit WardenPawn8 = unitlist[3];
WoWUnit WardenPawn9 = unitlist[4];
WoWUnit WardenPawn6 = unitlist[5];
WoWUnit WardenPawn3 = unitlist[6];
WoWUnit WardenPawn2 = unitlist[7];


MovementManager.FindTarget(WardenPawn8, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn8);
Interact.InteractWith(WardenPawn8.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn2, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn2);
Interact.InteractWith(WardenPawn2.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn8, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn8);
Interact.InteractWith(WardenPawn8.GetBaseAddress);	


MovementManager.FindTarget(WardenPawn4, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn4);
Interact.InteractWith(WardenPawn4.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn6, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn6);
Interact.InteractWith(WardenPawn6.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn4, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn4);
Interact.InteractWith(WardenPawn4.GetBaseAddress);	


MovementManager.FindTarget(WardenPawn1, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn1);
Interact.InteractWith(WardenPawn1.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn9, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn9);
Interact.InteractWith(WardenPawn9.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn1, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn1);
Interact.InteractWith(WardenPawn1.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn7, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn7);
Interact.InteractWith(WardenPawn7.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn3, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn3);
Interact.InteractWith(WardenPawn3.GetBaseAddress);	

MovementManager.FindTarget(WardenPawn7, 2);
Thread.Sleep(1200 + Usefuls.Latency);
MovementManager.Face(WardenPawn7);
Interact.InteractWith(WardenPawn7.GetBaseAddress);	



Thread.Sleep(2000 + Usefuls.Latency );
questObjective.IsObjectiveCompleted = true;

while (!nManager.Wow.Helpers.Quest.GetQuestCompleted(27693))
{
Thread.Sleep(1000 + Usefuls.Latency );

}


return true;


