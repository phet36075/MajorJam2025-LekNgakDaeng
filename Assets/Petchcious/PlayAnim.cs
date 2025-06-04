using UnityEngine;
using TMPro;
public class PlayAnim : MonoBehaviour
{
    private Animator _animator;

    public TextMeshProUGUI winLosetext;
   // public string animName;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PlayAnimName(string animName)
    {
        _animator.Play(animName);
    }

    public void SetText(string text)
    {
        winLosetext.text = text;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
