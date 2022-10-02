using System;
using System.Threading;

namespace Hw3.Tests;

public class SingleInitializationSingleton
{
    private static readonly object Locker = new();

    private static volatile bool _isInitialized = false;

    public const int DefaultDelay = 3_000;

    private static Lazy<SingleInitializationSingleton> _instance = new(() => new SingleInitializationSingleton());

    public int Delay { get; }

    private SingleInitializationSingleton(int delay = DefaultDelay)
    {
        Delay = delay;
        Thread.Sleep(delay);
    }

    internal static void Reset()
    {
        if (!_isInitialized)
            return;
        lock (Locker)
        {
            if (!_isInitialized)
                return;

            _isInitialized = false;
            _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton());
        }
    }

    public static void Initialize(int delay)
    {
        if (_isInitialized)
            throw new InvalidOperationException();

        lock (Locker)
        {
            if (_isInitialized)
                throw new InvalidOperationException();

            _instance = new Lazy<SingleInitializationSingleton>(() => new SingleInitializationSingleton(delay));
            _isInitialized = true;
        }
    }

    public static SingleInitializationSingleton Instance => _instance.Value;

}