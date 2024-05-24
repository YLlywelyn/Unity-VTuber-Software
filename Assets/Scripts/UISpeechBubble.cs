using System.Collections;
using System.Collections.Generic;
using TMPro;
using TwitchChatConnect.Data;
using UnityEngine;
using UnityEngine.UI;

public class UISpeechBubble : MonoBehaviour
{
    [Min(0f)]
    public float destroyAfterSeconds = 10f;
    public TwitchChatMessage message;

    public TextMeshProUGUI messageText;

    public RectTransform rectTransform { get { return (RectTransform)transform; } }

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        messageText.text = message.Message;

        if (destroyAfterSeconds > 0f)
            StartCoroutine(DestroyTimer());
    }

    private void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (messageText.textInfo.lineCount * 40) + 40);
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        animator.SetTrigger("close");
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
