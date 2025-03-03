using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Collections;
using System.Runtime.CompilerServices;

//namespace DeftSharp.Windows.Input{ }

/// <summary>
///  net472_polyFill
/// </summary>
public static partial class PolyFillExtensions
{
    //conditinal compile to get best performance.. 
    //when testing Remove conditinal formatting.
#if NETFRAMEWORK
    /// <summary>
    /// net472_polyFill - Queue.TakeLast
    /// </summary>
    public static IEnumerable<T> TakeLast<T>(this Queue<T> queue, int count)
    {
        if (queue is null)
            throw new ArgumentNullException("source is null ");

        if (count <= 0 || queue.Count == 0)
            return [];

        if (count <= 0)
            return Enumerable.Empty<T>();
        if (count >= queue.Count)
            return queue.ToList();

        //  isStartIndexFromEnd: true, startIndex: count,
        //  isEndIndexFromEnd: true, endIndex: 0);
        //return queue.Take(queue.Count - count).ToList();

        var startIndex = queue.Count - count;
        var take = count;

        return queue.Skip(startIndex).Take(take).ToList();

    }


    /// <summary>
    ///  net472_polyFill
    /// </summary>
    public static TValue GetValueOrDefault<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue)
    {
        return dictionary.TryGetValue(key, out var value) ? value : defaultValue;
    }

    /// <summary>
    ///  net472_polyFill 
    /// </summary>
    public static bool TryRemove<TK, TValue>(this ConcurrentDictionary<TK, TValue> dictionary, KeyValuePair<TK, TValue> item)
    {
        return dictionary.TryRemove(item.Key, out _);
    }
#endif


    /// <summary>
    /// net472_polyFill - nint.Zero
    /// </summary>
    public static nint nint_Zero
    {
        get { return (nint)0; } // Or return default(nint); in .NET Framework 4.7.2
    }

}

 

