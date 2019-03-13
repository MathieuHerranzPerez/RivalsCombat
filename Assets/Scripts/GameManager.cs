using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerControllerAssigner playerControllerAssigner;


    private PlayerPanel[] listPlayerPanel;
    private bool isSelectionPhase;

    void Awake()
    {
        isSelectionPhase = true;
    }

    void Start()
    {
        listPlayerPanel = playerControllerAssigner.GetListPlayerPanel();
    }

    // Update is called once per frame
    void Update()
    {
        if(isSelectionPhase)
        {
            // if we have at least 2 players
            if(playerControllerAssigner.GetListAssignedControllers().Count >= 2)
            {
                // check if the players are ready
                bool arePlayersReady = true;
                foreach(PlayerPanel pp in listPlayerPanel)
                {
                    arePlayersReady &= pp.IsReady();
                }

                if(arePlayersReady)
                {
                    LaunchGame();
                    isSelectionPhase = false;
                }
            }
        }
    }

    private void LaunchGame()
    {

    }
}
