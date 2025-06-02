using System;
using System.Collections;
using UnityEngine;

namespace Petchcious.Trade_Game
{
    
    
    public class PlayerHeldItem_Petchcious : MonoBehaviour
    {
        public string heldItem = null; 
        public Transform holdPoint;
        private GameObject heldItemObj;
        public Transform  enemy;

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
                StartCoroutine(MoveTowardsTarget());
                
                //Destroy(heldItemObj);
                
               // heldItemObj = null;
               // heldItem = null;
            }
        }
        private IEnumerator MoveTowardsTarget()
        {
            while (Vector3.Distance(heldItemObj.transform.position, enemy.position) > 0.01f)
            {
                heldItemObj.transform.position = Vector3.MoveTowards(
                    heldItemObj.transform.position,
                    enemy.position,
                    5 * Time.deltaTime
                );
                yield return null;
            }
            
            Destroy(heldItemObj);
        }
    }

}

