using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Game.Explosions;

[AutoInject]
internal sealed class ExplosionEffectHandler : MonoBehaviour
{
    public Action EffectDoneOnce;

    private EffectLight _Light;
    private bool _EffectOnGoing = false;

    private float _TimeLeft = 0.0f;

    internal void DoEffect(ExplosionEffectData data)
    {
        transform.position = data.Position;

        if (_Light == null)
        {
            _Light = gameObject.GetComponent<EffectLight>();
            _Light.Setup();
        }

        _Light.UpdateVisibility(true);

        _Light.Color = data.Color;
        _Light.Range = data.Range;
        _Light.Intensity = data.Intensity;
        
        _EffectOnGoing = true;
        _TimeLeft = data.Duration;
    }

    private void FixedUpdate()
    {
        if (!_EffectOnGoing)
            return;

        if ((_TimeLeft -= Time.fixedDeltaTime) <= 0.0f)
        {
            OnDone();
        }
    }

    private void OnDone()
    {
        if (_Light != null)
        {
            _Light.UpdateVisibility(false);
        }

        EffectDoneOnce?.Invoke();
        EffectDoneOnce = null;
        _Light = null;
        _EffectOnGoing = false;
        _TimeLeft = 0.0f;
    }
}
