using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StoreManager : MonoBehaviour
{
    public Button BuySeedsButton; //
    public Button startNextLevelButton;
    public TMPro.TextMeshProUGUI fundText;
    private int playerFund = 100; //hardcoded

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        fundText.text = "Fund: $" + playerFund; // Update fund text
        startNextLevelButton.interactable = false; // Show next level button

        BuySeedsButton.onClick.AddListener(BuySeeds);
        startNextLevelButton.onClick.AddListener(StartNextLevel);

    }

    // Update is called once per frame
    void BuySeeds()
    {
        if (playerFund >= 50)
        {
            playerFund -= 50; // Deduct 50 from player's fund
            fundText.text = "Fund: $" + playerFund; // Update fund text
            startNextLevelButton.interactable = true; // Enable next level button
        }

    }
    
    void StartNextLevel()
    {
        SceneManager.LoadScene("Scene3");
    }
}
