using UnityEngine;

public class lv12_startSetup : MonoBehaviour
{
    public SpriteRenderer bg;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(bg !=null)
        bg.sortingOrder = -3;
    }


    public void SetSortingOrder(int order)
    {
        bg.sortingOrder = order;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
