using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DebugScript : MonoBehaviour
{
#if UNITY_EDITOR

    [SerializeField] bool isFrameCheck;

    float deltaTime = 0.0f;
    GUIStyle style;
    Rect rect; float msec;
    float fps; float worstFps = 100f;
    string text;

    void Awake()
    {
        int w = Screen.width, h = Screen.height;
        rect = new Rect(0, 0, w, h * 4 / 100);
        style = new GUIStyle(); style.alignment = TextAnchor.UpperLeft;
        style.fontSize = h * 4 / 100;
        style.normal.textColor = Color.cyan;
        StartCoroutine("worstReset");
    }

    IEnumerator worstReset()
    {
        while (true)
        {
            yield return new WaitForSeconds(15f);
            worstFps = 100f;
        }
    }
    void Update() 
    { 
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;

        if (Input.GetKeyDown(KeyCode.Z))
        {
            UnityEngine.Debug.Log("½º¼¦¿Ï·á");
            ScreenCapture.CaptureScreenshot("wow.png");
        }
    }

    void OnGUI()
    {
        if (!isFrameCheck) return;
        msec = deltaTime * 1000.0f; fps = 1.0f / deltaTime;
        if (fps < worstFps)
            worstFps = fps; text = msec.ToString("F1") + "ms (" + fps.ToString("F1") + ") //worst : " + worstFps.ToString("F1"); GUI.Label(rect, text, style);
    }

    #endif
}
