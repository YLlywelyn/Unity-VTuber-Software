using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using UnityEngine.UI;

public class AvatarController2D: MonoBehaviour
{
    public Image sprite;

    public Sprite idle_sprite;
    public Sprite blinking_sprite;
    public Sprite talking_sprite;
    public Sprite blinking_talking_sprite;

    public float minBlinkTime;
    public float maxBlinkTime;

    private bool isBlinking;
    private bool isTalking;

    private Queue<TwitchChatMessage> messageQueue = new Queue<TwitchChatMessage>();

    public UISpeechBubble speechBubblePrefab;
    private UISpeechBubble speechBubble;

    [Min(0.01f)]
    public float spriteMovementSpeed = 1.0f;
    public float spriteMovementScale = 1f;
    Vector2 spriteTarget;
    public float minDistanceToNewTarget = 0.05f;
    public float distanceThreshold = 0.05f;

    public RectTransform rectTransform { get { return (RectTransform)transform; } }

    private void Start()
    {
        UpdateSprite();

        spriteTarget = GetRandomTarget();

        TwitchChatClient.instance.onBroadcasterMessageReceived += OnBroadcasterMessage;
    }

    private void Update()
    {
        if (messageQueue.Count > 0 && speechBubble == null)
        {
            speechBubble = Instantiate(speechBubblePrefab, transform);
            speechBubble.message = messageQueue.Dequeue();
        }

        UpdateSprite();

        if (Vector2.Distance(rectTransform.anchoredPosition, spriteTarget) <= distanceThreshold)
            spriteTarget = GetRandomTarget();
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, spriteTarget, Time.deltaTime * spriteMovementSpeed);
    }

    Vector2 GetRandomTarget()
    {
        Vector2 newTarget = new Vector2(Random.Range(-spriteMovementScale, spriteMovementScale),
                                        Random.Range(-spriteMovementScale, spriteMovementScale));
        while (Vector2.Distance(rectTransform.anchoredPosition, newTarget) < minDistanceToNewTarget)
            newTarget = new Vector2(Random.Range(-spriteMovementScale, spriteMovementScale),
                                    Random.Range(-spriteMovementScale, spriteMovementScale));
        return newTarget;
    }

    private void OnBroadcasterMessage(TwitchChatMessage message)
    {
        messageQueue.Enqueue(message);
    }

    private void UpdateSprite()
    {
        if (isBlinking)
            sprite.sprite = isTalking ? blinking_talking_sprite : blinking_sprite;
        else
            sprite.sprite = isTalking ? talking_sprite : idle_sprite;
    }
}
