using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Events;
public static class StartGameEvent
{
    public static event Action OnGameLoaded;

    internal static void Invoke_OnGameLoaded()
    {
        OnGameLoaded?.Invoke();
    }
}
