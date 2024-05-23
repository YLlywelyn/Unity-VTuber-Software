using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController2D: MonoBehaviour
{
    public Sprite normal;
    public Sprite blinking;
    public Sprite talking;
    public Sprite blinking_talking;

    private Image sprite;
    private bool isBlinking;
    private bool isTalking;

    private Queue<TwitchChatMessage> messageQueue;

    private void Start()
    {
        sprite = GetComponent<Image>();
        UpdateSprite();

        TwitchChatClient.instance.onBroadcasterMessageReceived += OnBroadcasterMessage;
    }

    private void Update()
    {
        if (messageQueue.Count > 0)
        {
            // TODO: Dequeue message and display it if one is not already being displayed
        }
    }

    private void OnBroadcasterMessage(TwitchChatMessage message)
    {
        messageQueue.Enqueue(message);
    }

    private void UpdateSprite()
    {
        if (isBlinking)
            sprite.sprite = isTalking ? blinking_talking : blinking;
        else
            sprite.sprite = isTalking ? talking : normal;
    }
}
