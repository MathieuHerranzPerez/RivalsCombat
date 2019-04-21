using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class WeaponBtn : MonoBehaviour
{
    // ---- INTERN ----
    private WeaponSelectedBtn weaponSelectedBtn = default;
    private CubeWeapon cubeWeapon;
    private Button btn;
    private Image weaponImg;

    public void Init()
    {
        weaponImg = GetComponent<Image>();
        btn = GetComponent<MyButton>();
        btn.onClick.AddListener(NotifyClick);
    }

    public void SetCubeWeapon(CubeWeapon weapon)
    {
        this.cubeWeapon = weapon;
        weaponImg.sprite = cubeWeapon.weaponImage;
    }

    public void SetSelectedBtn(WeaponSelectedBtn weaponSelectedBtn)
    {
        this.weaponSelectedBtn = weaponSelectedBtn;
    }

    private void NotifyClick()
    {
        weaponSelectedBtn.ChangeWeapon(cubeWeapon);
    }
}
