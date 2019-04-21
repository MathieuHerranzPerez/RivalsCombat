using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class WeaponBtn : MonoBehaviour
{
    // ---- INTERN ----
    private WeaponSelectedBtn weaponSelectedBtn = default;
    private GameObject cubeWeaponPrefab;
    private Button btn;
    private Image weaponImg;

    public void Init()
    {
        weaponImg = GetComponent<Image>();
        btn = GetComponent<MyButton>();
        btn.onClick.AddListener(NotifyClick);
    }

    public void SetCubeWeaponPrefab(GameObject weapon)
    {
        this.cubeWeaponPrefab = weapon;
        weaponImg.sprite = cubeWeaponPrefab.GetComponent<CubeWeapon>().weaponImage;
    }

    public void SetSelectedBtn(WeaponSelectedBtn weaponSelectedBtn)
    {
        this.weaponSelectedBtn = weaponSelectedBtn;
    }

    private void NotifyClick()
    {
        weaponSelectedBtn.ChangeWeapon(cubeWeaponPrefab);
    }
}
