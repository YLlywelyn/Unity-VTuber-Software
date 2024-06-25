using Spout;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    public SettingsData _settings { get; private set; }
    public static SettingsData settings { get { return instance._settings; } }

#if UNITY_EDITOR
    public string configPath = "Config/config.json";
    string _configPath { get { return Path.Combine(Application.dataPath, configPath); } }
#else
    string _configPath { get { return Path.Combine(Application.dataPath, "../config.json"); } }
#endif

    public SpoutReceiver spoutReceiver;
    public AvatarController2D avatarController;
    public WebcamInput hdmiInput;

    public OpenAIWrapper openAIWrapper;

    public static SettingLoader instance { get; private set; }

    void Start()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        LoadSettings();
        InputManager.OnRefresh += LoadSettings;
    }

    void LoadSettings()
    {
        Debug.Log("Loading config!");

        if (!File.Exists(_configPath))
        {
            using (StreamWriter file = new StreamWriter(_configPath, false))
            {
                _settings = new SettingsData();
                file.Write(JsonUtility.ToJson(_settings));
            }
        }
        else
        {
            using (StreamReader file = new StreamReader(_configPath))
            {
                _settings = JsonUtility.FromJson<SettingsData>(file.ReadToEnd());
            }
        }

        spoutReceiver.sharingName = _settings.spout_source_name;
        hdmiInput.Init(_settings.captureCardDeviceName);

        openAIWrapper.SetAPIKey(_settings.openai_api_key);

#if !UNITY_EDITOR
        avatarController.idle_sprite = LoadSpriteFromFile(_settings.idle_texture);
        avatarController.blinking_sprite = LoadSpriteFromFile(_settings.blinking_texture);
        avatarController.talking_sprite = LoadSpriteFromFile(_settings.talking_texture);
        avatarController.blinking_talking_sprite = LoadSpriteFromFile(_settings.talking_blinking_texture);
#endif
    }

    public static Sprite LoadSpriteFromFile(string path, float pixelsPerUnit = 100f)
    {
        byte[] bytes;
        Texture2D tex;

        if (File.Exists(path))
        {
            bytes = File.ReadAllBytes(path);
            tex = new Texture2D(2, 2);
            if (tex.LoadImage(bytes))
                return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(tex.width/2f, tex.height/2f), pixelsPerUnit); ;
        }

        Debug.LogError("Error loading sprite \"" + path + "\"");
        return null;
    }
}

[System.Serializable]
public class SettingsData
{
    public string spout_source_name = "Any";

    public string captureCardDeviceName = "";

    public string openai_api_key = "";

    public string idle_texture = "";
    public string blinking_texture = "";
    public string talking_texture = "";
    public string talking_blinking_texture = "";
}
