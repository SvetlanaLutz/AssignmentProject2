using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    [SerializeField] private AnimationClip[] _animationClip;

    private void Awake()
    {
        GameController gameController = FindObjectOfType<GameController>();
        _animation = GetComponent<Animation>();

        if (_animationClip.Length > gameController.IndexColors)
        {
            PlayAnimation(gameController.IndexColors, gameController.SpeedColors);
        }
        else
        {
            GameController.isZero = true;
        }
    }

    public void PlayAnimation(int index, float speed)
    {
        _animation.clip = _animationClip[index];
        _animation[_animationClip[index].name].speed = speed;
        _animation.Play();
    }
}
