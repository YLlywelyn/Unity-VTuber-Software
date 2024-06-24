using TwitchChatConnect.Client;
using UnityEngine;

public class TwitchChatInitializer : MonoBehaviour
{
    void Start()
    {
        InitializeChat();
        InputManager.OnRefresh += InitializeChat;
    }

    public void InitializeChat()
    {
        Debug.Log("Initialising chat!");

        TwitchChatClient.instance.Init(TwitchChatInitialized, TwitchChatInitializationError);
    }

    void TwitchChatInitialized()
    {
        Debug.Log("Twitch chat initialized!");
    }

    void TwitchChatInitializationError(string error)
    {
        Debug.LogError("Twitch chat initialization error: " + error);
    }
}
