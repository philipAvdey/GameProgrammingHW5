using UnityEngine;

public class PlantGrowth : MonoBehaviour
{
    [Header("Plant Growth Stages")]
    public GameObject smallPlant;   // Planted stage
    public GameObject mediumPlant;  // Growing stage
    public GameObject largePlant;   // Mature stage

    [Header("Growth Timing")]
    public int daysToMedium = 2;    // Days until small -> medium
    public int daysToLarge = 2;     // Days after medium -> large

    private int currentDaysPassed = 0;
    private GameObject dayNightControl;

    public enum PlantStage
    {
        Small,
        Medium,
        Large
    }
    public PlantStage currentStage = PlantStage.Small;

    void Start()
    {
        // Find the day/night controller
        dayNightControl = GameObject.Find("DayNightController");

        if (dayNightControl != null)
        {
            // Subscribe directly to the event (not using the helper method)
            DayNightControl dayControl = dayNightControl.GetComponent<DayNightControl>();
            dayControl.dayPassedEvent.AddListener(ADayPassed);
            Debug.Log(gameObject.name + " registered for day events");
        }
        else
        {
            Debug.LogError("DayNightController not found!");
        }

        // Start at small stage
        SetPlantStage(PlantStage.Small);
    }

    // This gets called automatically when a day passes (by DayNightControl)
    public void ADayPassed()
    {
        currentDaysPassed++;
        Debug.Log(gameObject.name + " - Days passed: " + currentDaysPassed);

        // Check if we should grow to next stage
        if (currentStage == PlantStage.Small && currentDaysPassed >= daysToMedium)
        {
            SetPlantStage(PlantStage.Medium);
        }
        else if (currentStage == PlantStage.Medium && currentDaysPassed >= (daysToMedium + daysToLarge))
        {
            SetPlantStage(PlantStage.Large);
        }
    }

    void SetPlantStage(PlantStage newStage)
    {
        currentStage = newStage;

        // Hide all plants first
        if (smallPlant != null) smallPlant.SetActive(false);
        if (mediumPlant != null) mediumPlant.SetActive(false);
        if (largePlant != null) largePlant.SetActive(false);

        // Show the correct stage
        switch (currentStage)
        {
            case PlantStage.Small:
                if (smallPlant != null) smallPlant.SetActive(true);
                Debug.Log(gameObject.name + " is now SMALL");
                break;
            case PlantStage.Medium:
                if (mediumPlant != null) mediumPlant.SetActive(true);
                Debug.Log(gameObject.name + " is now MEDIUM");
                break;
            case PlantStage.Large:
                if (largePlant != null) largePlant.SetActive(true);
                Debug.Log(gameObject.name + " is now LARGE (MATURE)");
                break;
        }
    }
}