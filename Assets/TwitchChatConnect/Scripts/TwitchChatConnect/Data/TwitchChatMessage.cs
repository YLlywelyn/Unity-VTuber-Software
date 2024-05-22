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

        public bool IsHighlighted => _idMessage == MESSAGE_HIGHLIGHTED;

        public List<Tuple<TwitchEmote, int, int>> Emotes { get; } = new List<Tuple<TwitchEmote, int, int>>();

        public TwitchChatMessage(TwitchUser user, string message, int bits, string idMessage = "") : this(user, message, bits, new List<Tuple<TwitchEmote, int, int>>(), idMessage) { }
        public TwitchChatMessage(TwitchUser user, string message, int bits, List<Tuple<TwitchEmote, int, int>> emotes, string idMessage = "")
        {
            Message = message;
            User = user;
            Bits = bits;
            _idMessage = idMessage;
            this.Emotes = emotes;
        }
    }
}