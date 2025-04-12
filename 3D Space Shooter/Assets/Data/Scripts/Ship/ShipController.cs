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
    float _thrustForce = 100f;

    Rigidbody _rigidBody;

    [SerializeField]
    [Range(-1f, 1f)]
    float _thrustAmount, _pitchAmount, _rollAmount, _yawAmount;

    [SerializeField]
    [Range(0f, 100f)]
    float _yawD, _pitchD, _rollD;



    float _deadZoneRadius = 0.1f;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
    }


    private void FixedUpdate()
    {
        if (!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidBody.AddForce(transform.forward * (_thrustForce * _thrustAmount));
        }
    }

    private void Update()
    {

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

        
    }
    public void SetThrustAmount(float amount) 
    {
        _thrustAmount = amount;
    }
    public void SetPitchAmount(float amount)
    {
        _pitchAmount = amount;
    }
    public void SetYawAmount(float amount)
    {
        _yawAmount = amount;
    }
    public void SetRollAmount(float amount)
    {
        _rollAmount = amount;
    }
}
