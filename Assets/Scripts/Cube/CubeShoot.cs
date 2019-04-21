using UnityEngine;

[RequireComponent(typeof(Cube))]
public class CubeShoot : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private WeaponHolder weaponHolder = default;

    // ---- INTER ----
    private Cube cube;
    private CubeWeapon weapon;

    void Awake()
    {
        cube = GetComponent<Cube>();
    }

    void Update()
    {
        DisplayWeaponDirection(); // affD
    }

    public void NotifyNewWeapon()
    {
        weapon = weaponHolder.GetWeapon();
        weapon.SetCube(cube);
    }

    private void DisplayWeaponDirection()
    {
        if (weapon)
        {
            Vector3 dir = weapon.firePoint.position - transform.position;
            Debug.DrawRay(transform.position, dir * 2, Color.green);
        }
    }
}
