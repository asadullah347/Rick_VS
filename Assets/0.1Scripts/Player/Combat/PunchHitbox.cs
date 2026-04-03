using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PunchHitbox : MonoBehaviour
{
    [SerializeField] private float damage = 1f;

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
        Debug.Log("HIT SOMETHING: " + other.name); 

        if (!isActive) return;

        if (hitTargets.Contains(other)) return;

        if (other.TryGetComponent<IDamageable>(out var dmg))
        {
            Debug.Log("DAMAGE APPLIED"); 
            hitTargets.Add(other);
            dmg.TakeDamage(damage);
        }
    }
}