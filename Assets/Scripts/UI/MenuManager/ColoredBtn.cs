using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]
public class ColoredBtn : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private ColorSelectedBtn colorSelectedBtn = default;

    // ---- INTERN ----
    private Color color;
    private Button btn;

    void Start()
    {
        color = GetComponent<Image>().color;
        btn = GetComponent<Button>();
        btn.onClick.AddListener(NotifyClick);
    }

    private void NotifyClick()
    {
        colorSelectedBtn.ChangeColor(color);
    }
}
