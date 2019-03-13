using UnityEngine;
using UnityEngine.UI;

public class PlayerPanel : MonoBehaviour
{
    public Player player;
    public bool HasControllerAssigned { get; private set; }

    public int playerNumber { get { return player.playerNumber; }}

    private bool isPlayerReady = false;

    [Header("UI")]
    [SerializeField]
    private GameObject imgPressAGO;

    void Awake()
    {
        HasControllerAssigned = false;
    }

    void Update()
    {
        if(HasControllerAssigned)
        {
            if (Input.GetButtonDown("J" + playerNumber + "A"))
            {
                isPlayerReady = true;
            }
        }
    }

    public bool IsReady()
    {
        //return !HasControllerAssigned ? false : cubeSelectionPanel.IsReady;
        return !HasControllerAssigned ? true : isPlayerReady;
    }

    public Player AssignController(int controller)
    {
        Debug.Log("Setting player to controller : " + controller); // affD
        player.SetControllerNumber(controller);
        HasControllerAssigned = true;

        imgPressAGO.SetActive(false);

        return player;
    }


}
