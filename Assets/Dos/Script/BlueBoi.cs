using UnityEngine;

public class BlueBoi : MonoBehaviour
{
    public Animator slapBroAnim;
    public AudioClip slapSound;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            rb.linearVelocityY = 0;
            SoundFXManager.instance.PlaySoundFXClip(slapSound);
        }
    }
}
