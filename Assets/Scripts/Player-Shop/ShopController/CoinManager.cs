using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public TMP_Text moneyText; // Reference to the money text UI element

    public int coins; // Store the current number of coins

    private void Start()
    {
        // Initialize the money text UI element
        LoadCoins();
        UpdateMoneyText();
    }

    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateMoneyText(); // Update the money text when coins are added
        SaveCoins(); // Save the coins to PlayerPrefs
        Debug.Log("Coins added: " + amount);
    }

    public bool SubtractCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateMoneyText(); // Update the money text when coins are subtracted
            SaveCoins(); // Save the coins to PlayerPrefs
            Debug.Log("Coins subtracted: " + amount);
            return true;
        }
        Debug.Log("Not enough coins to subtract: " + amount);
        return false;
    }

    private void UpdateMoneyText()
    {
        if (moneyText != null)
        {
            moneyText.text = coins.ToString();
            Debug.Log("Money text updated to: " + coins);
        }
    }

    private void LoadCoins()
    {
        coins = PlayerPrefs.GetInt("Money", 0); // Load coins from PlayerPrefs
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("Money", coins); // Save coins to PlayerPrefs
    }
}