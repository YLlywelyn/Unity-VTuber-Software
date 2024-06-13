using Spout;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    public SettingsData settings { get; private set; }

    public string configPath = "Config/config.json";
    string _configPath { get { return Path.Combine(Application.dataPath, configPath); } }

    public SpoutReceiver spoutReceiver;
    public AvatarController2D avatarController;
    public WebcamInput hdmiInput;

    void Start()
    {
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
                settings = new SettingsData();
                file.Write(JsonUtility.ToJson(settings));
            }
        }
        else
        {
            using (StreamReader file = new StreamReader(_configPath))
            {
                settings = JsonUtility.FromJson<SettingsData>(file.ReadToEnd());
            }
        }

        spoutReceiver.sharingName = settings.spout_source_name;
        hdmiInput.Init(settings.captureCardDeviceName);

#if !UNITY_EDITOR
        avatarController.idle_sprite = LoadSpriteFromFile(settings.idle_texture);
        avatarController.blinking_sprite = LoadSpriteFromFile(settings.blinking_texture);
        avatarController.talking_sprite = LoadSpriteFromFile(settings.talking_texture);
        avatarController.blinking_talking_sprite = LoadSpriteFromFile(settings.talking_blinking_texture);
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

    public string idle_texture = "";
    public string blinking_texture = "";
    public string talking_texture = "";
    public string talking_blinking_texture = "";
}
