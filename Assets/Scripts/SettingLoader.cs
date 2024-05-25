using Spout;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SettingLoader : MonoBehaviour
{
    public SettingsData settings { get; private set; }

    string configPath { get { return Path.Combine(Application.dataPath, "config.json"); } }

    public SpoutReceiver spoutReceiver;
    public AvatarController2D avatarController;

    void Start()
    {
        if (!File.Exists(configPath))
        {
            using (StreamWriter file = new StreamWriter(configPath, false))
            {
                settings = new SettingsData();
                file.Write(JsonUtility.ToJson(settings));
            }
        }
        else
        {
            using (StreamReader file = new StreamReader(configPath))
            {
                settings = JsonUtility.FromJson<SettingsData>(file.ReadToEnd());
            }
        }

        spoutReceiver.sharingName = settings.spout_source_name;

        avatarController.idle = LoadSpriteFromFile(settings.idle_texture);
        avatarController.blinking = LoadSpriteFromFile(settings.blinking_texture);
        avatarController.talking = LoadSpriteFromFile(settings.talking_texture);
        avatarController.blinking_talking = LoadSpriteFromFile(settings.talking_blinking_texture);
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

        throw new IOException("Error loading sprite \"" + path + "\"");
    }
}

[System.Serializable]
public class SettingsData
{
    public string spout_source_name = "Any";

    public string idle_texture = "";
    public string blinking_texture = "";
    public string talking_texture = "";
    public string talking_blinking_texture = "";
}
