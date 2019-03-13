using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    [SerializeField]
    protected ParticleSystem explosionParticles;
    [SerializeField]
    protected AudioSource explosionAudio;

    [SerializeField]
    protected float maxDamage = 100f;
    [SerializeField]
    protected float maxLifeTime = 5f;
}
