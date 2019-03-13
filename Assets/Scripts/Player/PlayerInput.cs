using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

    private string horizontalAxis;
    private string verticalAxis;
    private string aBtn;
    private string bBtn;
    private string triggerAxis;
    private int controllerNumber;

    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public enum Button
    {
        A,
        B,
    }

    void Awake()
    {
        player = GetComponent<Player>();
    }

    void Update()
    {
        if(controllerNumber > 0)
        {
            Horizontal = Input.GetAxis(horizontalAxis);
            Vertical = Input.GetAxis(verticalAxis);
        }
    }

    public void SetControllerNumber(int number)
    {
        controllerNumber = number;
        horizontalAxis = "J" + controllerNumber + "Horizontal";
        verticalAxis = "J" + controllerNumber + "Vertical";
        aBtn = "J" + controllerNumber + "A";
        bBtn = "J" + controllerNumber + "B";
        triggerAxis = "J" + controllerNumber + "Trigger";
    }

    bool ButtonIsDown(Button button)
    {
        switch(button)
        {
            case Button.A:
                return Input.GetButton(aBtn);
            case Button.B:
                return Input.GetButton(bBtn);
        }
        return false;
    }
}
