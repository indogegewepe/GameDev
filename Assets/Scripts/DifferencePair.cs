using UnityEngine;
using UnityEngine.UI;

public class DifferencePair : MonoBehaviour
{
    public Button buttonA;          // Invisible button on ImageA
    public Button buttonB;          // Invisible button on ImageB
    private bool differenceFound = false;

    void Start()
    {
        // Hide check icons at the start
        buttonA.image.color = new Color(1, 1, 1, 0);
        buttonB.image.color = new Color(1, 1, 1, 0);

        // Add listeners to both buttons
        buttonA.onClick.AddListener(OnDifferenceFound);
        buttonB.onClick.AddListener(OnDifferenceFound);
    }

    void OnDifferenceFound()
    {
        if (differenceFound) return; // Prevent multiple activations

        differenceFound = true;

        // Optionally, make buttons visible if you want to show the check action
        buttonA.image.color = Color.white; 
        buttonB.image.color = Color.white;

        // Disable buttons so they can't be clicked again
        buttonA.interactable = false;
        buttonB.interactable = false;

        // Notify the game controller
        SpotTheDifferenceController.Instance.DifferenceFound();
    }
}
