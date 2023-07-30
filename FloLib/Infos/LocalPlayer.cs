using FloLib.Utils;
using Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Infos;
public static class LocalPlayer
{
    public static bool HasAgent { get; private set; }
    public static PlayerAgent Agent { get; private set; }
    public static LocalPlayerAgent LocalAgent { get; private set; }

    private static int? _InstanceID;

    public static bool TryGetAgent(out PlayerAgent agent)
    {
        agent = Agent;
        return HasAgent;
    }

    public static bool TryGetLocalAgent(out LocalPlayerAgent agent)
    {
        agent = LocalAgent;
        return HasAgent;
    }

    public static Vector3 GetPosition()
    {
        if (HasAgent) return Agent.Position;
        else return Vector3.zero;
    }

    public static Vector3 GetPosition(Vector3 fallback)
    {
        if (HasAgent) return Agent.Position;
        else return fallback;
    }

    public static Vector3 GetEyePosition()
    {
        if (HasAgent) return Agent.EyePosition;
        else return Vector3.zero;
    }

    public static Vector3 GetEyePosition(Vector3 fallback)
    {
        if (HasAgent) return Agent.EyePosition;
        else return fallback;
    }


    [AutoInvoke(InvokeWhen.StartupAssetLoaded)]
    internal static void Init()
    {
        Coroutines.Start(UpdatePlayerInfo(), CoroutineLifeTime.Forever);
    }

    private static IEnumerator UpdatePlayerInfo()
    {
        while (true)
        {
            if (PlayerManager.HasLocalPlayerAgent())
            {
                var agent = PlayerManager.GetLocalPlayerAgent();
                var instanceID = agent.GetInstanceID();
                if (!_InstanceID.HasValue || _InstanceID.Value != instanceID)
                {
                    Agent = agent;
                    LocalAgent = agent.Cast<LocalPlayerAgent>();
                    _InstanceID = agent.GetInstanceID();
                    HasAgent = true;
                }
            }
            else
            {
                Agent = null;
                LocalAgent = null;
                _InstanceID = null;
                HasAgent = false;
            }

            yield return null;
        }
    }
}
