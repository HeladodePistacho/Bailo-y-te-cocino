using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAnimat : MonoBehaviour
{
    Animator anim;
    bool change = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(SwapFrame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SwapFrame()
    {
        while(true)
        {
            yield return new WaitForSeconds(SoundManager.Instance.beat.clip.length);
            anim.SetBool("Swap", change);
            change = !change;
        }
    }
}
