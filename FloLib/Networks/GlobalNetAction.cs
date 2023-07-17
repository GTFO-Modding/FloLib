using GTFO.API;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Networks;
public static class GlobalNetAction<P> where P : struct
{
    public static SNet_ChannelType SendChannel { get; set; } = SNet_ChannelType.GameOrderCritical;
    public static SNet_Player LastSender { get; private set; }
    public static ulong LastSenderID { get; private set; }

    public static event Action<ulong, P> OnReceive;
    public static event Action<ulong, P> OnReceiveLocally;

    private static string _EventName;
    private static bool _IsSetup = false;

    public static void Setup()
    {
        _EventName = UName.Get(typeof(P), "NA");
        NetworkAPI.RegisterEvent<P>(_EventName, Received);

        _IsSetup = true;
    }

    public static void Send(P payload)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        SendToLocal(payload);
        NetworkAPI.InvokeEvent(_EventName, payload, SendChannel);
    }

    public static void SendTo(P payload, SNet_Player target)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        if (target.IsLocal)
        {
            SendToLocal(payload);
            return;
        }

        NetworkAPI.InvokeEvent(_EventName, payload, SendChannel);
    }

    public static void SendTo(P payload, SNet_SendGroup group)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        var targets = SNet_PlayerSendGroupManager.GetGroup(group);
        foreach (var target in targets)
        {
            SendTo(payload, target);
        }
    }

    private static void SendToLocal(P payload)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        var localPlayer = SNet.LocalPlayer;
        LastSender = localPlayer;
        if (localPlayer != null)
        {
            LastSenderID = localPlayer.Lookup;
        }
        
        OnReceive?.Invoke(LastSenderID, payload);
        OnReceiveLocally?.Invoke(LastSenderID, payload);
    }

    private static void Received(ulong sender, P payload)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        if (!SNet.Core.TryGetPlayer(sender, out var lastSender, forceGet: false))
        {
            LastSender = null;
            LastSenderID = 0;
            Logger.Error($"NetAction sender was invalid!");
        }

        LastSender = lastSender;
        LastSenderID = sender;

        OnReceive?.Invoke(sender, payload);
        return;
    }
}
