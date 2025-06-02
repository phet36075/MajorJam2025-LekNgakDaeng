using UnityEngine;

namespace Petchcious.Trade_Game
{
    public class ServeZone_Petchcious : MonoBehaviour
    {
        public TradeManager_Petchcious tradeManager;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayerHeldItem_Petchcious player = other.GetComponent<PlayerHeldItem_Petchcious>();
                if (player.heldItem != null)
                {
                    if (player.heldItem == tradeManager.currentItem)
                    {
                        player.DropItem(); 
                       TradeGame_Win();
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
            Debug.Log("Trade Game Win!!");
        }

        public void TradeGame_Lose()
        {
            Debug.Log("Trade Game Lose!!");
        }
        
        
    }
}

