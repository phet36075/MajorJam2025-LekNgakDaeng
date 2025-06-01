using System;
using UnityEngine;

namespace Petchcious.Trade_Game
{
    public class PlayerHeldItem_Petchcious : MonoBehaviour
    {
        public string heldItem = null; 
        public Transform holdPoint;
        private GameObject heldItemObj;
        

        void OnTriggerEnter2D(Collider2D other)
        {
            if (heldItemObj == null && other.CompareTag("TradeItem"))
            {
                heldItem = other.gameObject.name;
                heldItemObj = Instantiate(other.gameObject, holdPoint.position, Quaternion.identity);
                heldItemObj.transform.SetParent(holdPoint);
                Destroy(other.gameObject);
            }
        }
        
        public void DropItem()
        {
            if (heldItemObj != null)
            {
                Destroy(heldItemObj);
                heldItemObj = null;
                heldItem = null;
            }
        }
    }

}

