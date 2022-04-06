using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InputHandler : MonoBehaviour, ICommand
{
    //The box we control with keys
    public Transform boxTrans;
    public WorldGrid grid;
    //The different keys we need
    private Command buttonW, buttonS, buttonA, buttonD, buttonB, buttonZ, buttonR;
    //Stores all commands for replay and undo
    public static List<Command> oldCommands = new List<Command>();
    //Box start position to know where replay begins
    private Vector3 boxStartPos;
    //To reset the coroutine
    private Coroutine replayCoroutine;
    //If we should start the replay
    public static bool shouldStartReplay;
    //So we cant press keys while replaying
    private bool isReplaying;


    void Start()
    {
        //Bind keys with commands
        buttonB = new DoNothing();
        buttonW = new MoveForward();
        buttonS = new MoveReverse();
        buttonA = new MoveLeft();
        buttonD = new MoveRight();
        buttonZ = new UndoCommand();
        buttonR = new ReplayCommand();

        boxStartPos = boxTrans.position;
        UpdateGrid(WorldGrid.BlockType.populated);
    }



    void Update()
    {
        if (!isReplaying)
        {
            HandleInput();
        }

        StartReplay();
    }


    //Check if we press a key, if so do what the key is binded to 
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            buttonA.Execute(boxTrans, buttonA, this);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            buttonB.Execute(boxTrans, buttonB, this);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            buttonD.Execute(boxTrans, buttonD, this);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            buttonR.Execute(boxTrans, buttonZ, this);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            buttonS.Execute(boxTrans, buttonS, this);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            buttonW.Execute(boxTrans, buttonW, this);
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            buttonZ.Execute(boxTrans, buttonZ, this);
        }
    }


    //Checks if we should start the replay
    void StartReplay()
    {
        if (shouldStartReplay && oldCommands.Count > 0)
        {
            shouldStartReplay = false;

            //Stop the coroutine so it starts from the beginning
            if (replayCoroutine != null)
            {
                StopCoroutine(replayCoroutine);
            }

            //Start the replay
            replayCoroutine = StartCoroutine(ReplayCommands(boxTrans));
        }
    }


    //The replay coroutine
    IEnumerator ReplayCommands(Transform boxTrans)
    {
        //So we can't move the box with keys while replaying
        isReplaying = true;

        //Move the box to the start position
        boxTrans.position = boxStartPos;

        for (int i = 0; i < oldCommands.Count; i++)
        {
            //Move the box with the current command
            oldCommands[i].Move(boxTrans, this);

            yield return new WaitForSeconds(0.3f);
        }

        //We can move the box again
        isReplaying = false;
    }

    public void UpdateGrid(WorldGrid.BlockType blockType)
    {
        grid.PopulateGrid(transform.position, blockType);
    }

    public bool IsWithinBoundary(Vector3 nextPos)
    {
        return grid.IsWithinBoundary(nextPos);
    }
}