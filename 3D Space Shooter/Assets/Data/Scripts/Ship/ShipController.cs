using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEditor;
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

    [SerializeField]
    [Range(-1f, 1f)]
    float _thrustAmount, _pitchAmount, _rollAmount, _yawAmount;

    [SerializeField]
    [Range(0f, 100f)]
    float _yawD, _pitchD, _rollD;

    GameObject[] allobjects;

    private Speeds _speed;

    float _deadZoneRadius = 0.1f;

    Vector2 _screenCenter = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);


    public RectTransform cursorRect;
    public float smoothSpeed = 5f;
    private Vector2 targetPosition;
    [SerializeField] TextMeshProUGUI m_Object;
    [SerializeField] TextMeshProUGUI m_Object2;
    [SerializeField] TextMeshProUGUI m_Object3;
    [SerializeField] TextMeshProUGUI m_Object4;


    private void Awake()
    {
        if (Application.isEditor)
        {

            //_screenCenter = EditorGUIUtility.GetMainWindowPosition().center*.5f;
            Debug.Log(_screenCenter);
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        }
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        SetCursorPos((int)_screenCenter.x, (int)_screenCenter.y);
        UnityEngine.Cursor.visible = false;
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
        /*if (!Mathf.Approximately(0f, _rollAmount))
        {
            _rigidBody.AddTorque(transform.forward * (-_rollForce * _rollAmount));
        }
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            _rigidBody.AddTorque(transform.right * (_pitchForce * _pitchAmount));
        }
        if (!Mathf.Approximately(0f, _yawAmount))
        {
            _rigidBody.AddTorque(transform.up * (_yawForce * _yawAmount));
        }*/
        if (!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidBody.AddForce(transform.forward * (_thrustForce * _thrustAmount));
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
        Debug.Log(Input.GetAxis("Mouse X"));
        Debug.Log(Input.GetAxis("Mouse Y"));
        //To jest droga!
        Vector3 targetPosition = Input.mousePosition;

        cursorRect.position = Vector2.Lerp(cursorRect.position, targetPosition, smoothSpeed * Time.deltaTime);

        float yaw = (cursorRect.position.x - _screenCenter.x) / _screenCenter.x;
        this._yawAmount = Mathf.Abs(yaw) > _deadZoneRadius ? yaw : 0f;

        float pitch = (cursorRect.position.y - _screenCenter.y) / _screenCenter.y;
        this._pitchAmount = Mathf.Abs(pitch) > _deadZoneRadius ? pitch : 0f;


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
            //SetMousePosition(newMousePos);
        }

        if (!Mathf.Approximately(0f, _rollAmount))
        {
            transform.RotateAround(transform.position, transform.forward, -_rollD * _rollAmount * Time.deltaTime);
        }
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
            transform.RotateAround(transform.position, transform.right, _pitchD * _pitchAmount * Time.deltaTime);
        }
        if (!Mathf.Approximately(0f, _yawAmount))
        {
            transform.RotateAround(transform.position, transform.up, _yawD * _yawAmount * Time.deltaTime);
        }


        m_Object.text = "C " + (int)targetPosition.y;
        m_Object2.text = _rigidBody.linearVelocity.magnitude.ToString();
        m_Object3.text = this.transform.position.magnitude.ToString();
        m_Object4.text = "Y " + targetPosition.y.ToString();
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

        void SetMousePosition(Vector2 position)
        {
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
            // Dla systemu Windows
            var pos = new System.Drawing.Point((int)position.x, Screen.height - (int)position.y);
            SetCursorPos(pos.X, pos.Y);
#else
        Debug.LogWarning("Ustawianie pozycji kursora nie jest obs³ugiwane na tej platformie.");
#endif
        }
    }
}
