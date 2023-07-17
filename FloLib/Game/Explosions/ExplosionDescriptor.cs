using FloLib.Networks.PayloadStructs;
using FloLib.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Game.Explosions;
public struct ExplosionDescriptor
{
    public Vector3 Position;
    public PL_Agent Inflictor;
    public float MaxDamage;
    public float MinDamage;
    public Half DamageMultiplierToInflictor;
    public Half DamageMultiplierToEnemy;
    public Half DamageMultiplierToPlayer;
    public float MaxDamageRange;
    public float MinDamageRange;
    public HalfRGBColor LightColor;
    public float NoiseRange_InstaWake;
    public float NoiseRange_Detectable;
    public NM_NoiseType NoiseType;
}
