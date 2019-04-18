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

    void Start()
    {
        cube = GetComponent<Cube>();
        weapon = weaponHolder.GetWeapon();
        weapon.SetCube(cube);
    }

    void Update()
    {
        DisplayWeaponDirection(); // affD
    }

    private void DisplayWeaponDirection()
    {
        Vector3 dir = weapon.firePoint.position - transform.position;
        Debug.DrawRay(transform.position, dir * 2, Color.green);
    }
}
