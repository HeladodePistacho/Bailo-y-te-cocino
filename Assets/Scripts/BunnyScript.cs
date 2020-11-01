using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ANIM_STATE
{
    IDLE,
    OPEN_MOUTH,
    ÑOM
}

public class BunnyScript : MonoBehaviour
{
    Animator anim;
    ANIM_STATE state = ANIM_STATE.IDLE;
    int index = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(Beat());
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = ANIM_STATE.OPEN_MOUTH;
            anim.SetBool("Idle", false);
            index = 0;
        }
    }

    IEnumerator Beat()
    {
        while(true)
        {
            anim.SetInteger("Index", index);
            ++index;

            ResetIndex();


            yield return new WaitForSeconds(0.4285625f);
        }
    }

    void ResetIndex()
    {
        switch(state)
        {
            case ANIM_STATE.IDLE:
            case ANIM_STATE.ÑOM:
                if (index > 3)
                    index = 0;
                break;

            case ANIM_STATE.OPEN_MOUTH:
                if (index > 4)
                    index = 0;
                break;
        }
    }
}
