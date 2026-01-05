using UnityEngine;

public class Crystal : MonoBehaviour
{
    public static Crystal Instance;
    private void Awake()
    {
        // If there is no instance yet, this is the one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            // If one already exists, destroy this duplicate
            Destroy(gameObject);
        }
    }
    private float health = 100;

    public Vector3 GetCrystalLocation()
    {
        return transform.position;
    }
}
