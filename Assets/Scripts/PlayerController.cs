using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _deadHealth = 0f;
    [SerializeField] private float _maxHealth = 100f;

    private float _damage = 10f;
    private float _heal = 10f;
    private float _currentHealth;
    private float _jumpForce = 350f;
    private ParticleSystem _particleSystem;
    private Rigidbody2D _rigidbody2D;

    public event UnityAction HealthChanged;
    public event UnityAction Healed;
    public event UnityAction Damaged;

    public float Health => _currentHealth;
    public float Damage => _damage;
    public float HealValue => _heal;
    public float MaxHealth => _maxHealth;
    public float DeadHealth => _deadHealth;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _currentHealth = _maxHealth;
    }

    private void OnMouseDown()
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce);
    }

    public void TakeDamage()
    {
        var particleColor = _particleSystem.colorOverLifetime;
        particleColor.color = new ParticleSystem.MinMaxGradient(Color.white, Color.red);
        _particleSystem.Play();
        _currentHealth -= _damage;

        if (_currentHealth < 0)
        {
            _currentHealth = 0;
        }

        Damaged?.Invoke();
    }

    [System.Obsolete]
    public void Heal()
    {
        var particleColor = _particleSystem.colorOverLifetime;
        particleColor.color = new ParticleSystem.MinMaxGradient(Color.white, Color.green);
        _particleSystem.Play();
        _currentHealth += _heal;

        if (_currentHealth > 100)
        {
            _currentHealth = 100;
        }

        Healed?.Invoke();
    }
}
