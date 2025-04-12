using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(ShipController))]
[RequireComponent (typeof(PlayerInput))]

public class HumanControl : MonoBehaviour
{
    private ShipController _ship;
    private GameObject[] allobjects;

    Vector2 _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);
    public Speeds _speed;
    private PlayerInput _playerInput;


    public RectTransform cursorRect;
    public float smoothSpeed = 5f;
    private Vector2 targetPosition;
    float _deadZoneRadius = 0.1f;
    bool _controller = false;


    private void Awake()
    {
        _ship = GetComponent<ShipController>();
        _playerInput = GetComponent<PlayerInput>();

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;


        //Moving scene setup
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
        //Moving scene
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
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale > 0f)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        //mouse look
        if (!this._controller)
        {
            Vector3 targetPosition = Input.mousePosition;

            cursorRect.position = Vector2.Lerp(cursorRect.position, targetPosition, smoothSpeed * Time.deltaTime);

            float yaw = (cursorRect.position.x - _screenCenter.x) / _screenCenter.x;
            this._ship.SetYawAmount(Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f);

            float pitch = (cursorRect.position.y - _screenCenter.y) / _screenCenter.y;
            this._ship.SetPitchAmount(Mathf.Abs(pitch) > _deadZoneRadius ? pitch : 0f);


            if (targetPosition.y > _screenCenter.y)
            {
                targetPosition.y -= ((targetPosition.y - _screenCenter.y) / _screenCenter.y) * Time.deltaTime;
            }
            else if (targetPosition.y < _screenCenter.y)
            {
                targetPosition.y += ((targetPosition.y - _screenCenter.y) / _screenCenter.y) * Time.deltaTime;
            }
            if (Time.deltaTime > 0f)
            {
                Vector2 newMousePos = Vector2.Lerp(targetPosition, _screenCenter, smoothSpeed * .5f * Time.deltaTime);
            }
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
        this._ship.SetThrustAmount(context.ReadValue<float>());
    }
    public void Roll(InputAction.CallbackContext context)
    {
        this._ship.SetRollAmount(context.ReadValue<float>());
    }
    public void Yaw(InputAction.CallbackContext context)
    {
        this._ship.SetYawAmount(context.ReadValue<float>());
    }
    public void Pitch(InputAction.CallbackContext context)
    {
        this._ship.SetPitchAmount(context.ReadValue<float>());
    }
    public void ControllChange()
    {
        if(_playerInput == null)
        {
            _playerInput = GetComponent<PlayerInput>();
        }
        if(_playerInput.currentControlScheme == "Gamepad")
        {
            this._controller = true;
            this.cursorRect.position = this._screenCenter;
        }else if(_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            this._controller= false;
        }
    }
}
