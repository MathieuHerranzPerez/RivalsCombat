using System.Collections;
using UnityEngine;

public class Pulse : Missile
{
    private Pulsor pulsor;

    void Start()
    {
        rBody = GetComponent<Rigidbody>();
        StartCoroutine(DestroyAfterMaxTime());
    }

    public void CallDestruction()
    {
        Explode();
    }

    protected override void Explode()
    {
        pulsor.NotifyCurrentPulseDestroyed();
        base.Explode();
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        if (!IsCollided)
        {
            IsCollided = true;
            Explode();
        }
    }

    public void SetPulsor(Pulsor pulsor)
    {
        this.pulsor = pulsor;
    }

    private IEnumerator DestroyAfterMaxTime()
    {
        float time = 0f;
        while(time < maxLifeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        pulsor.NotifyCurrentPulseDestroyed();
        Destroy(gameObject);
    }
}
