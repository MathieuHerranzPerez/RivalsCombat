using System.Collections;
using UnityEngine;

public class Sword : CubeWeapon
{
    [SerializeField]
    [Range(0.05f, 2f)]
    private float timeBetweenShoots = 0.6f;
    [Range(1f, 10f)]
    private float timeBewteenDashes = 2f;
    [Range(1f, 10f)]
    private float timeBewteenReflects = 5f;
    [Range(0.1f, 1f)]
    private float reflectDuration = 0.5f;
    [SerializeField]
    private float damageOnHit = 80f;
    [SerializeField]
    private float dashForce = 10f;
    [Header("Setup")]
    [SerializeField]
    private ReflectTrigger reflectTrigger = default;

    // ---- INTERN ----
    private bool canShoot = true;
    private bool canReflect = true;
    private bool canDash = true;
    private int numAnim = 0;

    void Update()
    {
        TrackFire(cube.input.LeftTrigger, cube.input.RightTrigger, cube.input.IsButtonDown(PlayerInput.Button.B), cube.input.IsButton(PlayerInput.Button.B), cube.input.IsButtonUp(PlayerInput.Button.B));
    }

    void LateUpdate()
    {
        // always turn the firePoint
        Aim();
    }

    public override void TrackFire(float leftTriggerValue, float rightTriggerValue, bool isFireBtnDown, bool isFireBtn, bool isFireBtnUp)
    {
        if (rightTriggerValue > 0.05f && canShoot)
        {
            Fire();
            canShoot = false;
            StartCoroutine(DelayAfterAction(timeBetweenShoots, ShootingAction.SHOOT));
        }
        else if((isFireBtnDown || isFireBtn) && canDash)
        {
            Vector3 heading = firePoint.position - cube.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            cube.GetCubeController().Dash(direction, dashForce);
            canDash = false;
            StartCoroutine(DelayAfterAction(timeBewteenDashes, ShootingAction.DASH));
        }
        else if(leftTriggerValue > 0.05f && canReflect)
        {
            Reflect();
            canReflect = false;
            StartCoroutine(StopReflect(reflectDuration));
            StartCoroutine(DelayAfterAction(timeBewteenReflects, ShootingAction.REFLECT));
        }
    }

    protected override void Fire()
    {
        if(numAnim == 0)
            weaponAnimator.SetTrigger("Shoot");
        else if(numAnim == 1)
            weaponAnimator.SetTrigger("Shoot1");
        else
            weaponAnimator.SetTrigger("Shoot2");
        numAnim = (numAnim == 2) ? 0 : numAnim + 1;
    }

    private IEnumerator DelayAfterAction(float duration, ShootingAction action)
    {
        float time = 0f;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (action == ShootingAction.SHOOT)
            canShoot = true;
        else if (action == ShootingAction.DASH)
            canDash = true;
        else if (action == ShootingAction.REFLECT)
            canReflect = true;
    }

    private IEnumerator StopReflect(float duration)
    {
        float time = 0f;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        StopReflect();
    }

    private void Reflect()
    {
        reflectTrigger.gameObject.SetActive(true);
        weaponAnimator.SetBool("IsReflecting", true);
    }

    public void NotifyReflection(Bullet bullet)
    {
        Vector3 direction = new Vector3(cube.input.Horizontal, cube.input.Vertical, 0f).normalized;
        if (direction != Vector3.zero)
            bullet.ChangeVelocity(direction);
        else
            bullet.InverseHorizontalVelocity();
    }

    private void StopReflect()
    {
        reflectTrigger.gameObject.SetActive(false);
        weaponAnimator.SetBool("IsReflecting", false);
    }

    public void Hit(Collider other)
    {
        Cube target = other.GetComponent<Cube>();
        if (target)
        {
            target.TakeDamageFormBullet(damageOnHit);
        }
        else
        {
            Bullet bullet = other.GetComponent<Bullet>();
            if(bullet)
            {
                // do nothing ?
            }
        }
    }

    private void Aim()
    {
        if (cube != null)
        {
            float x = cube.input.Horizontal;
            float y = cube.input.Vertical;
            if (x != 0.0 || y != 0.0)
            {
                float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
                // cube.GetCubeController().RotateForAim(angle);
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }

    private enum ShootingAction
    {
        SHOOT,
        DASH,
        REFLECT,
    }
}
