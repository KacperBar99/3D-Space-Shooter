using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Transform player;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        this.transform.position = player.position;
    }
}
