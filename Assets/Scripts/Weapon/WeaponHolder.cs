using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    // ---- INTERN ----
    private CubeWeapon cubeWeapon;

    void Awake()
    {
        cubeWeapon = transform.GetChild(0).GetComponent<CubeWeapon>();
    }

    public CubeWeapon GetWeapon()
    {
        return cubeWeapon;
    }
}
