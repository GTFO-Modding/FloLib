using AssetShards;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Events.Inject;
[HarmonyPatch(typeof(AssetShardManager), nameof(AssetShardManager.LoadAllShardsForBundleAsync))]
internal static class Inject_AssetShardManager
{
    //necessary evil: This ensures the timing of assetLoaded callback always after base-game callback has finished.
    internal static void Prefix(AssetBundleName name, ref Il2CppSystem.Action callback)
    {
        switch (name)
        {
            case AssetBundleName.Startup:
                callback += (Action)AssetEvent.Invoke_StartupAssetLoaded;
                break;

            case AssetBundleName.Enemies:
                callback += (Action)AssetEvent.Invoke_EnemyAssetLoaded;
                break;

            case AssetBundleName.Complex_Shared:
                callback += (Action)AssetEvent.Invoke_SharedAssetLoaded;
                break;
        }
    }
}
