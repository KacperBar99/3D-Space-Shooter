using UnityEngine;

public class Destroy : MonoBehaviour
{
    [Header("Czas ¿ycia (sekundy)")]
    public float minTime = 1f; // Dolna granica
    public float maxTime = 5f; // Górna granica

    void Start()
    {
        float randomTime = Random.Range(minTime, maxTime); // Losowy czas
        Destroy(gameObject, randomTime); // Usuniêcie obiektu po czasie
    }
}