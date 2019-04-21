using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorSelectedBtn : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private Image selectedColorImg = default;
    [SerializeField]
    private GameObject btnColorContainerLayoutGO = default;
    [SerializeField]
    private EventSystem eventSystem = default;
    [SerializeField]
    private Button firstBtnSelected = default;
    [SerializeField]
    private PersonalisationPanel personalisationPanel = default;

    // ---- INTERN ----
    private Color colorSeleted;

    public void DisplayColoredBtn()
    {
        btnColorContainerLayoutGO.SetActive(true);
        eventSystem.SetSelectedGameObject(firstBtnSelected.gameObject); 
    }

    public void ChangeColor(Color color)
    {
        colorSeleted = color;
        selectedColorImg.color = color;
        HideColoredBtn();
        personalisationPanel.NotifyColorChanged(color);
    }

    private void HideColoredBtn()
    {
        btnColorContainerLayoutGO.SetActive(false);
        eventSystem.SetSelectedGameObject(this.gameObject);
    }
}
