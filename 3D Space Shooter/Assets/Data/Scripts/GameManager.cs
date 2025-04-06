using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform player;
    [SerializeField]
    float _time = 10f;
    float _timer = 0f;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;

        if(_timer >= _time)
        {
            _timer = 0f;
            this.transform.position = player.position;
        }
    }
}
