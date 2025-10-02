using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // New input system

public class EnergyManager : MonoBehaviour
{
    public Image energyBar;
    public float energyAmount = 100f;
    private bool isWatering = false;
    public float energyDepleted = 10f;
    public float maxEnergy = 100f;
    public float regenRatePerSecond = 5f;

    public void Update()
    {
        if (isWatering)
        {
            // Deplete while watering
            energyAmount -= energyDepleted;
            isWatering = false;
        }
        else
        {
            // Slowly regenerate when not watering
            energyAmount += regenRatePerSecond * Time.deltaTime;
            energyAmount = Mathf.Clamp(energyAmount, 0, maxEnergy);
        }

        // Update UI
        energyBar.fillAmount = energyAmount / maxEnergy;
    }

    public void StartWatering()
    {
        if (energyAmount > 0)
        {
            isWatering = true;
        }
    }

    public float GetEnergyAmount()
    {
        return energyAmount;
    }
}