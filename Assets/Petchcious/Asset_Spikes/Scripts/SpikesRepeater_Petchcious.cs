using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using NavMeshPlus.Components;
namespace Petchcious.Spikes
{
    public class SpikesRepeater_Petchcious : MonoBehaviour
    {
        [SerializeField] AudioClip spikeUpSound;   // เสียงหนามขึ้น
        [SerializeField] AudioClip spikeDownSound; // เสียงหนามลง
        private bool hasPlayedSpikeUpSound = false;
        private bool hasPlayedSpikeDownSound = false;
        private Coroutine spikeUpCoroutine;
        private Coroutine spikeDownCoroutine;

        private WinLoseManager winLoseManager => FindAnyObjectByType<WinLoseManager>();
        
        lv12_startSetup _lv12StartSetup => FindAnyObjectByType<lv12_startSetup>();
        private NavMeshModifier _navMeshModifier;
        [SerializeField] NavMeshSurface[] navMesh;
        /// <summary>
        /// Player Should Be Rigidbody2D and Have Box Collider2D '^'
        /// </summary>
       public bool onlyUpSpike = false;
        public bool disableSpikeHitbox = false;
        public bool disableAutomaticPierce = false;
        public bool displayBgAfterHit;
        public float interval = 2f;
        public float activeDuration = 0.5f;
      
        public float spikeUpDelay = 0.3f;
        public float spikeDownDelay = 0.3f;

      //  public SpriteRenderer bgForDisplay;
        private Animator animator;
        private bool isActive = false;
        private bool isHitPlayer= false;
        private Collider2D spikeCollider;

        public bool startingAsSpikeUp = false;
        [Header("Reference")]
        [SerializeField] LayerMask playerMask;
        void Start()
        {
            _navMeshModifier = GetComponent<NavMeshModifier>();
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
            if (isActive)
            {
                if (Physics2D.OverlapCircle(transform.position, 0.1f, playerMask))
                {
                    if(isHitPlayer)return;
                    else
                    {
                        PlayerCollapseSpike();
                    }
                    
                  
                }
            }
          
        }

        void OnPlayerMove()
        {
            StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            if (disableAutomaticPierce)
            {
                if (isActive)
                {
                    DeactivateSpike();
                }
                else
                {
                    yield return new WaitForSeconds(0.3f);
                    ActivateSpike();
                   
                }
            }
        }
        public void ActivateSpike()
        {
            if (navMesh != null && _navMeshModifier != null)
            {
                _navMeshModifier.area = 1;
                BakeNewNav();
            }

            animator.Play("SpikeUp");

            // เล่นเสียงหนามขึ้นและปล่อยให้เสียงเล่นต่อจนจบ
            if (spikeUpCoroutine == null)
            {
                spikeUpCoroutine = StartCoroutine(PlaySpikeSoundContinuously(spikeUpSound));
            }

            Invoke("SpikeIdleUp", spikeUpDelay);
            if (!disableAutomaticPierce)
                Invoke("DeactivateSpike", activeDuration);
        }

        void SpikeIdleUp()
        {
            isActive = true;
            animator.Play("SpikeIdleUp");
        }

        public void DeactivateSpike()
        {
            if (navMesh != null && _navMeshModifier != null)
            {
                _navMeshModifier.area = 0;
                BakeNewNav();
            }

            animator.Play("SpikeDown");

            // เล่นเสียงหนามลงและปล่อยให้เสียงเล่นต่อจนจบ
            if (spikeDownCoroutine == null)
            {
                spikeDownCoroutine = StartCoroutine(PlaySpikeSoundContinuously(spikeDownSound));
            }

            isActive = false;
            if (!disableAutomaticPierce)
                Invoke("SpikeIdleDown", spikeDownDelay);
        }

        void SpikeIdleDown()
        {
            animator.Play("SpikeIdleDown");
        }

        void PlaySpikeSound(AudioClip sound)
        {
            SoundFXManager.instance.PlaySoundFXClip(sound);
        }
        IEnumerator PlaySpikeSoundContinuously(AudioClip sound)
        {
            // เล่นเสียง
            SoundFXManager.instance.PlaySoundFXClip(sound);

            // รอจนกว่าเสียงจะเล่นจบ
            yield return new WaitForSeconds(sound.length);

            // เมื่อเสียงเล่นจบแล้ว ก็ปล่อยให้ Coroutine จบ
            if (sound == spikeUpSound)
            {
                spikeUpCoroutine = null;
            }
            else if (sound == spikeDownSound)
            {
                spikeDownCoroutine = null;
            }
        }
        public void ResetSpikeSounds() 
        {
            // รีเซ็ตสถานะเสียงเมื่อหนามถูกรีเซ็ตหรือเริ่มใหม่
            spikeUpCoroutine = null;
            spikeDownCoroutine = null;
        }

        public void PlayerCollapseSpike()
        {
            isHitPlayer = true;
            if (displayBgAfterHit)
            {
                     if(_lv12StartSetup!=null) 
                         _lv12StartSetup.SetSortingOrder(-6);
            }
           
            Debug.Log("Player step on a spike!");
            //Game Over
            winLoseManager.OnLose();
        }

        public void BakeNewNav()
        {
            navMesh = FindObjectsByType<NavMeshSurface>(FindObjectsSortMode.None);

            foreach (NavMeshSurface surface in navMesh)
            {
                surface.BuildNavMesh();
            }
        }
    }
}


