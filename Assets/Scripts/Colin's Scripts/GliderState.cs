using UnityEngine;

public class GliderState : MonoBehaviour
{
    // Variables
    #region

    // Sees if the player has the Glide or not
    public bool hasGlide = false;

    #endregion

    // SetHasGlide
    #region

    // Checked true if player picks up the Glide
    public void SetHasGlide(bool value)
    {
        hasGlide = value;
        Debug.Log("Player has picked up the Glide ability");
    }

    #endregion
}
