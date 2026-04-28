using UnityEngine;

public class SaveTracker : MonoBehaviour
{
    public CheckPointReturner cpr;
    public GlideState gs;

    [SerializeField] private bool glideHave;
    [SerializeField] private int discs;
    [SerializeField] private int checkpoint;
    
    void Update()
    {
        if (gs != null)
        {
            glideHave = gs.IsGlideUnlocked();
        }

        if (cpr != null)
        {
            checkpoint = cpr.GetCurrentCheckpointIndex();
            discs = cpr.GetDiscs();
        }
    }
}
