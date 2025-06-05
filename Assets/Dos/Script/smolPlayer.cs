using System.Collections;
using UnityEngine;

public class smolPlayer : MonoBehaviour
{
    public static smolPlayer instance;
    public float speed;
    public Rigidbody2D rb;
    public Vector2 moveDir;
    public Animator kaboomAnim;
    public AudioClip kaboomSound;
    public AudioClip walkSound;

    private bool isDead;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(instance == null)
            instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
            rb.linearVelocity = moveDir * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            isDead = true;
            rb.linearVelocity = Vector2.zero;
            kaboomAnim.gameObject.SetActive(true);
            kaboomAnim.Play("Kaboom");
            SoundFXManager.instance.PlaySoundFXClip(kaboomSound);
            StartCoroutine(waitAndPlayWalkSound());
            StartCoroutine(waitForBlueboiToShow());
            
        }
        if (collision.CompareTag("Finish"))
        {
            WinLoseManager win = FindAnyObjectByType<WinLoseManager>();
            if (win != null)
            {
                win.OnWin();
            }
        }
    }
    IEnumerator waitForBlueboiToShow()
    {
        yield return new WaitForSeconds(1.5f);
        Rigidbody2D rbs = GameObject.Find("BlueBoi").GetComponent<Rigidbody2D>();
        rbs.linearVelocityY = 2;
    }
    
    IEnumerator waitAndPlayWalkSound()
    {
        yield return new WaitForSeconds(1.5f);
        SoundFXManager.instance.PlaySoundFXClipAndDestroy(walkSound,4);
    }
}
