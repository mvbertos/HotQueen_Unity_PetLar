using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] AudioInGame musics;
    public void PlaySceneMusic(string scene)
    {
        //switch scene names 
        if (scene == GameManager.instance.SceneName.StartMenu)
        {
            //create a new gameobject in the scene
            //add a audio source to it 
            //add a clip to the audio source
            //play the clip
            //loop is true
            PlayAudio(musics.MainMenu, true, GameManager.instance.SceneName.StartMenu);
        }//if in game play ingame music
        else if (scene == GameManager.instance.SceneName.Game)
        {
            PlayAudio(musics.InGame, true, GameManager.instance.SceneName.Game);
        }//else if in minigame play minigame music
        else if (scene == GameManager.instance.SceneName.Adoption)
        {
            PlayAudio(musics.MiniGame, true, GameManager.instance.SceneName.Adoption);
        }//else if in minigame play minigame music

    }

    public void PlayAudio(AudioClip clip, bool loop, string scene = null)
    {
        GameObject audioSource = new GameObject("AudioSource");
        if (scene != null)
        {
            audioSource.transform.parent = SceneManager.GetSceneByName(scene).GetRootGameObjects()[0].transform;
        }
        else
        {
            audioSource.transform.parent = null;
        }
        AudioSource audioSourceComponent = audioSource.AddComponent<AudioSource>();
        audioSourceComponent.clip = clip;
        audioSourceComponent.loop = loop;
        if (!loop)
        {
            StartCoroutine(WhenAudioFinishes(clip.length, audioSource));
        }
        
        audioSourceComponent.Play();

    }

    public IEnumerator WhenAudioFinishes(float audioLenght, GameObject reference)
    {
        yield return new WaitForSeconds(audioLenght);
        Destroy(reference.gameObject);
    }
}
[System.Serializable]
public class AudioInGame
{
    public AudioClip MainMenu;
    public AudioClip InGame;
    public AudioClip GameOver;
    public AudioClip MiniGame;
}