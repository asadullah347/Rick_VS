using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PunchHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 1f;
    [SerializeField] private float knockbackForce = 4f;

    public event Action<Collider> OnSuccessfulHit;

    private Collider col;
    private bool isActive;
    private HashSet<Collider> hitTargets = new HashSet<Collider>();

    private void Awake()
    {
        col = GetComponent<Collider>();
        col.isTrigger = true;
        col.enabled = false;
    }

    public void Activate()
    {
        hitTargets.Clear();
        isActive = true;
        col.enabled = true;
    }

    public void Deactivate()
    {
        isActive = false;
        col.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isActive) return;
        if (hitTargets.Contains(other)) return;

        if (other.TryGetComponent<IDamageable>(out var dmg))
        {
            hitTargets.Add(other);

            Vector3 hitDir = (other.transform.position - transform.position).normalized;
            dmg.TakeDamage(damage, hitDir, knockbackForce);

            OnSuccessfulHit?.Invoke(other);
        }
    }
}