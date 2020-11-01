using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceAnimation : MonoBehaviour
{
    public AnimationCurve vertical;
    public AnimationCurve horizontal;

    private SoundManager soundManagerInstance;

    private void Start()
    {
        soundManagerInstance = SoundManager.Instance;
    }

    void Update()
    {
        float currentTime = soundManagerInstance.beatAS.time;
        float audioLength = soundManagerInstance.beatAS.clip.length;

        float normalized = currentTime / audioLength;
        float lerpValue = Mathf.Lerp(normalized, 1, normalized);

        gameObject.transform.localScale = new Vector3(horizontal.Evaluate(lerpValue), vertical.Evaluate(lerpValue), 1);
    }
}
