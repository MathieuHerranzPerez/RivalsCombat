using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private CubeShoot cubeShoot;

    // ---- INTERN ----
    private CubeWeapon cubeWeapon;

    void Awake()
    {
        //cubeWeapon = transform.GetChild(0).GetComponent<CubeWeapon>();
    }

    public CubeWeapon GetWeapon()
    {
        return cubeWeapon;
    }

    public void SetWeapon(GameObject weaponPrefab)
    {
        GameObject weaponClone = (GameObject)Instantiate(weaponPrefab, transform);
        cubeWeapon = weaponClone.GetComponent<CubeWeapon>();
        cubeShoot.NotifyNewWeapon();
    }
}
