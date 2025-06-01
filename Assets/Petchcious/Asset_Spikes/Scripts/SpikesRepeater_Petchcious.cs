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
        public float interval = 2f;
        public float activeDuration = 0.5f;

        public float spikeUpDelay = 0.3f;
        public float spikeDownDelay = 0.3f;
        private Animator animator;
        private bool isActive = false;
        private Collider2D spikeCollider;

        void Start()
        {
            animator = GetComponent<Animator>();
            spikeCollider = GetComponent<Collider2D>();

            if (!onlyUpSpike)
            {
                InvokeRepeating("ActivateSpike", 1f, interval);
            }
            else
            {
                isActive = true;
                animator.Play("SpikeIdleUp");
            }

            if (disableSpikeHitbox)
                isActive = false;
        }

        void ActivateSpike()
        {
            animator.Play("SpikeUp");
            isActive = true;

            if (!disableSpikeHitbox)
            {
                Collider2D[] hits = Physics2D.OverlapBoxAll(spikeCollider.bounds.center, spikeCollider.bounds.size, 0f);
                foreach (Collider2D hit in hits)
                {
                    if (hit.CompareTag("Player"))
                    {
                        PlayerCollapseSpike();
                    }
                }
            }

            // ค้างไว้หลังแทงขึ้น
            Invoke("SpikeIdleUp", spikeUpDelay); 
            Invoke("DeactivateSpike", activeDuration);
        }

        void SpikeIdleUp()
        {
            animator.Play("SpikeIdleUp");
        }

        void DeactivateSpike()
        {
            animator.Play("SpikeDown");
            isActive = false;
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


