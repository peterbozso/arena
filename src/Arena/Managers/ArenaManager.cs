﻿using R2API.Utils;
using RoR2;

namespace Arena.Managers;

internal class ArenaManager : ManagerBase
{
    private bool _isEventInProgress;

    public void Start()
    {
        TeleporterInteraction.onTeleporterChargedGlobal += OnTeleporterCharged;
        On.RoR2.Run.AdvanceStage += OnAdvanceStage;

        Log.LogDebug($"{nameof(ArenaManager)} started.");
    }

    public override void Destroy()
    {
        TeleporterInteraction.onTeleporterChargedGlobal -= OnTeleporterCharged;
        On.RoR2.Run.AdvanceStage -= OnAdvanceStage;

        Log.LogDebug($"{nameof(ArenaManager)} stopped.");
    }

    private void OnTeleporterCharged(TeleporterInteraction tpi)
    {
        if (Store.Get<DeathManager>().IsSinglePlayer)
        {
            Log.LogDebug("Only one player is alive. Not starting the event.");
            return;
        }

        ChatMessage.Send("Good people of the Imperial City, welcome to the Arena!");

        Store.Get<ClockManager>().PauseClock();
        Store.Get<FriendlyFireManager>().EnableFriendlyFire();
        Store.Get<PortalManager>().DisableAllPortals();
        Store.Get<DeathManager>().Start(OnAllPlayersDead);

        _isEventInProgress = true;

        Log.LogDebug("Arena event started.");
    }

    private void OnAllPlayersDead(string championName)
    {
        Store.Get<PortalManager>().EnableAllPortals();

        ChatMessage.Send($"Good people, we have a winner! All hail the combatant, {championName}! Champion, leave the Arena now and rest! You've earned it!");
    }

    private void OnAdvanceStage(On.RoR2.Run.orig_AdvanceStage orig, Run self, SceneDef nextScene)
    {
        if (_isEventInProgress)
        {
            Store.Get<ClockManager>().ResumeClock();
            Store.Get<FriendlyFireManager>().DisableFriendlyFire();

            _isEventInProgress = false;

            Log.LogDebug("Arena event ended.");
        }

        orig(self, nextScene);
    }
}
