using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyRebind : MonoBehaviour
{
    public InputHandler inputHandler;
    public Dropdown ForwardDropdown, ReverseDropdown, LeftDrowdown, RightDropdown;

    void Start() 
    {
        ForwardDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ForwardDropdown); });
        ReverseDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(ReverseDropdown); });
        LeftDrowdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(LeftDrowdown); });
        RightDropdown.onValueChanged.AddListener(delegate { keyRebindDropdownHandler(RightDropdown); });
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
    }
}