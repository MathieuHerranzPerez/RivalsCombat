using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField]
    private PlayerControllerAssigner playerControllerAssigner = default;
    [SerializeField]
    private SceneFader sceneFader = default;

    [Header("Phase screens")]
    [SerializeField]
    private GameObject StartCanvasGO = default;
    [SerializeField]
    private GameObject TeamSelectionCanvasGO = default;

    // ---- INTERN ----
    private PlayerPanel[] listPlayerPanel;
    private int nbPlayers = -1;

    // states
    private bool isStartPhase;
    private bool isTeamSelectionPhase;

    void Awake()
    {
        isStartPhase = true;
    }

    void Start()
    {
        listPlayerPanel = playerControllerAssigner.GetListPlayerPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if(isStartPhase)
        {
            StartPhase();
        }
    }


    private void StartPhase()
    {
        // if we have at least 2 players
        if (playerControllerAssigner.GetListAssignedControllers().Count >= 2)
        {
            // check if the players are ready
            bool arePlayersReady = true;
            foreach (PlayerPanel pp in listPlayerPanel)
            {
                arePlayersReady &= pp.IsReady();
            }

            if (arePlayersReady)
            {
                GoFromStartPhase();
            }
        }
    }

    private void GoFromStartPhase()
    {
        isStartPhase = false;
        nbPlayers = playerControllerAssigner.GetListAssignedControllers().Count;

        // if we have more than 2 players
        if (nbPlayers > 2)
        {
            // TODO
            // display the team selection screen
        }
        else
        {
            LaunchGame();
        }

    }

    private void TeamSelectionPhase()
    {

    }

    private void LaunchGame()
    {
        sceneFader.FadeTo("MainScene");
    }
}
