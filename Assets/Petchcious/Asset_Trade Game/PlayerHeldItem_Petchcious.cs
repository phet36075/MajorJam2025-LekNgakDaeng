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
        [SerializeField] LayerMask layerMask;

        private void Update()
        {
            // ใช้ OverlapCircle หรือฟังก์ชันที่คุณต้องการเพื่อตรวจจับวัตถุใน LayerMask
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.1f, layerMask);

            foreach (var hitCollider in hitColliders)
            {
                // เช็คว่า object ที่ถูกชนอยู่ใน LayerMask ที่ต้องการ
                if (hitCollider != null)
                {
                    heldItem = hitCollider.gameObject.name;
                    heldItemObj = Instantiate(hitCollider.gameObject, holdPoint.position, Quaternion.identity);
                    heldItemObj.transform.SetParent(holdPoint);
                    Destroy(hitCollider.gameObject);
                }
            }
        }

        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     if (Physics2D.OverlapCircle(transform.position, 0.1f, layerMask))
        //     {
        //         heldItem = other.gameObject.name;
        //         heldItemObj = Instantiate(other.gameObject, holdPoint.position, Quaternion.identity);
        //         heldItemObj.transform.SetParent(holdPoint);
        //         Destroy(other.gameObject);
        //     }
        // }
        
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
                    5 * Time.deltaTime * 0.2f
                );
                yield return null;
            }
            
            Destroy(heldItemObj);
        }
    }

}

