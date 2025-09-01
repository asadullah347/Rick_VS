using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarImage;
    [SerializeField] private float reduceSpeed = 2;
    private float _target = 1;
    private Camera _cam;

    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        _healthBarImage.fillAmount = Mathf.MoveTowards(_healthBarImage.fillAmount, _target, reduceSpeed * Time.deltaTime);
    }

    public void UpdateHealthBar(float maxHealth, float currHealth)
    {
        _target = currHealth / maxHealth;
    }
}
