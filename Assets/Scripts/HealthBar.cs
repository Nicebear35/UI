using System.Collections;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private PlayerController _player;

    [SerializeField] private Image _greenBar;
    [SerializeField] private Image _redBar;
    [SerializeField] private float _speed = 0.3f;

    private Coroutine _currentCoroutine;
    private float NormalizedHealth => _player.Health / _player.MaxHealth;

    private void Start()
    {
        _greenBar.fillAmount = NormalizedHealth;
        _redBar.fillAmount = NormalizedHealth;
    }

    private void OnEnable()
    {
        _player.Healed += OnHealed;
        _player.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        _player.Healed -= OnHealed;
        _player.Damaged -= OnDamaged;
    }

    private void OnDamaged()
    {
        OnHealthChanged(_redBar, _greenBar);
    }

    private void OnHealed()
    {
        OnHealthChanged(_greenBar, _redBar);
    }

    private void OnHealthChanged(Image slowBar, Image fastBar)
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
        }

        fastBar.fillAmount = NormalizedHealth;
        _currentCoroutine = StartCoroutine(ChangeBarValue(slowBar));
    }

    private IEnumerator ChangeBarValue(Image slowBar)
    {
        while (slowBar.fillAmount != NormalizedHealth)
        {
            slowBar.fillAmount = Mathf.MoveTowards(slowBar.fillAmount, NormalizedHealth, _speed * Time.deltaTime);
            yield return null;
        }
    }
}