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
public class Speeds
{
    private enum speed
    {
        zero,
        quater,
        half,
        extra,
        full,
        reverse
    };

    speed value = speed.zero;
    public float GetSpeed()
    {
        switch (value)
        {
            case speed.reverse:
                {
                    return -0.25f;
                }
            case speed.full:
                {
                    return 1.0f;
                }
            case speed.extra:
                {
                    return 0.75f;
                }
            case speed.half:
                {
                    return 0.5f;
                }
            case speed.quater:
                {
                    return 0.25f;
                }
            case speed.zero:
            default:
                return 0.0f;
        }

    }
    public void SetSpeed(float input)
    {
        switch (input)
        {
            case 0.0f:
                {
                    this.value = speed.zero;
                    break;
                }
            case 0.25f:
                {
                    this.value = speed.quater;
                    break;
                }
            case 0.5f:
                {
                    this.value = speed.half;
                    break;
                }
            case 0.75f:
                {
                    this.value = speed.extra;
                    break;
                }
            default:
                if (input < 0f)
                {
                    this.value = speed.reverse;
                    break;
                }
                else
                {
                    this.value = speed.full;
                    break;
                }
        }
    }
}