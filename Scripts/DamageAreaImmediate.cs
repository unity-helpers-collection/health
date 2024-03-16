using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DamageAreaImmediate : MonoBehaviour
{
    private Collider cl;

    [SerializeField]
    public float damageAmount;

    [ContextMenu("Damage")]
    public void Damage()
    {
        //Debug.Log("Damage");
        StartCoroutine(DamageCoroutine());
    }

    private IEnumerator DamageCoroutine()
    {
        cl.enabled = true;
        yield return new WaitForFixedUpdate();
        yield return new WaitForFixedUpdate();
        cl.enabled = false;
    }

    void Start()
    {
        cl = GetComponent<Collider>();
        cl.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Damage Trigger");

        if (!other.TryGetComponent(out Health health)) return;

        health.Damage(new Health.DamageStruct() {damageAmount = damageAmount, pushBackVector = transform.forward});
    }
}
