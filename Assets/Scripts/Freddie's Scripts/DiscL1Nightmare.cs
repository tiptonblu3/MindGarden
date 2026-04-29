using UnityEngine;

public class DiscL1Nightmare : MonoBehaviour, IInteractable
{
    public NightmareChecker nightmareChecker;

    public void Interact()
    {
        nightmareChecker.nightmareActive = true;
    }
}
