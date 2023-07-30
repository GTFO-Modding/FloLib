using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Events;
public static class StartGameEvent
{
    static bool _GameLoadedInvoked = false;
    static event Action _OnGameLoaded;

    public static event Action OnGameLoaded
    {
        add
        {
            if (_GameLoadedInvoked)
            {
                Logger.Warn($"{nameof(StartGameEvent)}.{nameof(OnGameLoaded)} has already invoked before, therefore this will not be called");
                Logger.Warn($"from: {new StackTrace()}");
            }
            _OnGameLoaded += value;
        }
        remove
        {
            _OnGameLoaded -= value;
        }
    }

    internal static void Invoke_OnGameLoaded()
    {
        _OnGameLoaded?.Invoke();
        _GameLoadedInvoked = true;
    }
}
