using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrier : MonoBehaviour
{
//take animation components 
    [SerializeField] private Animation _animation;
    [SerializeField] private AnimationClip[] _animationClip;

    private void Awake()
    {
        //we are looking for a objects that have a game controllers components
        GameController gameController = FindObjectOfType<GameController>();
        _animation = GetComponent<Animation>();
//check the array out of the frame . if the index control is greatr than the volume resent and accelerate a speed
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
