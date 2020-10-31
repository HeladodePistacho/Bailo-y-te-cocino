using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    public float velocity = 0f;
    public AnimationCurve rotation;

    private float normalizedStep = 0;

    void Update()
    {
        /*if(normalizedStep >= 1)
        {
            normalizedStep = 0;
        }
        normalizedStep += Time.fixedDeltaTime * velocity;
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, rotation.Evaluate(normalizedStep));*/


        if (normalizedStep > 0.95)
        {
            normalizedStep = 0;
        }
        normalizedStep = Mathf.Lerp(normalizedStep, 1, Time.fixedDeltaTime * velocity);
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, rotation.Evaluate(normalizedStep));
    }
}
