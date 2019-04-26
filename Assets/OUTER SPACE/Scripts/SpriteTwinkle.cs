using UnityEngine;

public class SpriteTwinkle : MonoBehaviour
{
    //-- Use this script to randomize the start of an animation's playback.

    public float maxOffset = 0.0f;                              // Max offset for the playback

    private Animator twinkler;                                  // It's an Animator, ass.

    public void JumpStart()
    {
        twinkler = GetComponent<Animator>();

        if(maxOffset > 0)
        {
            twinkler.Play("Idle", 0, Random.Range(0.0f, maxOffset));
        }
    }
}