using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
//using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;

public class TowerBuildPrompt : MonoBehaviour
{
    public GameObject buyIndicatorCircle;
    public Material materialBuy;
    public Material materialNoBuy;

    public int playerBalance = 10;
    public bool isBuyable = false;
    public bool isBought = false;

    void Update()
    {
        // Checks if the towers can be purchased and sets the indicator material accordingly
        if (playerBalance >= 10)
        {
            isBuyable = true;
            buyIndicatorCircle.GetComponent<MeshRenderer>().material = materialBuy;
        } 
        else if (isBought == true)
        {
            buyIndicatorCircle.SetActive(false);
        } 
        else
        {
            isBuyable = false;
            buyIndicatorCircle.GetComponent<MeshRenderer>().material = materialNoBuy;
        }

        // If player presses B, call BuyTower method
        if (Input.GetKeyDown(KeyCode.B))
        {
            BuyTower();
        }
    }

    public void BuyTower()
    {
        // Checks if player balance is high enough
        if (playerBalance >= 10)
        {
            // Checks if the tower is listed as buyable
            if (isBuyable)
            {
                // Removes player balance
                playerBalance -= 10;
                isBought = true;
            }
        }
    }
}
