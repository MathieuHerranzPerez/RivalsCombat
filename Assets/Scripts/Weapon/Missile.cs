using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Missile : Bullet
{
    [SerializeField]
    private float explosionForce = 10f;
    [SerializeField]
    private float explosionRadius = 5f;
    [SerializeField]
    private GameObject explosionEffectPrefab;

    [Header("Camera shake")]
    [SerializeField]
    private float camShakeMagnitude = 0.1f;
    [SerializeField]
    private float camShakeRotation = 0.1f;
    [SerializeField]
    private float camShakeTime = 0.2f;

    // ---- INTERN ----
    private Rigidbody rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        Destroy(gameObject, maxLifeTime);
    }

    void Update()
    {
        // rotate the missile to follow its gravity curve
        float angle = Mathf.Atan2(rBody.velocity.y, rBody.velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void OnTriggerEnter(Collider other)
    {
        Explode();
    }

    private void Explode() 
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
            if (targetRigidBody)
            {
                Cube target = targetRigidBody.GetComponent<Cube>();
                if (target)
                {
                    float damageAmount = CalculateDamage(targetRigidBody.position);
                    target.TakeDamageFormBullet(damageAmount);
                }
            }
        }

        Collider[] collidersToAddForce = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in collidersToAddForce)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
            if (targetRigidBody)
            { 
                targetRigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
            

        // effect
        if(explosionEffectPrefab)
        {
            GameObject explosionGO = Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
            Destroy(explosionGO, 1.5f);
        }
        CameraShake.Instance.Shake(camShakeTime, camShakeMagnitude, camShakeRotation);

        Destroy(gameObject);
    }

    private float CalculateDamage(Vector3 targetPosition)
    {
        // Calculate the amount of damage a target should take based on it's position.
        Vector3 explosionToTarget = targetPosition - transform.position;
        float explosionDistance = explosionToTarget.magnitude;
        float relativeDistance = (explosionRadius - explosionDistance) / explosionRadius;
        float damage = relativeDistance * maxDamage;
        damage = Mathf.Max(0f, damage);

        return damage;
    }

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
