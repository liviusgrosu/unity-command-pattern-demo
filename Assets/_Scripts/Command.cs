using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CommandCallback<T>(T input);
//The parent class
public abstract class Command
{
    //How far should the box move when we press a button
    protected float moveDistance = 1f;

    //Move and maybe save command
    public abstract void Execute(Transform boxTrans, Command command, ICommand callback);

    //Undo an old command
    public virtual void Undo(Transform boxTrans) { }

    //Move the box
    public virtual void Move(Transform boxTrans, ICommand callback) { }
}


//
// Child classes
//

public class MoveForward : Command
{
    //Called when we press a key
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        //Move the box
        Move(boxTrans, callback);

        //Save the command
        InputHandler.oldCommands.Add(command);
    }

    //Undo an old command
    public override void Undo(Transform boxTrans)
    {
        boxTrans.Translate(-boxTrans.forward * moveDistance);
    }

    //Move the box
    public override void Move(Transform boxTrans, ICommand callback)
    {
        Vector3 nextPoint = boxTrans.forward * moveDistance; 

        if (callback.IsWithinBoundary(boxTrans.position + nextPoint))
        {
            boxTrans.Translate(nextPoint);
            callback.UpdateGrid(WorldGrid.BlockType.populated);
        }
    }
}


public class MoveReverse : Command
{
    //Called when we press a key
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        //Move the box
        Move(boxTrans, callback);

        //Save the command
        InputHandler.oldCommands.Add(command);
    }

    //Undo an old command
    public override void Undo(Transform boxTrans)
    {
        boxTrans.Translate(boxTrans.forward * moveDistance);
    }

    //Move the box
    public override void Move(Transform boxTrans, ICommand callback)
    {
        Vector3 nextPoint = -boxTrans.forward * moveDistance; 

        if (callback.IsWithinBoundary(boxTrans.position + nextPoint))
        {
            boxTrans.Translate(nextPoint);
            callback.UpdateGrid(WorldGrid.BlockType.populated);
        }
    }
}


public class MoveLeft : Command
{
    //Called when we press a key
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        //Move the box
        Move(boxTrans, callback);

        //Save the command
        InputHandler.oldCommands.Add(command);
    }

    //Undo an old command
    public override void Undo(Transform boxTrans)
    {
        boxTrans.Translate(boxTrans.right * moveDistance);
    }

    //Move the box
    public override void Move(Transform boxTrans, ICommand callback)
    {
        Vector3 nextPoint = -boxTrans.right * moveDistance; 

        if (callback.IsWithinBoundary(boxTrans.position + nextPoint))
        {
            boxTrans.Translate(nextPoint);
            callback.UpdateGrid(WorldGrid.BlockType.populated);
        }
    }
}


public class MoveRight : Command
{
    //Called when we press a key
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        //Move the box
        Move(boxTrans, callback);

        //Save the command
        InputHandler.oldCommands.Add(command);
    }

    //Undo an old command
    public override void Undo(Transform boxTrans)
    {
        boxTrans.Translate(-boxTrans.right * moveDistance);
    }

    //Move the box
    public override void Move(Transform boxTrans, ICommand callback)
    {
        Vector3 nextPoint = boxTrans.right * moveDistance; 

        if (callback.IsWithinBoundary(boxTrans.position + nextPoint))
        {
            boxTrans.Translate(nextPoint);
            callback.UpdateGrid(WorldGrid.BlockType.populated);
        }
    }
}


//For keys with no binding
public class DoNothing : Command
{
    //Called when we press a key
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        //Nothing will happen if we press this key
    }
}


//Undo one command
public class UndoCommand : Command
{
    //Called when we press a key
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        List<Command> oldCommands = InputHandler.oldCommands;

        if (oldCommands.Count > 0)
        {
            Command latestCommand = oldCommands[oldCommands.Count - 1];

            // Unpopulate the boxes current position in the grid
            callback.UpdateGrid(WorldGrid.BlockType.unpopulated);

            //Move the box with this command
            latestCommand.Undo(boxTrans);

            //Remove the command from the list
            oldCommands.RemoveAt(oldCommands.Count - 1);
        }
    }
}


//Replay all commands
public class ReplayCommand : Command
{
    public override void Execute(Transform boxTrans, Command command, ICommand callback)
    {
        callback.RefreshGrid();
        InputHandler.shouldStartReplay = true;
    }
}