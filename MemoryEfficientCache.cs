using System.Collections;

namespace Project_Euler;

/// <summary>
/// Provides memory-efficient caching strategies for Project Euler problems.
/// </summary>
public static class MemoryEfficientCache
{
    /// <summary>
    /// Creates a memory-efficient cache for Collatz sequence lengths.
    /// Uses a sparse array approach to save memory for large ranges.
    /// </summary>
    /// <param name="maxValue">Maximum value to cache</param>
    /// <param name="sparsityFactor">How sparse the cache should be (higher = less memory)</param>
    /// <returns>A cache that can store Collatz sequence lengths</returns>
    public static ICollatzCache CreateCollatzCache(uint maxValue, int sparsityFactor = 1)
    {
        if (sparsityFactor <= 0) sparsityFactor = 1;
        
        // For small ranges, use dense array
        if (maxValue <= 1000000)
        {
            return new DenseCollatzCache(maxValue);
        }
        
        // For large ranges, use sparse array
        return new SparseCollatzCache(maxValue, sparsityFactor);
    }

    /// <summary>
    /// Creates a memory-efficient prime sieve using BitArray.
    /// </summary>
    /// <param name="maxValue">Maximum value to sieve</param>
    /// <returns>A BitArray where true indicates prime numbers</returns>
    public static BitArray CreatePrimeSieve(int maxValue)
    {
        return Library.SieveOfEratosthenesBitArray(maxValue);
    }

    /// <summary>
    /// Creates a memory-efficient divisor sum cache.
    /// </summary>
    /// <param name="maxValue">Maximum value to cache</param>
    /// <returns>A cache for divisor sums</returns>
    public static IDivisorSumCache CreateDivisorSumCache(int maxValue)
    {
        return new DivisorSumCache(maxValue);
    }
}

public interface ICollatzCache
{
    ushort GetSequenceLength(ulong number);
    void SetSequenceLength(ulong number, ushort length);
    bool Contains(ulong number);
}

public interface IDivisorSumCache
{
    int GetDivisorSum(int number);
    void SetDivisorSum(int number, int sum);
    bool Contains(int number);
}

/// <summary>
/// Dense cache for small ranges - uses full array.
/// </summary>
public class DenseCollatzCache : ICollatzCache
{
    private readonly ushort[] _cache;
    private readonly uint _maxValue;

    public DenseCollatzCache(uint maxValue)
    {
        _maxValue = maxValue;
        _cache = new ushort[maxValue + 1];
    }

    public ushort GetSequenceLength(ulong number)
    {
        return number <= _maxValue ? _cache[number] : (ushort)0;
    }

    public void SetSequenceLength(ulong number, ushort length)
    {
        if (number <= _maxValue)
        {
            _cache[number] = length;
        }
    }

    public bool Contains(ulong number)
    {
        return number <= _maxValue && _cache[number] != 0;
    }
}

/// <summary>
/// Sparse cache for large ranges - uses dictionary for memory efficiency.
/// </summary>
public class SparseCollatzCache : ICollatzCache
{
    private readonly Dictionary<ulong, ushort> _cache;
    private readonly uint _maxValue;
    private readonly int _sparsityFactor;

    public SparseCollatzCache(uint maxValue, int sparsityFactor)
    {
        _maxValue = maxValue;
        _sparsityFactor = sparsityFactor;
        _cache = new Dictionary<ulong, ushort>();
    }

    public ushort GetSequenceLength(ulong number)
    {
        return _cache.TryGetValue(number, out var length) ? length : (ushort)0;
    }

    public void SetSequenceLength(ulong number, ushort length)
    {
        if (number <= _maxValue && number % (ulong)_sparsityFactor == 0)
        {
            _cache[number] = length;
        }
    }

    public bool Contains(ulong number)
    {
        return _cache.ContainsKey(number);
    }
}

/// <summary>
/// Memory-efficient cache for divisor sums.
/// </summary>
public class DivisorSumCache : IDivisorSumCache
{
    private readonly int[] _cache;
    private readonly int _maxValue;

    public DivisorSumCache(int maxValue)
    {
        _maxValue = maxValue;
        _cache = new int[maxValue + 1];
        InitializeCache();
    }

    private void InitializeCache()
    {
        // Initialize with 1 (every number is divisible by 1)
        for (int i = 1; i <= _maxValue; i++)
        {
            _cache[i] = 1;
        }

        // Add other divisors
        for (int i = 2; i <= _maxValue; i++)
        {
            for (int j = 2 * i; j <= _maxValue; j += i)
            {
                _cache[j] += i;
            }
        }
    }

    public int GetDivisorSum(int number)
    {
        return number <= _maxValue ? _cache[number] : 0;
    }

    public void SetDivisorSum(int number, int sum)
    {
        if (number <= _maxValue)
        {
            _cache[number] = sum;
        }
    }

    public bool Contains(int number)
    {
        return number <= _maxValue;
    }
}
