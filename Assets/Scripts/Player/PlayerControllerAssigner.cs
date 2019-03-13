using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerControllerAssigner : MonoBehaviour
{

    private List<int> listAssignedControllers = new List<int>();
    private PlayerPanel[] listPlayerPanel;


    void Awake()
    {
        listPlayerPanel = FindObjectsOfType<PlayerPanel>().OrderBy(t => t.playerNumber).ToArray();
    }

    void Update()
    {
        bool assigned = false;
        int i = 1;
        while(i <= 4 && !assigned)
        {
            if (!listAssignedControllers.Contains(i))
            {
                if (Input.GetButton("J" + i + "A"))
                {
                    AddPlayerController(i);
                    assigned = true;
                }
            }
            ++i;
        }
    }

    public Player AddPlayerController(int controller)
    {
        listAssignedControllers.Add(controller);

        int i = 0;
        bool found = false;
        while(i < listPlayerPanel.Length && !found)
        {
            if (!listPlayerPanel[i].HasControllerAssigned)
            {
                found = true;
            }
            else
            {
                ++i;
            }
        }

        Player playerRes = null;
        if(found)
            playerRes = listPlayerPanel[i].AssignController(controller);

        return playerRes;
    }
}
