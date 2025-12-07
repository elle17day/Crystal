using UnityEngine;
using UnityEngine.UI; 
public class UIManager : MonoBehaviour
{
    // Create UI manager instance
    public static UIManager Instance;
    private void Awake()
    {
        // If there is no instance yet, this is the one
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            
            Destroy(gameObject);
        }
    }



}