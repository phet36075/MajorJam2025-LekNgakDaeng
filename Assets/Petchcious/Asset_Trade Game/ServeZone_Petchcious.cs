using System.Collections;
using UnityEngine;

namespace Petchcious.Trade_Game
{
    public class ServeZone_Petchcious : MonoBehaviour
    {
        private WinLoseManager _winLoseManager => FindAnyObjectByType<WinLoseManager>();
        public TradeManager_Petchcious tradeManager;
        [SerializeField] LayerMask layerMask;

        public GameObject bananaGoal;

        public Transform tablePos;

        public Transform enemy;

        private bool isWin;
        // private void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (other.CompareTag("Player"))
        //     {
        //         PlayerHeldItem_Petchcious player = other.GetComponent<PlayerHeldItem_Petchcious>();
        //         if (player.heldItem != null)
        //         {
        //             if (player.heldItem == tradeManager.currentItem)
        //             {
        //                 player.DropItem(); 
        //                TradeGame_Win();
        //             }
        //             else
        //             {
        //                 player.DropItem(); 
        //                 TradeGame_Lose();
        //             }
        //         }
        //     }
        // }
        void Update()
        {
            // ตรวจสอบว่าวัตถุที่อยู่ใน LayerMask ในรัศมีที่ต้องการ
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, layerMask);

            foreach (var hitCollider in hitColliders)
            {
                // ตรวจสอบว่า Collider2D ที่เจอเป็น "Player"
               
                    PlayerHeldItem_Petchcious player = hitCollider.GetComponent<PlayerHeldItem_Petchcious>();
            
                    if (player != null && player.heldItem != null)
                    {
                        if (player.heldItem == tradeManager.currentItem)
                        {
                            player.DropItem();
                            if(isWin) return;
                            StartCoroutine(MoveTowardsTarget());
                          //  TradeGame_Win();
                        }
                        else
                        {
                            player.DropItem(); 
                            TradeGame_Lose();
                        }
                    }
                
            }
        }
        public void TradeGame_Win()
        {
          
            _winLoseManager.OnWin();
            Debug.Log("Trade Game Win!!");
        }

        public void TradeGame_Lose()
        {
            _winLoseManager.OnLose();
            Debug.Log("Trade Game Lose!!");
        }

        private IEnumerator MoveTowardsTarget()
        {
            isWin = true;
            yield return new WaitForSeconds(0.5f);
            
            bananaGoal.SetActive(true);
            while (Vector3.Distance(bananaGoal.transform.position, tablePos.position) > 0.01f)
            {
                bananaGoal.transform.position = Vector3.MoveTowards(
                    bananaGoal.transform.position,
                    tablePos.position,
                    5 * Time.deltaTime
                );
                yield return null;
            }

           
            TradeGame_Win();
        }
    }
    
    
}

