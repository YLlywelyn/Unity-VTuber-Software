using System;
using System.Collections.Generic;

namespace TwitchChatConnect.Data
{
    public class TwitchChatMessage
    {
        private static string MESSAGE_HIGHLIGHTED = "highlighted-message";

        private string _idMessage;

        public TwitchUser User { get; }
        public string Message { get; }
        public int Bits;

        public string MessageWithoutEmotes
        {
            get
            {
                if (EmoteOnly) return Message;

                string text = Message;

                if (Emotes.Count > 0)
                {
                    foreach (System.Tuple<TwitchEmote, int, int> emote in Emotes)
                    {
                        text = text.Remove(emote.Item2, (emote.Item3 - emote.Item2) + 1);
                    }
                }

                return text;
            }
        }

        public bool IsHighlighted => _idMessage == MESSAGE_HIGHLIGHTED;

        public bool EmoteOnly { get; }
        public List<Tuple<TwitchEmote, int, int>> Emotes { get; } = new List<Tuple<TwitchEmote, int, int>>();

        public TwitchChatMessage(TwitchUser user, string message, int bits, string idMessage = "")
            : this(user, message, bits, new List<Tuple<TwitchEmote, int, int>>(), idMessage) { }
        public TwitchChatMessage(TwitchUser user, string message, int bits, List<Tuple<TwitchEmote, int, int>> emotes, string idMessage = "", bool emoteOnly = false)
        {
            Message = message;
            User = user;
            Bits = bits;
            _idMessage = idMessage;
            Emotes = emotes;
            EmoteOnly = emoteOnly;
        }
    }
}