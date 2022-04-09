﻿using RoR2;
using System;

namespace Arena.Managers;

internal class DeathManager
{
    private readonly ChampionManager _championManager = new();
    private Action<string> _allPlayersDeadCallback;

    public bool IsSinglePlayer => _championManager.Name != string.Empty;

    public void Start(Action<string> allPlayersDeadCallback)
    {
        _allPlayersDeadCallback = allPlayersDeadCallback;
        On.RoR2.CharacterBody.OnDeathStart += CharacterBody_OnDeathStart;
    }

    private void Stop()
    {
        On.RoR2.CharacterBody.OnDeathStart -= CharacterBody_OnDeathStart;
        _allPlayersDeadCallback(_championManager.Name);
    }

    private void CharacterBody_OnDeathStart(On.RoR2.CharacterBody.orig_OnDeathStart orig, CharacterBody self)
    {
        if (!self.isPlayerControlled)
        {
            orig(self);
            return;
        }

        var championName = _championManager.Name;

        Log.LogMessage(championName == string.Empty
            ? "There are still multiple fighters alive."
            : "Only the Champion is alive: " + championName);

        if (championName != string.Empty)
        {
            Stop();
        }

        orig(self);
    }
}
