using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    public float velocity = 0f;
    public AnimationCurve rotation;

    private SoundManager soundManagerInstance;
    //private float normalizedStep = 0;

    private void Start()
    {
        soundManagerInstance = SoundManager.Instance;
    }

    void Update()
    {
        Debug.Log(soundManagerInstance.beat.time);
        float currentTime = soundManagerInstance.beat.time;
        float audioLength = soundManagerInstance.beat.clip.length;

        float normalized = currentTime / audioLength;
        float lerpValue = Mathf.Lerp(normalized, 1, normalized);

        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, rotation.Evaluate(lerpValue));

        //Movimiento con aceleraciones
        /*if (normalizedStep > 0.95)
        {
            normalizedStep = 0;
        }
        normalizedStep = Mathf.Lerp(normalizedStep, 1, Time.fixedDeltaTime * velocity);
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, rotation.Evaluate(normalizedStep));*/
    }
}
