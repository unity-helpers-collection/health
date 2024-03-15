using DG.Tweening;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public class AppliedDamageStruct
    {
        public float damageAmount;
        public Vector3 pushBackVector;
        public Health appliedTo;

        public AppliedDamageStruct(DamageStruct damage, Health appliedTo)
        {
            damageAmount = damage.damageAmount;
            pushBackVector = damage.pushBackVector;
            this.appliedTo = appliedTo;
        }
    }

    public class DamageStruct
    {
        public float damageAmount;
        public Vector3 pushBackVector;

        public DamageStruct()
        {
            damageAmount = 0;
            pushBackVector = Vector3.zero;
        }
    }

    [CanBeNull]
    private Rigidbody _rigidbody;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private float _maximumHealth;

    [SerializeField]
    private float _pushBackMultiplier = 0;

    [SerializeField]
    private UnityEvent<AppliedDamageStruct> _onDamage;


    public float CurrentHealth
    {
        get => _currentHealth;
        private set => _currentHealth = value;
    }

    public float MaximumHealth
    {
        get => _maximumHealth;
        private set => _maximumHealth = value;
    }

    public bool IsDead => CurrentHealth <= 0;

    public float HealthProportion => CurrentHealth / MaximumHealth;

    public void Damage(DamageStruct damage)
    {
        CurrentHealth -= damage.damageAmount;
        Debug.Log($"pushing {damage.pushBackVector}");
        ApplyPushBack(damage.pushBackVector);
        _onDamage?.Invoke(new AppliedDamageStruct(damage, this));
    }

    public void ApplyPushBack(Vector3 pushBackVector)
    {
        var scaledPushBack = pushBackVector * _pushBackMultiplier;
        transform.DOMove(transform.position + scaledPushBack, 0.5f).SetEase(Ease.OutCubic);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
