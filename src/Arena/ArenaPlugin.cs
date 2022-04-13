﻿using Arena.Logging;
using Arena.Managers;
using BepInEx;
using RoR2;
using UnityEngine;

namespace Arena;

[BepInDependency(R2API.R2API.PluginGUID)]
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class ArenaPlugin : BaseUnityPlugin
{
    public const string PluginGUID = PluginAuthor + "." + PluginName;
    public const string PluginAuthor = "peterbozso";
    public const string PluginName = "Arena";
    public const string PluginVersion = "0.1.1";

    private readonly StatusLogger _statusLogger = new();

    public void Awake()
    {
        Log.Init(Logger);

        On.RoR2.Run.Awake += OnRunAwake;
        On.RoR2.Run.OnDestroy += OnRunOnDestroy;

        Log.Info("Plugin hooked.");
    }

    public void OnDestroy()
    {
        On.RoR2.Run.Awake -= OnRunAwake;
        On.RoR2.Run.OnDestroy -= OnRunOnDestroy;

        Log.Info("Plugin unhooked.");
    }

    private void OnRunAwake(On.RoR2.Run.orig_Awake orig, Run self)
    {
        orig(self);

        Store.Instance.Get<ArenaManager>().WatchStageEvents();

        Log.Info("Run started.");
    }

    private void OnRunOnDestroy(On.RoR2.Run.orig_OnDestroy orig, Run self)
    {
        orig(self);

        Store.Instance.CleanUp();

        Log.Info("Run ended.");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F2))
        {
            _statusLogger.LogStatus();
        }
    }
}
