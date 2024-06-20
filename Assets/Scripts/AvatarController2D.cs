using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
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
    public float blinkLength = 0.5f;

    private bool isBlinking;
    private bool isTalking;

    private bool canBlink { get { return (blinking_sprite != null) && (blinking_talking_sprite  != null); } }

    private Queue<TwitchChatMessage> messageQueue = new Queue<TwitchChatMessage>();

    public UIChatMessage speechBubblePrefab;
    private UIChatMessage speechBubble;

    public TTSManager ttsManager;

    [Min(0.01f)]
    public float spriteMovementSpeed = 1.0f;
    public float spriteMovementScale = 1f;

    Vector2 spriteTarget;
    public float minDistanceToNewTarget = 0.05f;
    public float distanceThreshold = 0.05f;

    public RectTransform rectTransform { get { return (RectTransform)transform; } }

    private void Start()
    {
        if (canBlink)
            StartCoroutine(Blink());

        UpdateSprite();

        spriteTarget = GetRandomTarget();

        TwitchChatClient.instance.onBroadcasterMessageReceived += OnBroadcasterMessage;
    }

    private void Update()
    {
        if (isTalking && speechBubble == null)
        {
            isTalking = false;
        }

        if (messageQueue.Count > 0 && speechBubble == null)
        {
            TwitchChatMessage message = messageQueue.Dequeue();

            speechBubble = Instantiate(speechBubblePrefab, transform);
            speechBubble.message = message;

            ttsManager.SynthesizeAndPlay(message.MessageWithoutEmotes);
            
            isTalking = true;
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

    private IEnumerator Blink()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minBlinkTime, maxBlinkTime));
            isBlinking = true;
            yield return new WaitForSeconds(blinkLength);
            isBlinking = false;
        }
    }
}
