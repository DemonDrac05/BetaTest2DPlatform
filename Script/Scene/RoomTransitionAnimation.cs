using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransitionAnimation : MonoBehaviour
{
    public Animator transition;

    public float transitionTime;

    public RoomTransfer room;

    [field: SerializeField] public float transitionDuration = 1f;

    private void Start()
    {
        transitionTime = 0f;

        room = FindObjectOfType<RoomTransfer>();
    }
    void Update()
    {
        if (room.transferred && transitionTime <= 0f)
        {
            transitionTime = transitionDuration;
        }

        TransitionAniamtion();
    }
    void TransitionAniamtion()
    {
        if (transitionTime > 0f)
        {
            transitionTime -= Time.deltaTime;
            transition.Play("Fade_End");
        }

        else if (transitionTime <= 0f)
        {
            transitionTime = 0f;
            transition.Play("Fade_Start");
        }
    }
}
