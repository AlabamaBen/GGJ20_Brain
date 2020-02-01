using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;
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
        buttonArray[0].transform.GetChild(1).GetComponent<Button>().Select();
        lastMenuButtonSelected = buttonArray[0].transform.GetChild(1).GetComponent<Button>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            lastMenuButtonSelected.Select();
        }
    }

    public void SelectButton(GameObject container)
    {
        lastMenuButtonSelected = container.transform.GetChild(1).GetComponent<Button>();
        for (int i = 0; i < buttonArray.Length; i++)
        {
            if (buttonArray[i].Equals(container))
            {
                container.transform.GetChild(0).gameObject.SetActive(true);
                container.transform.GetChild(1).GetComponent<Button>().Select();
            }
            else
            {
                buttonArray[i].transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    
}
