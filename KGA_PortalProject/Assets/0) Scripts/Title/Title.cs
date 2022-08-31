using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{

    [Header("Menu1")]
    [SerializeField] GameObject menu1;
    [SerializeField] Text[] menu1Text;
    [SerializeField] Transform menu1Cam;


    [Header("Menu2")]
    [SerializeField] GameObject menu2;
    [SerializeField] Transform menu2Cam;
    [SerializeField] Text playerNameInput;

    int nowMenu;
    int menuNumber;
    Transform mainCam;

    void Start()
    {
        GameManager.Instance.playerName = "";

        nowMenu = 0;

        mainCam = Camera.main.gameObject.transform;
        mainCam.position = menu1Cam.position;
        mainCam.rotation = menu1Cam.rotation;

        menuNumber = 0;
        menu1Text[0].color = new Color(1, 1f, 0.2f, 1f);
        menu1Text[1].color = new Color(1, 1, 1, 0.5f);
    }

    void Update()
    {
        SelectMenu();



    }

    void SelectMenu()
    {
        switch (nowMenu)
        {
            case 0:
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (0 < menuNumber)
                    {
                        menuNumber--;
                        menu1Text[0].color = new Color(1, 1f, 0.2f, 1f);
                        menu1Text[1].color = new Color(1, 1, 1, 0.5f);
                    }
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (menuNumber < 1)
                    {
                        menuNumber++;
                        menu1Text[0].color = new Color(1, 1, 1, 0.5f);
                        menu1Text[1].color = new Color(1, 1f, 0.2f, 1f);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    if (GameManager.Instance.playerName == "") return;

                    switch (menuNumber)
                    {
                        case 0:
                            ChangeMenu();
                            break;
                        case 1:
                            Application.Quit();
                            break;
                        default:
                            break;
                    }
                }
                break;

            case 1:
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    GameManager.Instance.playerName = playerNameInput.text;
                    StartCoroutine("LoadSceneCoroutine");

                }
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    ChangeMenu();
                }
                break;
        }
    }

    bool isChanging;

    void ChangeMenu()
    {
        if (isChanging) return;

        isChanging = true;
        switch (nowMenu)
        {
            case 0:
                nowMenu = 1;
                StartCoroutine(MenuCoroutine(menu1Cam, menu2Cam));
                menu1.SetActive(false);
                menu2.SetActive(true);
                break;
            case 1:
                nowMenu = 0;
                StartCoroutine(MenuCoroutine(menu2Cam, menu1Cam));
                break;
        }
    }

    IEnumerator MenuCoroutine(Transform a, Transform b)
    {
        float lerpvalue = 0;
        while(lerpvalue < 1)
        {
            lerpvalue += 1.5f * Time.deltaTime;
            mainCam.position = Vector3.Lerp(a.position, b.position, lerpvalue);
            mainCam.rotation = Quaternion.Euler(Vector3.Lerp(a.rotation.eulerAngles, b.rotation.eulerAngles, lerpvalue));
            yield return new WaitForSeconds(Time.deltaTime);
        }
        if(nowMenu == 0)
        {
            menu1.SetActive(true);
            menu2.SetActive(false);
        }
        isChanging = false;
    }

    IEnumerator LoadSceneCoroutine()
    {
        TTS.Instance.TTSPlay(GameManager.Instance.playerName + "님 안녕하세요. 프로젝트를 시작하겠습니다.");

        while(TTS.Instance.audioSource.isPlaying)
        {
            yield return new WaitForSeconds(0.5f);
        }

        SceneManager.LoadScene(1);
    }
}
