using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageAreaTrigger : MonoBehaviour
{
    private Collider cl;

    [SerializeField]
    public float damageAmount;

    void Start()
    {
        cl = GetComponent<Collider>();
        cl.enabled = false;
    }

    public void Activate()
    {
        cl.enabled = true;
    }

    public void Deactivate()
    {
        cl.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out Health health)) return;

        health.Damage(new Health.DamageStruct() {damageAmount = damageAmount, pushBackVector = transform.forward});
    }
}
