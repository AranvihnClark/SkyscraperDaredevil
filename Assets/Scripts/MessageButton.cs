using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageButton : MonoBehaviour
{
    // Variable declarations.
    [SerializeField] KeyCode key;
    private Button button;

    
    private void Start()
    {
        // Initialization of our variable.
        button = GetComponent<Button>();
    }

    // Update is called once per frame
    private void Update()
    {
        // Checks to see if the 'jump' button is being pressed.
        // If so, it will 'invoke' (emulate) a mouse click on the button.
        if (Input.GetButtonDown("Jump"))
        {
            button.onClick.Invoke();
        }
    }
}
