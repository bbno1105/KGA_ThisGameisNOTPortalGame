using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    [SerializeField] ParticleSystem particle;
    [SerializeField] Image fadeImage;
    bool ending;
    bool realEnd;
    float fadeValue;

    void Start()
    {
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

        while (fadeValue <= 1)
        {
            fadeImage.color = new Color(0, 0, 0, fadeValue);
            fadeValue += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        yield return new WaitForSeconds(1f);

        GameManager.Instance.isGameClear = true;

        SceneManager.LoadScene(0);
        // ¾À º¯°æ
    }
}
