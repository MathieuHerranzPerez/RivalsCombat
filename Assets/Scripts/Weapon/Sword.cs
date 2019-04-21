using System.Collections;
using UnityEngine;

public class Sword : CubeWeapon
{
    [SerializeField]
    [Range(0.05f, 2f)]
    private float timeBetweenShoots = 0.6f;
    [Range(1f, 10f)]
    private float timeBewteenDashes = 2f;
    [SerializeField]
    private float damageOnHit = 80f;
    [SerializeField]
    private float dashForce = 10f;

    // ---- INTERN ----
    private bool canShoot = true;
    private bool canDash = true;
    private int numAnim = 0;

    void Update()
    {
        TrackFire(cube.input.RightTrigger, cube.input.IsButtonDown(PlayerInput.Button.B), cube.input.IsButton(PlayerInput.Button.B), cube.input.IsButtonUp(PlayerInput.Button.B));
    }

    void LateUpdate()
    {
        // always turn the firePoint
        Aim();
    }

    public override void TrackFire(float triggerValue, bool isFireBtnDown, bool isFireBtn, bool isFireBtnUp)
    {
        if (triggerValue > 0.05f && canShoot)
        {
            Fire();
            canShoot = false;
            StartCoroutine(DelayAfterAction(timeBetweenShoots, true));
        }
        else if((isFireBtnDown || isFireBtn) && canDash)
        {
            Vector3 heading = firePoint.position - cube.transform.position;
            float distance = heading.magnitude;
            Vector3 direction = heading / distance;

            cube.GetCubeController().Dash(direction, dashForce);
            canDash = false;
            StartCoroutine(DelayAfterAction(timeBewteenDashes, false));
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

    private IEnumerator DelayAfterAction(float duration, bool isShooting)
    {
        float time = 0f;
        while(time < duration)
        {
            time += Time.deltaTime;
            yield return null;
        }
        if (isShooting)
            canShoot = true;
        else
            canDash = true;
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
}
