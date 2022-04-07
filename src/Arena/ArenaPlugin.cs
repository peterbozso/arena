﻿using BepInEx;
using R2API.Utils;
using RoR2;

namespace Arena;

// This attribute specifies that we have a dependency on R2API, as we're using it to add our item to the game.
[BepInDependency(R2API.R2API.PluginGUID)]
// This attribute is required, and lists metadata for the plugin.
[BepInPlugin(PluginGUID, PluginName, PluginVersion)]

public class ArenaPlugin : BaseUnityPlugin
{
    public const string PluginGUID = PluginAuthor + "." + PluginName;
    public const string PluginAuthor = "peterbozso";
    public const string PluginName = "Arena";
    public const string PluginVersion = "0.0.1";

    // The Awake() method is run at the very start when the game is initialized.
    public void Awake()
    {
        Log.Init(Logger);

#if DEBUG
        EnableDebugMode();
#endif

        TeleporterInteraction.onTeleporterFinishGlobal += TeleporterInteraction_onTeleporterFinishGlobal;
    }

    private void EnableDebugMode()
    {
        // Make player invulnerable:
        On.RoR2.HealthComponent.TakeDamage += (orig, self, damageInfo) =>
        {
            var charComponent = self.GetComponent<CharacterBody>();
            if (charComponent != null && charComponent.isPlayerControlled)
            {
                return;
            }
            orig(self, damageInfo);
        };
    }

    private void TeleporterInteraction_onTeleporterFinishGlobal(TeleporterInteraction obj)
    {
        ChatMessage.Send("Good people of the Imperial City, welcome to the Arena!");
        // TODO: disable teleporter, stop clock, add Artifact of Chaos
    }

    // The Update() method is run on every frame of the game.
    private void Update()
    {
    }
}
