using UnityEngine;

public class PlayerPanel : MonoBehaviour
{
    public Player player;
    public bool HasControllerAssigned = false;

    public int playerNumber { get { return player.playerNumber; }}

    public bool IsReady()
    {
        //return !HasControllerAssigned ? false : cubeSelectionPanel.IsReady;
        return HasControllerAssigned;
    }

    public Player AssignController(int controller)
    {
        Debug.Log("Setting player to controller"); // affD
        player.input.SetControllerNumber(controller);
        HasControllerAssigned = true;

        return player;
    }
}
