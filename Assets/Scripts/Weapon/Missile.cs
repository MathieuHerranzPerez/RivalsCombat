using UnityEngine;

public class Missile : Bullet
{
    [SerializeField]
    private float explosionForce = 50f;
    [SerializeField]
    private float explosionRadius = 100f;

    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody targetRigidBody = collider.GetComponent<Rigidbody>();
            if (targetRigidBody)
            {
                targetRigidBody.AddExplosionForce(explosionForce, transform.position, explosionRadius);
                Cube target = targetRigidBody.GetComponent<Cube>();
                if (target)
                {
                    float damageAmount = CalculateDamage(targetRigidBody.position);
                    target.TakeDamageFormBullet(damageAmount, this);
                }
            }
        }

        explosionParticles.transform.parent = null;
        explosionParticles.Play();
        explosionAudio.Play();

        Destroy(explosionParticles.gameObject, explosionParticles.main.duration);
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
}
