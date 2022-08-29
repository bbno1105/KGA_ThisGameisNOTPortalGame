using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    int menuNumber;

    [SerializeField] Text[] menu;

    void Start()
    {
        menuNumber = 0;
        menu[0].color = new Color(1, 1f, 0.2f, 1f);
        menu[1].color = new Color(1, 1, 1, 0.5f);
    }

    void Update()
    {
        SelectMenu();
    }

    void SelectMenu()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (0 < menuNumber)
            {
                menuNumber--;
                menu[0].color = new Color(1, 1f, 0.2f, 1f);
                menu[1].color = new Color(1, 1, 1, 0.5f);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if(menuNumber < 1)
            {
                menuNumber++;
                menu[0].color = new Color(1, 1, 1, 0.5f);
                menu[1].color = new Color(1, 1f, 0.2f, 1f);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            switch (menuNumber)
            {
                case 0:
                    SceneManager.LoadScene(1);
                    break;
                case 1:
                    Application.Quit();
                    break;
                default:
                    break;
            }
        }
    }
}
