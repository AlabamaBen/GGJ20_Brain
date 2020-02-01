using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Panel_Behviour : MonoBehaviour
{

    public string TrueString;
    public string FalseString;


    private TextMeshProUGUI textUI; 

    // Start is called before the first frame update
    void Start()
    {
        textUI = GameObject.FindGameObjectWithTag("PanelUI").GetComponent<TextMeshProUGUI>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(GameManager.Gm.ReadActivated)
            {
                textUI.text = TrueString; 
            }
            else
            {
                textUI.text = FalseString;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textUI.text = "";
        }
    }
}
