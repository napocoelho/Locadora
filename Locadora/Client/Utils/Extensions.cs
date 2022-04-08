

namespace Locadora.Client.Utils;

public static class Extensions
{
    /// <summary>
    /// Procura por incidências através de [predicate] e as substitui por [newItem].
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="newItem"></param>
    /// <param name="predicate"></param>
    public static void Replace<T>(this List<T> list, T newItem, Predicate<T> predicate)
    {
        if (list is null)
            return;

        for (int idx = 0; idx < list.Count; idx++)
        {
            if (predicate(list[idx]))
            {
                list[idx] = newItem;
            }
        }
    }

    /// <summary>
    /// Procura por inciências através de [predicate] e as remove.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="predicate"></param>
    public static void Remove<T>(this List<T> list, Predicate<T> predicate)
    {
        if (list is null)
            return;

        for (int idx = list.Count - 1; list.Any() && idx >= 0; idx--)
        {
            if (predicate(list[idx]))
            {
                list.RemoveAt(idx);
            }
        }
    }

    //public static bool ContainsIn
}