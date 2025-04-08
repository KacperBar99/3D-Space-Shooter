using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    [Range(1f, 5000f)]
    float _thrustForce = 100f,
        _pitchForce = 100f,
        _rollForce = 100f,
        _yawForce = 100f;

    Rigidbody _rigidBody;

    [SerializeField][Range(-1f,1f)]
    float _thrustAmount, _pitchAmount, _rollAmount, _yawAmount;
    GameObject[] allobjects;

    private Speeds _speed;

    float _deadZoneRadius = 0.1f;

    Vector2 _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);


    public RectTransform cursorRect;
    public float smoothSpeed = 5f;
    private Vector2 targetPosition;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
         allobjects = FindObjectsOfType<GameObject>();
        List<GameObject> rootObjects = new List<GameObject>();
        foreach (GameObject obj in allobjects) 
        {
            if (obj.transform.parent == null)
            {
                rootObjects.Add(obj);
            }
        }
        allobjects = rootObjects.ToArray();
    }
    

    private void FixedUpdate()
    {
       
        Vector3 playerPos = this.transform.position;
        if (transform.position.magnitude >= 10000f)
        {
            foreach (GameObject obj in allobjects)
            {
                if (!obj.scene.IsValid())
                    continue;
                obj.transform.position = obj.transform.position - playerPos;

            }
        }
        if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidBody.AddTorque(transform.forward * (-_rollForce * _rollAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidBody.AddTorque(transform.right * (_pitchForce * _pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidBody.AddTorque(transform.up * (_yawForce * _yawAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidBody.AddForce(transform.forward * (_thrustForce * _thrustAmount * Time.fixedDeltaTime));
        }
    }

    public void Throttle(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (context.ReadValue<float>() > 0.0f)
        {
            this._speed.SetSpeed(this._speed.GetSpeed() + 0.25f);
        }
        else
        {
            this._speed.SetSpeed(this._speed.GetSpeed() - 0.25f);
        }
    }
    public void Thrust(InputAction.CallbackContext context)
    {
        Debug.Log(_rigidBody.linearVelocity.magnitude);
        this._thrustAmount = context.ReadValue<float>();
    }
    public void Roll(InputAction.CallbackContext context) 
    {
        this._rollAmount = context.ReadValue<float>();
    }
    public void Yaw(InputAction.CallbackContext context)
    {
        this._yawAmount = context.ReadValue<float>();
    }
    public void Pitch(InputAction.CallbackContext context)
    {
        this._pitchAmount = context.ReadValue<float>();
    }

    private void Update()
    {
        Vector3 targetPosition = Input.mousePosition;
        

        cursorRect.position = Vector2.Lerp(cursorRect.position, targetPosition, smoothSpeed * Time.deltaTime);

        float yaw = (targetPosition.x - _screenCenter.x) / _screenCenter.x;
        this._yawAmount = Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f;

        float pitch = (targetPosition.y - _screenCenter.y) / _screenCenter.y;
        this._pitchAmount = Mathf.Abs(pitch) > _deadZoneRadius ? pitch : 0f;
    }
}
