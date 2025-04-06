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
    }
    bool IsChildOfPlayer(Transform t)
    {
        while (t != null)
        {
            if (t.CompareTag("Player"))
                return true;

            t = t.parent;
        }

        return false;
    }

    private void FixedUpdate()
    {
        //To wymaga jeszcze masy pracy
        if (Mathf.Abs(this.transform.position.z)>10000)
        {
            foreach(GameObject obj in allobjects)
            {
                if (!obj.scene.IsValid() || obj == this.gameObject)
                    continue;

                if (IsChildOfPlayer(obj.transform))
                    continue;
                obj.transform.position = obj.transform.position + new Vector3(0, 0, 10000f);
                
            }
            this.transform.position=new Vector3(transform.position.x,transform.position.y,0);


        }
        if (!Mathf.Approximately(0f, _rollAmount))
        {
            float x, y;
            x = transform.rotation.eulerAngles.x;
            y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(x, y, _rollAmount * 360); //tak trzeba jakoœ, fizyka nie dzia³a xD

            //_rigidBody.AddTorque(transform.forward * (_rollForce * _rollAmount * Time.fixedDeltaTime));

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
            _rigidBody.maxLinearVelocity = 10000;
            _rigidBody.linearVelocity = transform.forward * 1000 * _thrustAmount;
        }
        Debug.Log(_rigidBody.linearVelocity);
    }
}
