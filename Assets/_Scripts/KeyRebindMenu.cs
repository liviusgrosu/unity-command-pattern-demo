using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyRebindMenu : MonoBehaviour
{
    public InputHandler inputHandler;
    public Dropdown ForwardDropdown, ReverseDropdown, LeftDrowdown, RightDropdown, UndoDropdown, ReplayDropdown;
    void Start()
    {
        // Add listeners to dropdowns
        ForwardDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ForwardDropdown); });
        ReverseDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ReverseDropdown); });
        LeftDrowdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(LeftDrowdown); });
        RightDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(RightDropdown); });
        UndoDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(UndoDropdown); });
        ReplayDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ReplayDropdown); });
    }

    void Update()
    {
        // Toggle the keybinding menu
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(!child.gameObject.activeSelf);
            }
        }
    }

    void Destroy() 
    {
        // Remove all listeners to dropdown
        ForwardDropdown.onValueChanged.RemoveAllListeners();
    }
    
    private void keyRebindDropdownHandler(Dropdown target) 
    {
        // Change FORWARD key
        if (ForwardDropdown == target)
        {
            inputHandler.RebindKey("forward", target.value);
        }
        // Change REVERSE key
        else if (ReverseDropdown == target)
        {
            inputHandler.RebindKey("reverse", target.value);
        }
        // Change LEFT key
        else if (LeftDrowdown == target)
        {
            inputHandler.RebindKey("left", target.value);
        }
        // Change RIGHT key
        else if (RightDropdown == target)
        {
            inputHandler.RebindKey("right", target.value);
        }
        // Change UNDO key
        else if (UndoDropdown == target)
        {
            inputHandler.RebindKey("undo", target.value);
        }
        // Change REPLAY key
        else if (ReplayDropdown == target)
        {
            inputHandler.RebindKey("replay", target.value);
        }
    }
}