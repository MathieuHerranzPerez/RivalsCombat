using System.Collections;
using UnityEngine;

public class MissileLauncher : ProjectilWeapon
{
    [SerializeField]
    private float minLaunchForce = 5f;
    [SerializeField]
    private float maxLaunchForce = 30f;
    [SerializeField]
    private float maxChargeTime = 1.5f;
    [SerializeField]
    private float recoilForceDiv = 6f;
    [SerializeField]
    private float cooldown = 0.2f;

    [Header("Invisibility")]
    [SerializeField]
    private float invisibilityTime = 1.5f;
    [SerializeField]
    private float timeBetweenInvisibilities = 6f;
    [SerializeField]
    private ParticleSystem startInvisibilityEffect = default;
    [SerializeField]
    private ParticleSystem endInvisibilityEffect = default;

    // ---- INTERN ----
    private float currentLaunchForce;
    private float chargeSpeed;
    private bool fired;
    private bool canFire = true;
    private bool canHide = true;

    private float lastRightTriggerValue = 0f;

    void Start()
    {
        Init();
        chargeSpeed = (maxLaunchForce - minLaunchForce) / maxChargeTime;
        currentLaunchForce = minLaunchForce;
        fired = false;
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
        // reset the cursor
        // cursorImage.fillAmount = 0;

        // instantiate and launch the shell
        fired = true;
        GameObject shellCloneGO = (GameObject) Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        shellCloneGO.GetComponent<Rigidbody>().velocity = currentLaunchForce * firePoint.forward;

        Vector3 heading = cube.transform.position - firePoint.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        cube.GetCubeController().ApplyForce(direction * (currentLaunchForce / recoilForceDiv));

        currentLaunchForce = minLaunchForce;

        canFire = false;
        StartCoroutine(Delay(cooldown, ShootingAction.SHOOT));

        //FiringAudio.Play();
        //weaponAnimator.SetTrigger("shoot");
    }

    public override void TrackFire(float leftTriggerValue, float rightTriggerValue, bool isFireBtnDown, bool isFireBtn, bool isFireBtnUp)
    {
        if (currentLaunchForce >= maxLaunchForce && fired)
        {
            cursorImageToFill.fillAmount = 1;
            currentLaunchForce = maxLaunchForce;
        }
        else if(rightTriggerValue > 0.05f && lastRightTriggerValue < 0.05f)
        {
            fired = false;
            currentLaunchForce = minLaunchForce;
        }
        else if((rightTriggerValue > 0.05f && lastRightTriggerValue > 0.05f) && !fired)
        {
            currentLaunchForce += chargeSpeed * Time.deltaTime;

            if (currentLaunchForce >= maxLaunchForce)
                currentLaunchForce = maxLaunchForce;
            else
                currentLaunchForce += chargeSpeed * Time.deltaTime;
            // fill the UI cursor
            cursorImageToFill.fillAmount = currentLaunchForce / maxLaunchForce;
        }
        else if ((rightTriggerValue < 0.05f && lastRightTriggerValue > 0.05f) && !fired)
        {
            if(canFire)
                Fire();
            cursorImageToFill.fillAmount = 0f;
        }
        else if(isFireBtnDown && canHide)
        {
            ParticleSystem p = (ParticleSystem)Instantiate(startInvisibilityEffect, transform.position, transform.rotation);
            Destroy(p, 2f);
            cube.GetCubeController().SetInvisible();
            canHide = false;
            StartCoroutine(DelayInvisibility());
            StartCoroutine(Delay(timeBetweenInvisibilities, ShootingAction.INVISIBLE));
        }

        lastRightTriggerValue = cube.input.RightTrigger;
    }

    private IEnumerator Delay(float duration, ShootingAction action)
    {
        float time = 0f;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }

        if (action == ShootingAction.SHOOT)
            canFire = true;
        else if (action == ShootingAction.INVISIBLE)
            canHide = true;
    }

    private IEnumerator DelayInvisibility()
    {
        float time = 0f;
        while (time < invisibilityTime)
        {
            time += Time.deltaTime;
            yield return null;
        }
        ParticleSystem p = (ParticleSystem)Instantiate(endInvisibilityEffect, transform.position, transform.rotation, transform);
        Destroy(p, 2f);
        cube.GetCubeController().SetVisible();
    }

    private enum ShootingAction
    {
        SHOOT,
        INVISIBLE,
    }
}
