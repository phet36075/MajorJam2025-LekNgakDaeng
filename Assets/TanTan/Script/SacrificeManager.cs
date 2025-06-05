using System.Collections;
using UnityEngine;

public class SacrificeManager : MonoBehaviour
{
    public float killAmount = 0;
    [SerializeField] GameObject goalPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Animator animator;
    bool isSpawned = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        AnimationRun();
        if(killAmount >= 3 && !isSpawned)
        {
            StartCoroutine(Spawn());
            isSpawned = true;
        }
    }


    void AnimationRun()
    {
        animator.SetInteger("enemyKilled", (int)killAmount);
    }

    IEnumerator Spawn()
    {
        animator.SetBool("isComplete", true);
        yield return new WaitForSeconds(1f);
        Instantiate(goalPrefab, spawnPoint.position, Quaternion.identity);
    }
}
