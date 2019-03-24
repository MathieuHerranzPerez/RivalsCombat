using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Player player;

    private string horizontalAxis;
    private string verticalAxis;
    public string aBtn { get; private set; }
    public string bBtn { get; private set; }
    public string xBtn { get; private set; }
    private string triggerAxis;
    private int controllerNumber;

    public float Horizontal { get; set; }
    public float Vertical { get; set; }

    public enum Button
    {
        A,
        B,
        X,
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
        xBtn = "J" + controllerNumber + "X";
        triggerAxis = "J" + controllerNumber + "Trigger";
    }

    // todo doing something to handle launch force
    public bool IsButtonDown (Button button)       // pas dingue ... marche mal ?
    {
        switch(button)
        {
            case Button.A:
                return Input.GetButton(aBtn);
            case Button.B:
                return Input.GetButton(bBtn);
            case Button.X:
                return Input.GetButton(xBtn);
        }
        return false;
    }
}
