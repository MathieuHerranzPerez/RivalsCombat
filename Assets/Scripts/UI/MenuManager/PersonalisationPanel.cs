using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PersonalisationPanel : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private PlayerPanel playerPanel = default;
    [SerializeField]
    private EventSystem eventSystem = default;
    [SerializeField]
    private StandaloneInputModule standaloneInputModule = default;
    [SerializeField]
    private Button firstBtnSelected = default;

    void Start()
    {
        // set a default color to the cube
        NotifyColorChanged(Color.white);
    }

    public void NotifyColorChanged(Color color)
    {
        playerPanel.ChangePlayerColor(color);
    }

    public void NotifyWeaponChanged(GameObject weaponPrefab)
    {
        playerPanel.ChangePlayerWeapon(weaponPrefab);
    }

    public void AssignController(int controller)
    {
        //eventSystem.firstSelectedGameObject = firstBtnSelected.gameObject;
        eventSystem.SetSelectedGameObject(firstBtnSelected.gameObject);

        standaloneInputModule.horizontalAxis = InputNameManager.GetHorizontalName(controller);
        standaloneInputModule.verticalAxis = InputNameManager.GetVerticalName(controller);
        standaloneInputModule.submitButton = InputNameManager.GetAName(controller);
        standaloneInputModule.cancelButton = InputNameManager.GetBName(controller);
    }
}
