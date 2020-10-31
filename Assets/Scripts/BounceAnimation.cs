using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    public float velocity = 0f;
    public AnimationCurve vertical;
    public AnimationCurve horizontal;

    private float normalizedStep = 0;
    private IEnumerator myCoroutine;

    private void Start()
    {
        myCoroutine = StartSquash();
    
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            StopCoroutine(myCoroutine);
            normalizedStep = 0;
            StartCoroutine(myCoroutine);
        }
    }

    private void Squash()
    {
        gameObject.transform.localScale = new Vector3(horizontal.Evaluate(normalizedStep), vertical.Evaluate(normalizedStep), 1);
    }

    IEnumerator StartSquash()
    {
        while(normalizedStep >= 0)
        {
            normalizedStep = Mathf.Lerp(normalizedStep, 1, Time.fixedDeltaTime * velocity); //Time.deltaTime * velocity;
            Squash();
            yield return null;
        }
    }
}
