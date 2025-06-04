using UnityEngine;

enum DoorOccupants
{
    Enemy,Goal
}
public class ToiletDoor : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private DoorOccupants doorOccupant;

    [Header("Components")]
    [SerializeField] private GameObject DoorObject;
    [SerializeField] private GameObject EnemySprite;
    [SerializeField] private GameObject GoalSprite;

    private bool IsActivated;
    void Start()
    {
        switch (doorOccupant)
        {
            case DoorOccupants.Enemy: EnemySprite.SetActive(true); GoalSprite.SetActive(false); break;
            case DoorOccupants.Goal:  GoalSprite.SetActive(true); EnemySprite.SetActive(false); break;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (!IsActivated)
                {
                    OnOpened();
                }
               
            }
        }
    }

    void OnOpened()
    {
        IsActivated = true;
        DoorObject.SetActive(false);
        switch (doorOccupant)
        {
            case DoorOccupants.Enemy: Debug.Log("You Found Enemy!");  break; //Lose Function Here
            case DoorOccupants.Goal: Debug.Log("You Found Banana!"); break;  //Win Function Here
        }
    }
}
