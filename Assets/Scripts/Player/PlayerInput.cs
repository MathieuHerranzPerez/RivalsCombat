using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{

    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public float LeftTrigger { get; set; }
    public float RightTrigger { get; set; }

    // ---- INTERN ----
    private Player player;

    // button
    private string aBtn;
    private string bBtn;
    private string xBtn;
    private string yBtn;
    private string leftBumper;
    private string rightBumper;
    private string backBtn;
    private string startBtn;
    private string leftStickBtn;
    private string rightStickBtn;

    // axis
    private string leftTriggerAxis;
    private string rightTriggerAxis;
    private string horizontalAxis;
    private string verticalAxis;
    private string dPadHorizontal;
    private string dPadVertical;
    private string rightHorizontalAxis;
    private string rightVerticalAxis;
    

    private int controllerNumber;


    public enum Button
    {
        A,
        B,
        X,
        Y,
        LB,
        RB,
        BACK,
        START,
        LEFT_STICK,
        RIGHT_STICK,
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
            LeftTrigger = Input.GetAxis(leftTriggerAxis);
            RightTrigger = Input.GetAxis(rightTriggerAxis);
        }
    }

    public void SetControllerNumber(int number)
    {
        controllerNumber = number;

        // button
        aBtn = "J" + controllerNumber + "A";
        bBtn = "J" + controllerNumber + "B";
        xBtn = "J" + controllerNumber + "X";
        yBtn = "J" + controllerNumber + "Y";
        leftBumper = "J" + controllerNumber + "LB";
        rightBumper = "J" + controllerNumber + "RB";
        backBtn = "J" + controllerNumber + "Back";
        startBtn = "J" + controllerNumber + "Start";
        leftStickBtn = "J" + controllerNumber + "LeftStick";
        rightStickBtn = "J" + controllerNumber + "RightStick";

        // axis
        horizontalAxis = "J" + controllerNumber + "Horizontal";
        verticalAxis = "J" + controllerNumber + "Vertical";
        rightHorizontalAxis = "J" + controllerNumber + "RightHorizontal";
        rightVerticalAxis = "J" + controllerNumber + "RightVertical";
        dPadHorizontal = "J" + controllerNumber + "DPadHorizontal";
        dPadVertical = "J" + controllerNumber + "DPadVertical";
        leftTriggerAxis = "J" + controllerNumber + "LeftTrigger";
        rightTriggerAxis = "J" + controllerNumber + "RightTrigger";
    }

    public bool IsButtonDown (Button button)
    {
        switch(button)
        {
            case Button.A:
                return Input.GetButtonDown(aBtn);
            case Button.B:
                return Input.GetButtonDown(bBtn);
            case Button.X:
                return Input.GetButtonDown(xBtn);
            case Button.Y:
                return Input.GetButtonDown(yBtn);
            case Button.LB:
                return Input.GetButtonDown(leftBumper);
            case Button.RB:
                return Input.GetButtonDown(rightBumper);
            case Button.BACK:
                return Input.GetButtonDown(backBtn);
            case Button.START:
                return Input.GetButtonDown(startBtn);
            case Button.LEFT_STICK:
                return Input.GetButtonDown(leftStickBtn);
            case Button.RIGHT_STICK:
                return Input.GetButtonDown(rightStickBtn);
        }
        return false;
    }

    public bool IsButton(Button button)
    {
        switch (button)
        {
            case Button.A:
                return Input.GetButton(aBtn);
            case Button.B:
                return Input.GetButton(bBtn);
            case Button.X:
                return Input.GetButton(xBtn);
            case Button.Y:
                return Input.GetButton(yBtn);
            case Button.LB:
                return Input.GetButton(leftBumper);
            case Button.RB:
                return Input.GetButton(rightBumper);
            case Button.BACK:
                return Input.GetButton(backBtn);
            case Button.START:
                return Input.GetButton(startBtn);
            case Button.LEFT_STICK:
                return Input.GetButton(leftStickBtn);
            case Button.RIGHT_STICK:
                return Input.GetButton(rightStickBtn);
        }
        return false;
    }

    public bool IsButtonUp(Button button)
    {
        switch (button)
        {
            case Button.A:
                return Input.GetButtonUp(aBtn);
            case Button.B:
                return Input.GetButtonUp(bBtn);
            case Button.X:
                return Input.GetButtonUp(xBtn);
            case Button.Y:
                return Input.GetButtonUp(yBtn);
            case Button.LB:
                return Input.GetButtonUp(leftBumper);
            case Button.RB:
                return Input.GetButtonUp(rightBumper);
            case Button.BACK:
                return Input.GetButtonUp(backBtn);
            case Button.START:
                return Input.GetButtonUp(startBtn);
            case Button.LEFT_STICK:
                return Input.GetButtonUp(leftStickBtn);
            case Button.RIGHT_STICK:
                return Input.GetButtonUp(rightStickBtn);
        }
        return false;
    }
}
