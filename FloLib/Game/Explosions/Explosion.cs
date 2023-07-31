using AK;
using FloLib.Networks;
using GTFO.API;
using Player;
using SNetwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Game.Explosions;
public sealed class Explosion
{
    public static readonly Color DefaultFlashColor = new(1, 0.2f, 0, 1);

    [AutoInvoke(InvokeWhen.StartupAssetLoaded)]
    internal static void Init()
    {
        ExplosionEffectPooling.Initialize();
        GlobalNetAction<ExplosionDescriptor>.OnReceive += OnReceive;
    }

    private static void OnReceive(ulong sender, ExplosionDescriptor data)
    {
        Internal_TriggerExplosion(data);
    }

    public static void Trigger(ExplosionDescriptor data)
    {
        GlobalNetAction<ExplosionDescriptor>.Send(data);
    }

    internal static void Internal_TriggerExplosion(ExplosionDescriptor data)
    {
        CellSound.Post(EVENTS.STICKYMINEEXPLODE, data.Position);

        LightFlash(data.Position, data.MaxDamageRange, data.LightColor);

        if (!SNet.IsMaster)
            return;

        var targets = Physics.OverlapSphere(data.Position, data.MaxDamageRange, LayerManager.MASK_EXPLOSION_TARGETS);
        if (targets.Count < 1)
            return;

        DamageUtil.IncrementSearchID();
        var searchID = DamageUtil.SearchID;
        foreach (var target in targets)
        {
            ProcessExplosion(data, target, searchID);
        }
    }

    private static void ProcessExplosion(ExplosionDescriptor data, Collider target, uint searchID)
    {
        if (target == null)
            return;

        if (target.gameObject == null)
            return;

        if (!target.gameObject.TryGetComp<IDamageable>(out var targetDamagable))
            return;

        targetDamagable = targetDamagable.GetBaseDamagable();
        if (targetDamagable == null)
            return;

        if (targetDamagable.TempSearchID == searchID)
            return;

        var targetPosition = GetDamagablePosition(targetDamagable);
        targetDamagable.TempSearchID = searchID;

        var distance = Vector3.Distance(data.Position, targetPosition);
        if (IsExplosionBlocked(data.Position, targetPosition, target))
        {
            return;
        }

        var newDamage = CalcBaseRangeDamage(data.MaxDamage, distance, data.MaxDamageRange, data.MinDamageRange);
        var enemyBase = targetDamagable.TryCast<Dam_EnemyDamageBase>();
        if (enemyBase != null)
        {
            newDamage *= (float)data.DamageMultiplierToEnemy;
        }
        else
        {
            var playerBase = targetDamagable.TryCast<Dam_PlayerDamageBase>();
            if (playerBase != null)
            {
                newDamage *= (float)data.DamageMultiplierToPlayer;
            }
        }

        var agent = targetDamagable.GetBaseAgent();
        if (agent != null && agent.GlobalID == data.Inflictor.AgentID)
        {
            newDamage *= (float)data.DamageMultiplierToInflictor;
        }

        if (Mathf.Abs(newDamage) > float.Epsilon)
        {
            targetDamagable.ExplosionDamage(newDamage, data.Position, Vector3.up * 1000);
        }
    }

    private static Vector3 GetDamagablePosition(IDamageable damagable)
    {
        var baseAgent = damagable.GetBaseAgent();
        if (baseAgent != null)
        {
            return baseAgent.EyePosition;
        }
        else
        {
            return damagable.DamageTargetPos;
        }
    }

    private static bool IsExplosionBlocked(Vector3 pos1, Vector3 pos2, Collider targetCollider)
    {
        if (Physics.Linecast(pos1, pos2, out RaycastHit hit, LayerManager.MASK_EXPLOSION_BLOCKERS))
        {
            if (hit.collider == null)
                return false;

            if (hit.collider.gameObject == null)
                return false;

            if (hit.collider.gameObject.GetInstanceID() != targetCollider.gameObject.GetInstanceID())
                return false;
        }

        return true;
    }

    private static float CalcBaseRangeDamage(float damage, float distance, float minRange, float maxRange)
    {
        var newDamage = 0.0f;
        if (distance <= minRange)
        {
            newDamage = damage;
        }
        else if (distance <= maxRange)
        {
            newDamage = Mathf.Lerp(damage, 0.0f, (distance - minRange) / (maxRange - minRange));
        }
        return newDamage;
    }

    public static void LightFlash(Vector3 pos, float range, Color lightColor)
    {
        ExplosionEffectPooling.TryDoEffect(new ExplosionEffectData()
        {
            Position = pos,
            Color = lightColor,
            Intensity = 5.0f,
            Range = range,
            Duration = 0.05f
        });

        if (PlayerManager.HasLocalPlayerAgent())
        {
            var localAgent = PlayerManager.GetLocalPlayerAgent();
            var dist = (localAgent.Position - pos).magnitude;
            var amp = 6.0f * Mathf.Max(0.0f, Mathf.InverseLerp(range, 0, dist));
            if (amp > 0.01f)
            {
                PlayerManager.GetLocalPlayerAgent().FPSCamera.Shake(1.5f, amp, 0.09f);
            }
        }
    }
}
