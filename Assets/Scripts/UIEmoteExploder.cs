using System;
using System.Collections;
using System.Collections.Generic;
using TwitchChatConnect.Client;
using UnityEngine;

public class UIEmoteExploder : MonoBehaviour
{
    public ParticleSystem particlePrefab;

    private void Start()
    {
        TwitchChatClient.instance.onTwitchMessageEmotes += OnTwitchEmoteMessage;
    }

    private void OnTwitchEmoteMessage(string emoteID)
    {
        StartCoroutine(EmoteExplosion(emoteID));
    }

    private IEnumerator EmoteExplosion(string emoteID)
    {
        Texture2D tex = TwitchEmote.GetEmoteById(emoteID);
        if (tex == null)
        {
            yield return TwitchEmote.GetEmoteFromAPI(emoteID);
            tex = TwitchEmote.GetEmoteById(emoteID);
        }

        ParticleSystem particles = Instantiate(particlePrefab, transform);
        ParticleSystemRenderer particleRenderer = particles.GetComponent<ParticleSystemRenderer>();

        particleRenderer.material.mainTexture = tex;

        particles.Play();
    }
}
