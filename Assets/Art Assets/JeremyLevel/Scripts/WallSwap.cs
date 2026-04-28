using UnityEngine;

public class WallSwap : MonoBehaviour
{
    public Material[] materials;
    public Material NightmareMat;
    public float delay = 1.0f;
    public NighmarManager nightmar;

    private MeshRenderer walrend;






    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        walrend = GetComponent<MeshRenderer>();
        InvokeRepeating("SwapMaterial", delay, delay);
    }

    // Update is called once per frame
    void SwapMaterial()
    {
        if (nightmar.isNighmarActive == false)
        {
            if (materials.Length > 0)
            {
                int index = Random.Range(0, materials.Length);
                walrend.material = materials[index];
            }
            else
            {
                Debug.LogWarning("Error");
            }
        }
        else
        {
            walrend.material = NightmareMat;
        }





    }
}
