using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Image ImgFade;
    bool ending;
    bool realEnd;

    void Start()
    {
        StartCoroutine("StartScene");
        realEnd = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Moon")
        {
            if (realEnd) return;
            StartCoroutine("ChangeEndingScene");
        }
    }

    IEnumerator ChangeEndingScene()
    {
        realEnd = true;

        particle.Play();

        yield return new WaitForSeconds(3f);
        
        GameManager.Instance.isGameClear = true;

        float fadeValue = 0;

        while (fadeValue <= 1)
        {
            ImgFade.color = new Color(0, 0, 0, fadeValue);
            fadeValue += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(0);
        // 씬 변경
    }

    IEnumerator StartScene()
    {
        float fadeValue = 1;

        while (fadeValue > 0)
        {
            ImgFade.color = new Color(0, 0, 0, fadeValue);
            fadeValue -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        TTS.Instance.TTSPlay($"{GameManager.Instance.playerName}님 안녕하세요. 프로그램에 참여해 주셔서 대단히 감사드립니다. 본 프로그램에서는 한 개발자의 게임 이야기를 하려고 합니다.");
    }
}
