using GTFO.API;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Networks;
public static class GlobalNetMasterAction<P> where P : struct
{
    public static SNet_ChannelType SendChannel { get; set; } = SNet_ChannelType.GameOrderCritical;
    public static SNet_Player LastSender { get; private set; }
    public static ulong LastSenderID { get; private set; }

    public static Func<ulong, P, bool> IsActionValid = (sender, p) => { return true; };
    public static event Action<ulong, P> OnReceive;
    public static event Action<ulong, P> OnMasterReceive;

    private static string _AskEventName;
    private static string _EventName;

    private static bool _IsSetup = false;

    public static void Setup()
    {
        _AskEventName = UName.Get(typeof(P), "NMA0");
        _EventName = UName.Get(typeof(P), "NMA1");
        NetworkAPI.RegisterEvent<P>(_AskEventName, ReceivedAsk);
        NetworkAPI.RegisterEvent<P>(_EventName, Received);

        _IsSetup = true;
    }

    public static void Ask(P payload)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        if (!SNet.HasMaster)
            return;

        if (SNet.IsMaster)
        {
            if (IsActionValid?.Invoke(SNet.LocalPlayer.Lookup, payload) ?? true)
            {
                NetworkAPI.InvokeEvent(_EventName, payload, SendChannel);
                OnReceive?.Invoke(SNet.LocalPlayer.Lookup, payload);
                OnMasterReceive?.Invoke(SNet.LocalPlayer.Lookup, payload);
            }
        }
        else
        {
            NetworkAPI.InvokeEvent(_AskEventName, payload, SNet.Master, SendChannel);
        }
    }

    private static void ReceivedAsk(ulong sender, P payload)
    {
        if (!_IsSetup)
        {
            Logger.Error("Action Wasn't Setup!");
            return;
        }

        if (!SNet.IsMaster)
        {
            Logger.Error("Non Master Received Ask Action!");
            return;
        }

        if (IsActionValid?.Invoke(sender, payload) ?? true)
        {
            NetworkAPI.InvokeEvent(_EventName, payload, SendChannel);
            OnReceive?.Invoke(SNet.LocalPlayer.Lookup, payload);
            OnMasterReceive?.Invoke(sender, payload);
        }
        return;
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
            Logger.Error($"NetMasterAction sender was invalid!");
            return;
        }

        LastSender = lastSender;
        LastSenderID = sender;

        if (!LastSender.IsMaster)
        {
            Logger.Error($"Sender was not a master");
            return;
        }   

        OnReceive?.Invoke(sender, payload);
        return;
    }
}
