using System;
using System.Collections;
using UnityEditor.Build;
using UnityEngine;

namespace Petchcious.Spikes
{
    public class SpikesRepeater_Petchcious : MonoBehaviour
    {
        /// <summary>
        /// Player Should Be Rigidbody2D and Have Box Collider2D '^'
        /// </summary>
       public bool onlyUpSpike = false;
        public bool disableSpikeHitbox = false;
        public bool disableAutomaticPierce = false;
        public float interval = 2f;
        public float activeDuration = 0.5f;
      
        public float spikeUpDelay = 0.3f;
        public float spikeDownDelay = 0.3f;
        private Animator animator;
        private bool isActive = false;
        private Collider2D spikeCollider;

        public bool startingAsSpikeUp = false;
        [Header("Reference")]
        [SerializeField] LayerMask playerMask;
        void Start()
        {
            OnPlayerMoveSubscription.Instance.OnPlayerMove += this.OnPlayerMove;
            animator = GetComponent<Animator>();
            spikeCollider = GetComponent<Collider2D>();

            if (!onlyUpSpike && !disableAutomaticPierce)
            {
                InvokeRepeating("ActivateSpike", 1f, interval);
            }
            if(onlyUpSpike)
            {
                isActive = true;
                animator.Play("SpikeIdleUp");
            }

            if (startingAsSpikeUp)
            {
                ActivateSpike();
                
            }

            if (disableSpikeHitbox)
                isActive = false;
        }

        private void Update()
        {
            if (Physics2D.OverlapCircle(transform.position, 0.1f, playerMask))
            {
                PlayerCollapseSpike();
            }
        }

        void OnPlayerMove()
        {
            StartCoroutine(Delay());
           
           
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(0.3f);
            if (disableAutomaticPierce)
            {
                if (isActive)
                {
                    DeactivateSpike();
                }
                else
                {
                    ActivateSpike();
                }
            }
        }
       public void ActivateSpike()
        {
            animator.Play("SpikeUp");
            isActive = true;

            if (!disableSpikeHitbox)
            {
                
                     
            }

            
            Invoke("SpikeIdleUp", spikeUpDelay); 
            if(!disableAutomaticPierce)
            Invoke("DeactivateSpike", activeDuration);
        }

        void SpikeIdleUp()
        {
            animator.Play("SpikeIdleUp");
        }

      public void DeactivateSpike()
        {
            animator.Play("SpikeDown");
            isActive = false;
            if(!disableAutomaticPierce)
            Invoke("SpikeIdleDown", spikeDownDelay); 
        }

        void SpikeIdleDown()
        {
            animator.Play("SpikeIdleDown");
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isActive && collision.CompareTag("Player") && !disableSpikeHitbox)
            {
                PlayerCollapseSpike();
            }
        }

        public void PlayerCollapseSpike()
        {
            Debug.Log("Player step on a spike!");
        }
    }
}


