using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleButton : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private Animator _animator;
    private string _isMouseOver = "IsMouseOver";

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnMouseOver()
    {
        Debug.Log("Particle ON");
        _animator.SetBool(_isMouseOver, true);
        _particleSystem.Play();
    }

    private void OnMouseExit()
    {
        _animator.SetBool(_isMouseOver, false);
        _particleSystem.Stop();        
        Debug.Log("Particle OFF");
    }
}
