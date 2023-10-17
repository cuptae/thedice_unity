using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class FirstTouch : MonoBehaviour
{
    private Text text;
    public Button GooleLogin;

    public float blinkSpeed = 1.0f; // 깜빡거림 속도 (초당 알파값 변화)

    private bool isFadingOut = false; // 알파값을 서서히 줄이는 중인지 여부
    private bool isblink = true;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(BlinkText());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            isblink = false;
            text.gameObject.SetActive(false);
            GooleLogin.gameObject.SetActive(true);
        }
    }

    IEnumerator BlinkText()
    {
        while (isblink)
        {
            float targetAlpha = isFadingOut ? 0f : 1f;
            float currentAlpha = text.color.a;

            currentAlpha = Mathf.MoveTowards(currentAlpha, targetAlpha, blinkSpeed * Time.deltaTime);

            Color newColor = text.color;
            newColor.a = currentAlpha;
            text.color = newColor;

            if (currentAlpha == 0f || currentAlpha == 1f)
            {
                isFadingOut = !isFadingOut;
            }

            yield return null;
        }
    }
}
