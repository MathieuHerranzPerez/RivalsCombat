using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponSelectedBtn : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private string weaponsFolderPath = "Weapons";
    [SerializeField]
    private Image selectedWeaponImg = default;
    [SerializeField]
    private GameObject btnWeaponContainerLayoutGO = default;
    [SerializeField]
    private EventSystem eventSystem = default;
    [SerializeField]
    private GameObject btnWeaponPrefab = default;

    // ---- INTERN ----
    private List<CubeWeapon> listWeapon = new List<CubeWeapon>();
    private Object[] weapons;
    private CubeWeapon weaponSelected;

    private MyButton firstBtnSelected;

    void Start()
    {
        InitBtn();
        ChangeWeapon(listWeapon[0]);
    }

    public void DisplayWeaponBtn()
    {
        btnWeaponContainerLayoutGO.SetActive(true);
        eventSystem.SetSelectedGameObject(firstBtnSelected.gameObject);
    }

    public void ChangeWeapon(CubeWeapon weapon)
    {
        weaponSelected = weapon;
        selectedWeaponImg.sprite = weapon.weaponImage;
        HideWeaponBtn();
    }

    private void HideWeaponBtn()
    {
        btnWeaponContainerLayoutGO.SetActive(false);
        eventSystem.SetSelectedGameObject(this.gameObject);
    }

    private void InitBtn()
    {
        // find all the weapons in the resources folder
        weapons = Resources.LoadAll(weaponsFolderPath, typeof(GameObject));

        foreach (Object o in weapons)
        {
            listWeapon.Add(((GameObject)o).GetComponent<CubeWeapon>());
        }

        MyButton previousBtn = null;
        foreach (CubeWeapon cw in listWeapon)
        {
            GameObject btnGO = (GameObject)Instantiate(btnWeaponPrefab, btnWeaponContainerLayoutGO.transform) as GameObject;
            MyButton currentBtn = btnGO.GetComponent<MyButton>();
            MyEventSystemProvider btnEventSystem = btnGO.GetComponent<MyEventSystemProvider>();
            btnEventSystem.eventSystem = this.eventSystem;

            // Set the navigation between the buttons
            Navigation navigation = currentBtn.navigation;
            navigation.mode = Navigation.Mode.Explicit;
            if (previousBtn != null)
            {
                // current
                navigation.selectOnLeft = previousBtn;

                // previous
                Navigation navigationPrevious = previousBtn.navigation;
                navigationPrevious.selectOnRight = currentBtn;
                previousBtn.navigation = navigationPrevious;
            }
            else
            {
                firstBtnSelected = currentBtn;
            }
            currentBtn.navigation = navigation;

            // set the image
            currentBtn.image.sprite = cw.weaponImage;
            // todo give the weapon
            WeaponBtn currentWeaponBtn = currentBtn.GetComponent<WeaponBtn>();
            currentWeaponBtn.Init();
            currentWeaponBtn.SetSelectedBtn(this);
            currentWeaponBtn.SetCubeWeapon(cw);

            previousBtn = currentBtn;
        }
    }
}
