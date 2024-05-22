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

        public List<Tuple<TwitchEmote, int, int>> Emotes { get; } = new List<Tuple<TwitchEmote, int, int>>();

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
                foreach (Capture capture in match.Groups[2].Captures)
                {
                    Match m = Regex.Match(capture.Value, @"([0-9]+)-([0-9]+)");
                    (int, int) position = (int.Parse(m.Groups[1].Value), int.Parse(m.Groups[2].Value));
                    Emotes.Add(new Tuple<TwitchEmote, int, int>(TwitchEmote.GetEmoteById(match.Groups[1].Value), position.Item1, position.Item2));
                }
            }
        }
    }
}