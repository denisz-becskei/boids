using UnityEngine;

public class FlockInitializer : MonoBehaviour
{
    [SerializeField] private Flock flockBasePrefab;
    [SerializeField] private Color[] colors;
    
    void Start()
    {
        foreach (Color color in colors)
        {
            Flock newFlock = Instantiate(flockBasePrefab, Vector3.zero, Quaternion.identity, transform);
            newFlock.agentColor = color;
        }
    }
}
