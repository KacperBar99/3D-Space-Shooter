using UnityEngine;

public static class Statics
{
    public static Vector2 rotateVector2(Vector2 v, float rotation)
    {
        rotation *= Mathf.Deg2Rad;
        return new Vector2(
            v.x * Mathf.Cos(rotation) - v.y * Mathf.Sin(rotation),
            v.x * Mathf.Sin(rotation) + v.y * Mathf.Cos(rotation)
            );
    }
    public static bool IsChildOfPlayer(Transform t)
    {
        while (t != null)
        {
            if (t.CompareTag("Player"))
                return true;

            t = t.parent;
        }

        return false;
    }
}