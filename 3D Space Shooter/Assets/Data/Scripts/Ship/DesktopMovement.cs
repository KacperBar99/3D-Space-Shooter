using System;
using UnityEngine;

[Serializable]
public class DesktopMovement : MovementControlsBase
{
    [SerializeField]
    float _deadZoneRadius = 0.1f;

    Vector2 _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

    public override float YawAmount {
        get {
            Vector3 mousePosition = Input.mousePosition;
            float yaw = (mousePosition.x - _screenCenter.x) / _screenCenter.x;
            return Math.Abs(yaw) > _deadZoneRadius ? yaw : 0f;
        } 
    }

    public override float PitchAmount
    {
        get
        {
            Vector3 mousePosition = Input.mousePosition;
            float pitch = (mousePosition.y - _screenCenter.y) / _screenCenter.y;
            return Math.Abs(pitch) > _deadZoneRadius ? pitch : 0f;
        }
    }

    public override float RollAmount { get; }

    public override float ThrustAmount { get; }
}
