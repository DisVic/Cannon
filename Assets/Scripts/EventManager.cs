using System.Diagnostics;
using UnityEngine.Events;

public static class EventManager
{
    public static event UnityAction<int> OnAddPoints;

    public static void RaiseAddPoints(int points) => OnAddPoints?.Invoke(points);
}