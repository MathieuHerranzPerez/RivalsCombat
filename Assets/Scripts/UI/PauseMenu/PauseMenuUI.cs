using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuUI : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private GameObject pauseCanvasGO = default;
    [SerializeField]
    private SceneFader sceneFader = default;
    [SerializeField]
    private string menuSceneName = "SampleScene";

    [Header("Input setup")]
    [SerializeField]
    private EventSystem eventSystem = default;
    [SerializeField]
    private StandaloneInputModule standaloneInputModule = default;
    [SerializeField]
    private Button firstBtnSelected = default;

    // ---- INTERN ----
    private bool isDisplayed = false;

    void Update()
    {
        if (Input.GetButtonDown(/*"J1Start"*/ InputNameManager.GetStartBtnName(1)))
        {
            Toggle();
        }
    }

    public void Resume()
    {
        Toggle();
    }

    public void Exit()
    {
        Toggle();
        // quit the game
        sceneFader.FadeTo(menuSceneName);
    }

    private void Toggle()
    {
        if (!isDisplayed)
        {
            isDisplayed = true;
            pauseCanvasGO.SetActive(true);
            Time.timeScale = 0f;        // freeze
            AssignController(1);
        }
        else
        {
            isDisplayed = false;
            pauseCanvasGO.SetActive(false);
            Time.timeScale = 1f;        // unfreeze
        }
    }

    private void AssignController(int controller)
    {
        //eventSystem.firstSelectedGameObject = firstBtnSelected.gameObject;
        eventSystem.SetSelectedGameObject(firstBtnSelected.gameObject);

        standaloneInputModule.horizontalAxis = InputNameManager.GetHorizontalName(controller);
        standaloneInputModule.verticalAxis = InputNameManager.GetVerticalName(controller);
        standaloneInputModule.submitButton = InputNameManager.GetAName(controller);
        standaloneInputModule.cancelButton = InputNameManager.GetBName(controller);
    }
}
