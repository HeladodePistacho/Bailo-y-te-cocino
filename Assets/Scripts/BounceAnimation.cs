using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceAnimation : MonoBehaviour
{
    public float velocity = 0f;
    public AnimationCurve vertical;
    public AnimationCurve horizontal;

    private float normalizedStep = 1;

    void Start()
    {
    }

    void Update()
    {
        normalizedStep = Mathf.Lerp(normalizedStep, 0, Time.fixedDeltaTime * velocity);
        MoveHorizontal();
        MoveVertical();
    }

    private void MoveHorizontal()
    {
        gameObject.transform.localScale = new Vector3(horizontal.Evaluate(normalizedStep), gameObject.transform.localScale.y, 1);
    }

    private void MoveVertical()
    {
        gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x, vertical.Evaluate(normalizedStep), 1);
    }
}
