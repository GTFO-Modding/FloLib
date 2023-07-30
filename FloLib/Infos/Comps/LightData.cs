using Il2CppInterop.Runtime.InteropTypes.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Infos.Comps;
[AutoInject]
internal sealed class LightData : MonoBehaviour
{
    public Il2CppValueField<float> PrefabIntensity;
    public LightMode SpawnedMode;
    public float SpawnedIntensity;
    public Color SpawnedColor;

    public LG_LightInfo.Data CreateData()
    {
        return new()
        {
            PrefabIntensity = PrefabIntensity,
            SpawnedMode = SpawnedMode,
            SpawnedIntensity = SpawnedIntensity,
            SpawnedColor = SpawnedColor
        };
    }
}
