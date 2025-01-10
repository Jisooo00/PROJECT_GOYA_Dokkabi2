using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextEffect : MonoBehaviour
{
    public TMP_Text context;

    public enum eEffect
    {
        Twinkle,  // 텍스트가 깜빡이다가 사라지는 효과
        Fall      // 텍스트가 아래로 떨어지며 서서히 사라지는 효과
    }

    public eEffect effectType;

    private float duration = 0.5f;  // 기본 지속 시간
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.localPosition;
        context.alpha = 0f;
    }

    public void PlayEffect()
    {
        switch (effectType)
        {
            case eEffect.Twinkle:
                StartCoroutine(TwinkleEffect());
                break;
            case eEffect.Fall:
                StartCoroutine(FallEffect());
                break;
        }
    }

    public void SetText(string text)
    {
        context.text = text;
    }

    private System.Collections.IEnumerator TwinkleEffect()
    {
        context.alpha = 1f;
        yield return new WaitForSeconds(duration);
        context.alpha = 0f;
    }

    private System.Collections.IEnumerator FallEffect()
    {
        float elapsedTime = 0f;
        Vector3 targetPosition = initialPosition + new Vector3(0, -5f, 0);

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            context.alpha = alpha;
            transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / duration);
            yield return null;
        }
    }
}