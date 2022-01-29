using System.Collections.Generic;

public static class List
{
    public static T PickOneAndRemove<T>(this List<T> source)
    {
        T picked = source.PickOne();
        source.Remove(picked);
        return picked;
    }

    public static void Shuffle<T>(this List<T> source)
    {
        var count = source.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = UnityEngine.Random.Range(i, count);
            var tmp = source[i];
            source[i] = source[r];
            source[r] = tmp;
        }
    }
}