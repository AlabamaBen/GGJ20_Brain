using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    public static MenuManager instance = null;
    [SerializeField] GameObject[] buttonArray;
    Button lastMenuButtonSelected;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Select first button and save it as the last selected button
        buttonArray[0].transform.GetChild(1).GetComponent<Button>().Select();
        lastMenuButtonSelected = buttonArray[0].transform.GetChild(1).GetComponent<Button>();
    }

    private void Update()
    {
        //If the player click outside buttons, select last selected button
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            if (lastMenuButtonSelected != null)
            {
                lastMenuButtonSelected.Select();
            }
        }
    }

    //Called when a button is selected
    public void SelectButton(GameObject container)
    {
        //Save the selected button
        lastMenuButtonSelected = container.transform.GetChild(1).GetComponent<Button>();

        //Find which button has been selected
        for (int i = 0; i < buttonArray.Length; i++)
        {
            //Display selector
            if (buttonArray[i].Equals(container))
            {
                container.transform.GetChild(0).gameObject.SetActive(true);
                container.transform.GetChild(1).GetComponent<Button>().Select();
            }

            //Hide the selector of other buttons
            else
            {
                buttonArray[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    
}
