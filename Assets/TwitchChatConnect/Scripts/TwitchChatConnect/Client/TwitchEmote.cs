using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwitchChatConnect.Client;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class TwitchEmote
{
	private static Dictionary<string, TwitchEmote> EMOTES { get; } = new Dictionary<string, TwitchEmote>();

	public static implicit operator Texture2D(TwitchEmote emote) => emote.texture;
	
	public string id { get; private set; }
	// public string name { get; private set; }
	public Texture2D texture { get; private set; }
	
	protected TwitchEmote(string id, Texture2D texture)
	{
		this.id = id;
		this.texture = texture;
	}
	
	public static TwitchEmote GetEmoteById(string id)
	{
		if (EMOTES.ContainsKey(id))
			return EMOTES[id];

		return new TwitchEmote(id, null);
	}

	public static IEnumerator GetEmoteFromAPI(string id)
    {
		const string CLIENT_ID = "hhsmoewj07b21o4crvw8o4zhfx7bgr";
		const string EMOTE_FORMAT = "static";

		using (UnityWebRequest request = UnityWebRequestTexture.GetTexture(string.Format("https://static-cdn.jtvnw.net/emoticons/v2/{0}/{1}/dark/3.0", id, EMOTE_FORMAT)))
		{
			Debug.Log("Emote URL: " + request.url);
			request.SetRequestHeader("Client-ID", CLIENT_ID);
			request.SetRequestHeader("Authorization", "Bearer nxselx4diwls2xgggbh40gidbzwa7n");
			yield return request.SendWebRequest();

			Texture2D tex = DownloadHandlerTexture.GetContent(request) ?? new Texture2D(1, 1);
			EMOTES.TryAdd(id, new TwitchEmote(id, tex));
        }
    }
}
