using FloLib.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FloLib.Utils;

/// <summary>
/// Utility Class for Easier Random Number Generation
/// </summary>
public class RNG
{
    private const int Float01Precision = 2000000000;
    private const float Float01Inv = 1.0f / (float)Float01Precision;

    /// <summary>
    /// GlobalRNG to use RNG without creating new instance
    /// </summary>
    public static GlobalRNG Global { get; private set; } = new GlobalRNG();

    private static uint _UniqueIDForRandomSeed = 0;

    private Random _Rand;
    private int _Seed;

    public RNG()
    {
        if (_UniqueIDForRandomSeed == uint.MaxValue)
        {
            _UniqueIDForRandomSeed = 0;
        }
        else
        {
            _UniqueIDForRandomSeed++;
        }

        _Seed = $"{_UniqueIDForRandomSeed} {Environment.TickCount64}".GetHashCode();
        _Rand = new(_Seed);
    }

    public RNG(int seed)
    {
        _Seed = seed;
        _Rand = new(seed);
    }

    public virtual void Reset(int? newSeed = null)
    {
        if (newSeed != null)
        {
            _Seed = newSeed.Value;
        }
        _Rand = new(_Seed);
    }

    /// <summary>
    /// return random float value 0.0 and 1.0 are both inclusive
    /// </summary>
    public float Float01
    {
        get => _Rand.Next(0, Float01Precision + 1) * Float01Inv;
    }

    /// <summary>
    /// return random int (0 ~ 2147483646)
    /// </summary>
    public int Int0ToPositive
    {
        get => _Rand.Next(0, int.MaxValue);
    }

    /// <summary>
    /// return random int (–2147483648 ~ 0)
    /// </summary>
    public int Int0ToNegative
    {
        get => _Rand.Next(int.MinValue, 1);
    }

    /// <summary>
    /// return random int value (–2147483648 ~ 2147483646)
    /// </summary>
    public int Int
    {
        get => _Rand.Next(int.MinValue, int.MaxValue);
    }

    /// <summary>
    /// Check if probability has pass (0.0 - 1.0)
    /// </summary>
    /// <param name="probability">probability (0.0 - 1.0) lower = less</param>
    /// <returns>true if probability has pass</returns>
    public bool Probability(float probability)
    {
        if (probability <= 0.0f)
            return false;

        if (probability >= 1.0f)
            return true;

        return Float01 < probability;
    }
    
    /// <summary>
    /// Make possibility of 1 in x situation
    /// </summary>
    /// <param name="cases">total case count</param>
    /// <returns>if pass 1 in cases chance, true otherwise false</returns>
    public bool OneIn(int cases)
    {
        if (cases <= 0)
        {
            Logger.Warn($"{nameof(RNG)}.{nameof(RNG.OneIn)} received {cases} (0 or negative); fallback to false");
            return false;
        }

        if (cases == 1)
            return true;

        return _Rand.Next(0, cases) == 0;
    }

    /// <summary>
    /// Randomly Choice single item in sequence
    /// </summary>
    /// <typeparam name="T">Type of sequence element</typeparam>
    /// <param name="items">Sequence of items</param>
    /// <returns>Return selected item</returns>
    public T Choice<T>(IEnumerable<T> items)
    {
        if (IsChoiceHaveSimpleScenarioValue(items, out var fastResult))
        {
            return fastResult;
        }

        var length = items.Count();
        var randomIndex = _Rand.Next(0, length);
        return items.ElementAt(randomIndex);
    }

    /// <summary>
    /// Randomly Choice number of item in sequence
    /// </summary>
    /// <typeparam name="T">Type of sequence element</typeparam>
    /// <param name="items">Sequence of items</param>
    /// <param name="count">Count of items</param>
    /// <returns>Array of chosen item</returns>
    public T[] Choice<T>(IEnumerable<T> items, int count)
    {
        if (IsChoiceHaveSimpleScenarioValue(items, out var fastResult))
        {
            return new T[] { fastResult };
        }

        var shuffled = items.OrderBy(x => _Rand.NextDouble());
        return shuffled.Take(count).ToArray();
    }

    /// <summary>
    /// Choice Item with Weighted Values
    /// </summary>
    /// <typeparam name="T">Item Generic Type</typeparam>
    /// <param name="itemTuples">Tuple Enumerable which contains item and weight</param>
    /// <returns></returns>
    public T WeightedChoice<T>(IEnumerable<(T item, float weight)> itemTuples)
    {
        if (IsChoiceHaveSimpleScenarioValue(itemTuples, out var fastResult))
        {
            return fastResult.item;
        }

        T[] items = itemTuples.Select(x=>x.item).ToArray();
        float[] weights = itemTuples.Select(x=>x.weight).ToArray();

        float accumulate = 0.0f;
        for (int i = 0; i<weights.Length; i++)
        {
            var weight = weights[i];
            if (weight <= 0.0f)
            {
                weights[i] = -1.0f;
                continue;
            }

            accumulate += weight;
            weights[i] = accumulate;
        }

        if (accumulate <= 0.0f)
        {
            return Choice(items);
        }

        //How it works (ie):
        // index:  0    1     2     3
        // inWei:  2.0  0.0   5.0   8.0
        // outWei: 2.0  -1.0  7.0   15.0
        // acc:    2.0  2.0   7.0   15.0
        //
        // pickedWeight <= weights[i]: pick that item
        //
        //ie)
        //
        //5.0 <= 2.0? false : Not the first item
        //5.0 <= -1.0? false : Weight 0.0 Second item ignored
        //5.0 <= 10.0? true : Second item PICKED

        float pickedWeight = Float01 * accumulate;
        for (int i = 0; i < weights.Length; i++)
        {
            if (i == (weights.Length - 1) || pickedWeight <= weights[i])
            {
                return items[i];
            }
        }

        throw new InvalidOperationException("What? this should never happen");
    }

    private static bool IsChoiceHaveSimpleScenarioValue<T>(IEnumerable<T> items, out T value)
    {
        var count = items.Count();
        if (count <= 0)
        {
            throw new ArgumentException("Argument Item Count is 0!", nameof(items));
        }

        if (count == 1)
        {
            value = items.First();
            return true;
        }

        value = default;
        return false;
    }
}

/// <summary>
/// RNG for Global
/// </summary>
public sealed class GlobalRNG : RNG
{
    /// <summary>
    /// This does nothing Do NOT use this
    /// </summary>
    /// <param name="newSeed">no</param>
    public sealed override void Reset(int? newSeed = null)
    {
        Logger.Warn("Resetting Seed Value is not allowed on RNG.Global");
    }
}