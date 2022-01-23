using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class IEnumerableExtensions
{
    public static T PickOne<T>(this IEnumerable<T> source)
    {
        return source.ElementAt(Random.Range(0, source.Count()));
    }
}