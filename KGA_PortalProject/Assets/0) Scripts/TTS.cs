using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTS : SingletonBehabiour<TTS>
{
    public AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void TTSPlay(string _guideText)
    {
        StartCoroutine(DownloadTheAudio(_guideText));
    }

    IEnumerator DownloadTheAudio(string _guideText)
    {
        string url = "https://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + _guideText + "&tl=ko";
        WWW www = new WWW(url);
        yield return www;

        audioSource.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        audioSource.Play();
    }
}
