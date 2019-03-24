using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    public Player player;
    public bool hasControllerAssigned { get; private set; }

    public int playerNumber { get { return player.playerNumber; }}

    private bool isPlayerReady = false;

    [Header("UI")]
    [SerializeField]
    private GameObject imgPressAGO = default;
    [SerializeField]
    private GameObject imgPressXGO = default;
    [SerializeField]
    private GameObject imgReadyGO = default;

    void Awake()
    {
        hasControllerAssigned = false;
    }

    void Update()
    {
        if(hasControllerAssigned)
        {
            //if (Input.GetButtonDown("J" + playerNumber + "A"))
            if (Input.GetButtonDown(player.GetPlayerInput().xBtn))
            {
                ToggleReady();
            }
        }
    }

    public bool IsReady()
    {
        return !hasControllerAssigned ? true : isPlayerReady;
    }

    public Player AssignController(int controller)
    {
        Debug.Log("Setting player to controller : " + controller); // affD
        player.SetControllerNumber(controller);
        hasControllerAssigned = true;

        imgPressAGO.SetActive(false);
        imgPressXGO.SetActive(true);

        return player;
    }


    private void ToggleReady()
    {
        isPlayerReady = !isPlayerReady;
        if (isPlayerReady)
        {
            imgReadyGO.SetActive(true);
            imgPressXGO.SetActive(false);
        }
        else
        {
            imgReadyGO.SetActive(false);
            imgPressXGO.SetActive(true);
        }
    }
}
