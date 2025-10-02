using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public FarmTIleManager tileManager; // Reference to the FarmTileManager
    public GameObject winUI; // UI to show when player wins
    public Button nextLevelButton; // Button to go to the store
    public TextMeshProUGUI fundText; // Text to display player's fund

    private int playerFund = 0; // Player's fund amount in beginning
    private bool hasWon = false; // To ensure win condition is only triggered once

    private void Start() 
    {
        winUI.SetActive(false); // Hide win UI at start
        nextLevelButton.gameObject.SetActive(false); // Hide next level button at start

        nextLevelButton.onClick.AddListener(GoToStore); // Add listener to button
    }

    private void Update()
    {
        if (!hasWon && CheckAllWatered()) // Check win condition
        {
            WinLevel(); // Trigger win condition
        }
    }

    private bool CheckAllWatered() // Check if all farm tiles are watered
    {
        foreach (FarmTileControl tile in tileManager.allFarmTiles) // Iterate through all farm tiles
        {
            if (tile.tileCond != FarmTileControl.FarmTileCond.Watered) // If any tile is not watered
                return false; // Return false
        }
        return true; // All tiles are watered, return true
    }

    private void WinLevel()
    {
        hasWon = true; // Set hasWon to true to prevent multiple triggers since using bool
        winUI.SetActive(true); // Show win UI
        nextLevelButton.gameObject.SetActive(true); // Show next level button

        // Add funds
        playerFund += 100; // Add 100 to player's fund
        fundText.text = "Fund: $" + playerFund; // Update fund text
    }

    private void GoToStore()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2-Store"); // Load store scene
    }
}

