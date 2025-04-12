using TMPro;
using UnityEngine;
[RequireComponent(typeof(ShipController))]
public class DisplaysController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI m_Object;
    [SerializeField] TextMeshProUGUI m_Object2;
    [SerializeField] TextMeshProUGUI m_Object3;
    [SerializeField] TextMeshProUGUI m_Object4;

    private ShipController _ship;

    //tmp
    private Rigidbody _rigidbody;
    private Speeds _speeds;
    private void Awake()
    {
        _ship = GetComponent<ShipController>();
        _rigidbody = GetComponent<Rigidbody>();
        _speeds = GetComponent<HumanControl>()._speed;
        if(_speeds == null)_speeds = new Speeds();
    }

    // Update is called once per frame
    void Update()
    {
        m_Object.text = this.transform.position.magnitude.ToString();
        m_Object2.text = this._rigidbody.linearVelocity.ToString();
        m_Object3.text = this._rigidbody.linearVelocity.magnitude.ToString();
        m_Object4.text = _speeds.GetSpeed().ToString();
    }
}
