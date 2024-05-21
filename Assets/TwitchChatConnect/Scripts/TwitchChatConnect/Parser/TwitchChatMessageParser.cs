using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TwitchChatConnect.Client;
using TwitchChatConnect.Data;
using TwitchChatConnect.Manager;
using UnityEngine;

namespace TwitchChatConnect.Parser
{
    public class TwitchChatMessageParser
    {
        public string Id { get; }
        public string Sent { get; }
        public TwitchUser User { get; }
        public int Bits { get; }
        public IReadOnlyList<TwitchUserBadge> Badges { get; }

        public List<Tuple<string, int, int>> Emotes { get; } = new List<Tuple<string, int, int>>();

        public string rawMessage { get; }

        public TwitchChatMessageParser(TwitchInputLine command)
        {
            rawMessage = command.Message;

            Bits = 0;

            Id = TwitchChatRegex.IdMessageRegex.Match(command.Message).Groups[1].Value;
            string badgesText = TwitchChatRegex.MessageRegex.Match(command.Message).Groups[1].Value;
            string displayName = TwitchChatRegex.MessageRegex.Match(command.Message).Groups[2].Value;
            string idUser = TwitchChatRegex.MessageRegex.Match(command.Message).Groups[3].Value;
            string username = TwitchChatRegex.MessageRegex.Match(command.Message).Groups[4].Value;
            Sent = TwitchChatRegex.MessageRegex.Match(command.Message).Groups[5].Value;

            Badges = TwitchChatRegex.BuildBadges(badgesText);
            User = TwitchUserManager.AddUser(username);
            User.SetData(idUser, displayName, Badges);

            if (Sent.Length == 0) return;

            MatchCollection matches = TwitchChatRegex.CheerRegex.Matches(Sent);
            foreach (Match match in matches)
            {
                if (match.Groups.Count != 2) continue; // First group is 'cheerXX', second group is XX.
                string value = match.Groups[1].Value;
                if (!int.TryParse(value, out int bitsAmount)) continue;
                Bits += bitsAmount;
            }

            Debug.Log(rawMessage);
            MatchCollection emoteMatches = TwitchChatRegex.EmoteRegex.Matches(command.Message);
            foreach (Match match in emoteMatches)
            {
                Debug.Log(string.Format("Emote match: \"{0}\", {1}-{2}", match.Groups[2], match.Groups[3], match.Groups[4]));
                Emotes.Add(new Tuple<string, int, int>(match.Groups[2].Value, int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value)));
            }
        }
    }
}