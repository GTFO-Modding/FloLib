using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace FloLib.Utils;
/// <summary>
/// Easing Functions
/// </summary>
public static class EaseFunc
{
	/// <summary>
	/// Type of Easing Function
	/// </summary>
    public enum Type : byte
    {
		/// <summary>
		/// No Easing
		/// </summary>
        Linear,
		/// <summary>
		/// No Easing, Always return 0.0
		/// </summary>
		Zero,
		/// <summary>
		/// No Easing, Always return 1.0
		/// </summary>
		One,


		/// <summary>
		/// Quadratic-In Ease (Slow Movement on Start)
		/// </summary>
		InQuad,
		/// <summary>
		/// Quadratic-In Ease (Fast Movement on Start)
		/// </summary>
		OutQuad,
		/// <summary>
		/// Quadratic-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutQuad,


		/// <summary>
		/// Cubic-In Ease (Slow Movement on Start)
		/// </summary>
		InCubic,
		/// <summary>
		/// Cubic-Out Ease (Fast Movement on Start)
		/// </summary>
		OutCubic,
		/// <summary>
		/// Cubic-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutCubic,


		/// <summary>
		/// Quartic-In Ease (Slow Movement on Start)
		/// </summary>
		InQuart,
		/// <summary>
		/// Quartic-Out Ease (Fast Movement on Start)
		/// </summary>
		OutQuart,
		/// <summary>
		/// Quartic-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutQuart,


		/// <summary>
		/// Quintic-In Ease (Slow Movement on Start)
		/// </summary>
		InQuint,
		/// <summary>
		/// Quintic-Out Ease (Fast Movement on Start)
		/// </summary>
		OutQuint,
		/// <summary>
		/// Quintic-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutQuint,


		/// <summary>
		/// Sine-In Ease (Slow Movement on Start)
		/// </summary>
		InSine,
		/// <summary>
		/// Sine-Out Ease (Fast Movement on Start)
		/// </summary>
		OutSine,
		/// <summary>
		/// Sine-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutSine,

		/// <summary>
		/// Exponential-In Ease (Slow Movement on Start)
		/// </summary>
		InExpo,
		/// <summary>
		/// Exponetial-Out Ease (Fast Movement on Start)
		/// </summary>
		OutExpo,
		/// <summary>
		/// Exponential-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutExpo,

		/// <summary>
		/// Circular-In Ease (Slow Movement on Start)
		/// </summary>
		InCirc,
		/// <summary>
		/// Circular-Out Ease (Fast Movement on Start)
		/// </summary>
		OutCirc,
		/// <summary>
		/// Circular-InOut Ease (Slow Movement on Start and End)
		/// </summary>
		InOutCirc,


		/// <summary>
		/// Elastic-In Ease
		/// </summary>
		InElastic,
		/// <summary>
		/// Elastic-Out Ease
		/// </summary>
		OutElastic,
		/// <summary>
		/// Elastic-InOut Ease
		/// </summary>
		InOutElastic,


		/// <summary>
		/// Back-In Ease
		/// </summary>
        InBack,
		/// <summary>
		/// Back-Out Ease
		/// </summary>
		OutBack,
		/// <summary>
		/// Back-InOut Ease
		/// </summary>
		InOutBack,


		/// <summary>
		/// Bounce-In Ease
		/// </summary>
        InBounce,
		/// <summary>
		/// Bounce-Out Ease
		/// </summary>
		OutBounce,
		/// <summary>
		/// Bounce-InOut Ease
		/// </summary>
		InOutBounce
	}

	public static float Evaluate(this Type type, float t)
    {
		t = Mathf.Clamp01(t);

		return type switch
		{
			Type.Linear => t,
			Type.Zero => 0.0f,
			Type.One => 1.0f,
			Type.InQuad => InQuad(t),
			Type.OutQuad => OutQuad(t),
			Type.InOutQuad => InOutQuad(t),
			Type.InCubic => InCubic(t),
			Type.OutCubic => OutCubic(t),
			Type.InOutCubic => InOutCubic(t),
			Type.InQuart => InQuart(t),
			Type.OutQuart => OutQuart(t),
			Type.InOutQuart => InOutQuart(t),
			Type.InQuint => InQuint(t),
			Type.OutQuint => OutQuint(t),
			Type.InOutQuint => InOutQuint(t),
			Type.InSine => InSine(t),
			Type.OutSine => OutSine(t),
			Type.InOutSine => InOutSine(t),
			Type.InExpo => InExpo(t),
			Type.OutExpo => OutExpo(t),
			Type.InOutExpo => InOutExpo(t),
			Type.InCirc => InCirc(t),
			Type.OutCirc => OutCirc(t),
			Type.InOutCirc => InOutCirc(t),
			Type.InElastic => InElastic(t),
			Type.OutElastic => OutElastic(t),
			Type.InOutElastic => InOutElastic(t),
			Type.InBack => InBack(t),
			Type.OutBack => OutBack(t),
			Type.InOutBack => InOutBack(t),
			Type.InBounce => InBounce(t),
			Type.OutBounce => OutBounce(t),
			Type.InOutBounce => InOutBounce(t),
			_ => throw new ArgumentOutOfRangeException(nameof(type), "Given type was invalid!")
		};
    }


	/// <summary>
	/// Quadratic-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InQuad(float t) => t * t;
	/// <summary>
	/// Quadratic-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutQuad(float t) => 1 - InQuad(1 - t);
	/// <summary>
	/// Quadratic-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutQuad(float t)
	{
		if (t < 0.5) return InQuad(t * 2) / 2;
		return 1 - InQuad((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Cubic-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InCubic(float t) => t * t * t;
	/// <summary>
	/// Cubic-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutCubic(float t) => 1 - InCubic(1 - t);
	/// <summary>
	/// Cubic-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutCubic(float t)
	{
		if (t < 0.5) return InCubic(t * 2) / 2;
		return 1 - InCubic((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Quartic-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InQuart(float t) => t * t * t * t;
	/// <summary>
	/// Quartic-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutQuart(float t) => 1 - InQuart(1 - t);
	/// <summary>
	/// Quartic-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutQuart(float t)
	{
		if (t < 0.5) return InQuart(t * 2) / 2;
		return 1 - InQuart((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Quintic-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InQuint(float t) => t * t * t * t * t;
	/// <summary>
	/// Quintic-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutQuint(float t) => 1 - InQuint(1 - t);
	/// <summary>
	/// Quintic-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutQuint(float t)
	{
		if (t < 0.5) return InQuint(t * 2) / 2;
		return 1 - InQuint((1 - t) * 2) / 2;
	}
	

	/// <summary>
	/// Sine-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InSine(float t) => (float)-Math.Cos(t * Math.PI / 2);
	/// <summary>
	/// Sine-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutSine(float t) => (float)Math.Sin(t * Math.PI / 2);
	/// <summary>
	/// Sine-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutSine(float t) => (float)(Math.Cos(t * Math.PI) - 1) / -2;


	/// <summary>
	/// Exponential-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InExpo(float t) => (float)Math.Pow(2, 10 * (t - 1));
	/// <summary>
	/// Exponential-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutExpo(float t) => 1 - InExpo(1 - t);
	/// <summary>
	/// Exponential-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutExpo(float t)
	{
		if (t < 0.5) return InExpo(t * 2) / 2;
		return 1 - InExpo((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Circular-In Ease (Slow Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InCirc(float t) => -((float)Math.Sqrt(1 - t * t) - 1);
	/// <summary>
	/// Circular-Out Ease (Fast Movement on Start)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutCirc(float t) => 1 - InCirc(1 - t);
	/// <summary>
	/// Circular-InOut Ease (Slow Movement on Start and End)
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutCirc(float t)
	{
		if (t < 0.5) return InCirc(t * 2) / 2;
		return 1 - InCirc((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Elastic-In Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InElastic(float t) => 1 - OutElastic(1 - t);
	/// <summary>
	/// Elastic-Out Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutElastic(float t)
	{
		float p = 0.3f;
		return (float)Math.Pow(2, -10 * t) * (float)Math.Sin((t - p / 4) * (2 * Math.PI) / p) + 1;
	}
	/// <summary>
	/// Elastic-InOut Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutElastic(float t)
	{
		if (t < 0.5) return InElastic(t * 2) / 2;
		return 1 - InElastic((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Back-In Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InBack(float t)
	{
		float s = 1.70158f;
		return t * t * ((s + 1) * t - s);
	}
	/// <summary>
	/// Back-Out Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutBack(float t) => 1 - InBack(1 - t);
	/// <summary>
	/// Back-InOut Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutBack(float t)
	{
		if (t < 0.5) return InBack(t * 2) / 2;
		return 1 - InBack((1 - t) * 2) / 2;
	}


	/// <summary>
	/// Bounce-In Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InBounce(float t) => 1 - OutBounce(1 - t);
	/// <summary>
	/// Bounce-Out Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float OutBounce(float t)
	{
		float div = 2.75f;
		float mult = 7.5625f;

		if (t < 1 / div)
		{
			return mult * t * t;
		}
		else if (t < 2 / div)
		{
			t -= 1.5f / div;
			return mult * t * t + 0.75f;
		}
		else if (t < 2.5 / div)
		{
			t -= 2.25f / div;
			return mult * t * t + 0.9375f;
		}
		else
		{
			t -= 2.625f / div;
			return mult * t * t + 0.984375f;
		}
	}
	/// <summary>
	/// Bounce-InOut Ease
	/// </summary>
	/// <param name="t">Input Time [0.0 - 1.0]</param>
	/// <returns>Eased Time</returns>
	public static float InOutBounce(float t)
	{
		if (t < 0.5) return InBounce(t * 2) / 2;
		return 1 - InBounce((1 - t) * 2) / 2;
	}
}
