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

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI messageText;

    void Start()
    {
        nameText.text = message.User.DisplayName;
        messageText.text = message.Message;

        if (destroyAfterSeconds > 0f)
            StartCoroutine(DestroyTimer());
    }

    IEnumerator DestroyTimer()
    {
        yield return new WaitForSeconds(destroyAfterSeconds);
        Destroy(gameObject);
    }
}
