using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BarrierScript : MonoBehaviour
{
    [SerializeField] private Animation _animation => GetComponent<Animation>();
    [SerializeField] private AnimationClip[] _animationClip;

    private Random rand = new Random();

    private void Awake()
    {
        for (int i = 0; i < _animationClip.Length; i++)
        {
            _animation.clip = _animationClip[rand.Next(0, _animationClip.Length)];
            _animation.Play();
            break;
        }
    }
}
