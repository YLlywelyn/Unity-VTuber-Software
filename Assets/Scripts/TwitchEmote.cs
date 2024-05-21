public class TwitchEmote
{
	private static Dictionary<string, TwitchEmote> EMOTES {get;} = new Dictionary<string, TwitchEmote>();
	
	public static implicit operator Texture2D(TwitchEmote emote) => emote.texture;
	
	public string id {get; private set;}
	public string name {get; private set;}
	public Texture2D texture {get; private set;}
	
	protected TwitchEmote(string id, string name, Texture2D texture)
	{
		this.id = id;
		this.name = name;
		this.texture = texture;
	}
	
	public static TwitchEmote GetEmoteById(string id)
	{
		if (EMOTES.ContainsKey(id))
			return EMOTES[id];
		
		else
			TwitchEmote emote = new TwitchEmote("", "", null);
			// Get emote from api
			
			EMOTES.Add(id, emote);
			return emote;
	}
}