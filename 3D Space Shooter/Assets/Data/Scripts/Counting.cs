using UnityEngine;

[ExecuteInEditMode] // Dzia�a w edytorze bez odpalania gry
public class ChildCounter : MonoBehaviour
{
    public int counter = 0;
    void Update()
    {
        counter=transform.childCount;
    }
}