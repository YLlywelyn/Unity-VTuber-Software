using System.Collections;
using System.Collections.Generic;
using TMPro;
using TwitchChatConnect.Data;
using UnityEngine;
using UnityEngine.UI;

public class UIChatMessage : MonoBehaviour
{
    [Min(0f)]
    public float destroyAfterSeconds = 30f;
    public TwitchChatMessage message;

    [Tooltip("Height = (lineHeight * lineCount) + heightModifier")]
    public int heightModifier = 40;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;

    public RectTransform rectTransform { get { return (RectTransform)transform; } }

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        if (nameText != null )
            nameText.text = message.User.DisplayName;
        messageText.text = message.Message;

        UITypewriter uITypewriter = messageText.GetComponent<UITypewriter>();

        if (destroyAfterSeconds > 0f)
        {
            if (uITypewriter != null)
                uITypewriter.OnTypeWriterFinished += () => StartCoroutine(DestroyTimer());
            else
                StartCoroutine(DestroyTimer());
        }
    }

    private void Update()
    {
        rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, (messageText.textInfo.lineCount * messageText.textInfo.lineInfo[0].lineHeight) + heightModifier);
        //Debug.Log("line count: " + messageText.textInfo.lineCount);
    }

    IEnumerator DestroyTimer()
    {
        Debug.Log("Starting destroy timer for " + this.name);
        yield return new WaitForSeconds(destroyAfterSeconds);

        animator.SetTrigger("close");
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
