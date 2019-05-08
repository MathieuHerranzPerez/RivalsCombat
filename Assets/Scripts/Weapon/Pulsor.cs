using System.Collections;
using UnityEngine;

public class Pulsor : ProjectilWeapon
{ 
    [SerializeField]
    private float launchForce = 30f;
    [SerializeField]
    private float timeBetweenShots = 1f;
    [Header("TP")]
    [SerializeField]
    private float timeBetweenTP = 6f;
    [SerializeField]
    private float tpDistance = 5f;
    [SerializeField]
    private ParticleSystem startTPEffect = default;
    [SerializeField]
    private ParticleSystem endTPEffect = default;

    // ---- INTERN ----
    private bool fired;
    private bool canShoot = true;
    private bool canTP = true;

    private Pulse currentPulse;

    private float lastRightTriggerValue = 0f;

    void Start()
    {
        Init();
        fired = false;
        cursorImageToFill.fillAmount = 1f;
    }

    void Update()
    {
        ActOnCubeAndTrackFire();
    }

    void LateUpdate()
    {
        Aiming();   // to aim if the player is shooting
    }

    protected override void Fire()
    {
        // instantiate and launch the pusle
        fired = true;
        GameObject shellCloneGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shellCloneGO.GetComponent<Rigidbody>().velocity = launchForce * firePoint.forward;

        Vector3 heading = cube.transform.position - firePoint.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        canShoot = false;

        currentPulse = shellCloneGO.GetComponent<Pulse>();
        currentPulse.SetPulsor(this);

        StartCoroutine(StartDelay(timeBetweenShots, ShootingAction.SHOOT));
        //FiringAudio.Play();
        //weaponAnimator.SetTrigger("shoot");
    }

    public override void TrackFire(float leftTriggerValue, float rightTriggerValue, bool isFireBtnDown, bool isFireBtn, bool isFireBtnUp)
    {
        if (canShoot)
        {
            if (rightTriggerValue > 0.05f && lastRightTriggerValue < 0.05f)
            {
                fired = false;
            }
            else if ((rightTriggerValue < 0.05f && lastRightTriggerValue > 0.05f) && !fired)
            {
                Fire();
            }
            
            lastRightTriggerValue = cube.input.RightTrigger;
        }
        if(currentPulse != null)
        {
            if (leftTriggerValue > 0.05f)
            {
                currentPulse.CallDestruction();
            }
        }

        if ((isFireBtnDown || isFireBtn) && canTP)
        {
            Vector3 direction = new Vector3(cube.input.Horizontal, cube.input.Vertical, 0f).normalized;

            ParticleSystem fxStartTP = (ParticleSystem)Instantiate(startTPEffect, transform.position, transform.rotation);
            Destroy(fxStartTP.gameObject, 2f);
            cube.GetCubeController().TP(direction, tpDistance);
            ParticleSystem fxEndTP = (ParticleSystem)Instantiate(endTPEffect, transform.position, transform.rotation);
            Destroy(fxEndTP.gameObject, 2f);

            canTP = false;
            StartCoroutine(StartDelay(timeBetweenTP, ShootingAction.TP));
        }
    }

    public void NotifyCurrentPulseDestroyed()
    {
        currentPulse = null;
    }

    private IEnumerator StartDelay(float duration, ShootingAction action)
    {
        float time = 0f;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (action == ShootingAction.SHOOT)
            canShoot = true;
        else if (action == ShootingAction.TP)
            canTP = true;
    }

    private enum ShootingAction
    {
        SHOOT,
        TP,
    }
}
