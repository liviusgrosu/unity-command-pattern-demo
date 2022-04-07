using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyRebindMenu : MonoBehaviour
{
    public InputHandler inputHandler;
    public Dropdown ForwardDropdown, ReverseDropdown, LeftDrowdown, RightDropdown, UndoDropdown, ReplayDropdown;
    private bool toggleState;

    void Start()
    {
        ForwardDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ForwardDropdown); });
        ReverseDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ReverseDropdown); });
        LeftDrowdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(LeftDrowdown); });
        RightDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(RightDropdown); });
        UndoDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(UndoDropdown); });
        ReplayDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ReplayDropdown); });
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            toggleState = !toggleState;

            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(toggleState);
            }
        }
    }

    void Destroy() 
    {
        ForwardDropdown.onValueChanged.RemoveAllListeners();
    }
    
    private void keyRebindDropdownHandler(Dropdown target) 
    {
        if (ForwardDropdown == target)
        {
            inputHandler.RebindKey("forward", target.value);
        }
        else if (ReverseDropdown == target)
        {
            inputHandler.RebindKey("reverse", target.value);
        }
        else if (LeftDrowdown == target)
        {
            inputHandler.RebindKey("left", target.value);
        }
        else if (RightDropdown == target)
        {
            inputHandler.RebindKey("right", target.value);
        }
        else if (UndoDropdown == target)
        {
            inputHandler.RebindKey("undo", target.value);
        }
        else if (ReplayDropdown == target)
        {
            inputHandler.RebindKey("replay", target.value);
        }
    }
}