using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    [Range(1000f, 10000f)]
    float _thrustForce = 7500f,
        _pitchForce = 6000f,
        _rollForce = 1000f,
        _yawForce = 2000f;

    Rigidbody _rigidBody;

    [SerializeField][Range(-1f,1f)]
    float _thrustAmount, _pitchAmount, _rollAmount, _yawAmount;
    GameObject[] allobjects;
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
           /* float x, y;
            x = transform.rotation.eulerAngles.x;
            y = transform.rotation.eulerAngles.y;*/
            //transform.rotation = Quaternion.Euler(x, y, _rollAmount * 180); //tak trzeba jakoœ, fizyka nie dzia³a xD

            _rigidBody.AddTorque(transform.forward * (-_rollForce * _rollAmount * Time.fixedDeltaTime));

        }
        if (!Mathf.Approximately(0f, _pitchAmount))
        {
           /* float y, z;
            z = transform.rotation.eulerAngles.z;
            y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(_pitchAmount * 180, y, z);*/
            _rigidBody.AddTorque(transform.right * (_pitchForce * _pitchAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _yawAmount))
        {
            /*float x, z;
            x = transform.rotation.eulerAngles.x;
            z = transform.rotation.eulerAngles.z;
            transform.rotation = Quaternion.Euler(x, _yawAmount * 180, z);*/
            _rigidBody.AddTorque(transform.up * (_yawForce * _yawAmount * Time.fixedDeltaTime));
        }
        if (!Mathf.Approximately(0f, _thrustAmount))
        {
            _rigidBody.maxLinearVelocity = 1000;
            _rigidBody.linearVelocity = transform.forward * 1000 * _thrustAmount;
        }
    }
}
