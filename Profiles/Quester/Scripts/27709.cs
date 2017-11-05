
Logging.Write("27709 logging - turning pawns in the sentinels room");

if(nManager.Wow.Helpers.Quest.GetQuestCompleted(27709) || nManager.Wow.Helpers.Quest.GetLogQuestIsComplete(27709))
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
        Name = "Sentinels Pawns event",
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



nManager.Wow.Class.Point[]  points_path = new nManager.Wow.Class.Point[]
{
    new Point( (float)-6902.38, (float)-3378.21, (float)202.2832 ), //  (0) 1  
    new Point( (float)-6897.0,  (float)-3392.07, (float)202.2843 ), //  (1) 2  
    new Point( (float)-6911.45, (float)-3404.39, (float)202.2843 ), //  (2) 2 
    new Point( (float)-6922.67, (float)-3387.31, (float)202.2843 ), //  (3) 1  
    new Point( (float)-6933.82, (float)-3398.87, (float)202.2844 ), //  (4) 2  
    new Point( (float)-6940.76, (float)-3378.07, (float)202.284  )  //  (5) 2 
};

uint[] points_interacts = new uint[] {1,2,2,1,2,2};
int[] points_travel   = new int[] {3000,4000,4000,4500,4000,4500};

System.Collections.Generic.List<WoWUnit> unitlist = ObjectManager.GetWoWUnitByEntry(46395, false);
WoWUnit SentinelPawn;

for (int i = 0; i<=5; i++)
{
    MovementManager.MoveTo( points_path[i] ,false);
    Thread.Sleep(points_travel[i]+ Usefuls.Latency);
    SentinelPawn = ObjectManager.GetNearestWoWUnit(unitlist, true, true, true);
    Thread.Sleep(100 + Usefuls.Latency);
    MovementManager.Face(SentinelPawn);
    Thread.Sleep(100 + Usefuls.Latency);
    for (int t =1; t<= points_interacts[i];t++)
    {
        Interact.InteractWith(SentinelPawn.GetBaseAddress);	
        Thread.Sleep(1200 + Usefuls.Latency);
    }
     
}

Thread.Sleep(2000 + Usefuls.Latency );
questObjective.IsObjectiveCompleted = true;


while (!nManager.Wow.Helpers.Quest.GetQuestCompleted(27709))
{
Thread.Sleep(1000 + Usefuls.Latency );

}


return true;
