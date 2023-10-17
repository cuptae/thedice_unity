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

    public float blinkSpeed = 1.0f; // �����Ÿ� �ӵ� (�ʴ� ���İ� ��ȭ)

    private bool isFadingOut = false; // ���İ��� ������ ���̴� ������ ����
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
