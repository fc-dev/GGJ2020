﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour {

    public SerializableEvent[] afterDialogueEvent;

    public string[] dialogues;

    private int index = 0;

    private void Start() {
        //if this gameobject has a collider it means it's an NPC
        if (!GetComponent<Collider2D>()) {
            //if it doesn't it mean this is a random dialogue attached on nothing, so i'll assume this is a level-starting dialogue
            Player.player.dialogue = this;
        }

    }


    public string next {
        get {
            if (index < dialogues.Length) {
                return dialogues[index++];
            }
            foreach (SerializableEvent evt in afterDialogueEvent) {
                evt.Invoke();
            }
            Reset();
            return null;
        }
    }

    public void Reset() {
        index = 0;
    }

    public void changeWeapon(String weapon) {
        Player.player.weapon = (Player.Weapon) Enum.Parse(typeof(Player.Weapon), weapon) ;
    }

    public void loadScene(string sceneName, string spawnPoint = "spawn") {
        Game.loadScene(sceneName, spawnPoint);
    }

    public void playSound(AudioClip clip) {
        GameObject sound = new GameObject();
        AudioSource source = sound.AddComponent<AudioSource>();
        source.clip = clip;
        source.Play();
    }

    public void playSound(String clip) {
        GameObject sound = new GameObject();
        AudioSource source = sound.AddComponent<AudioSource>();
        source.clip = Resources.Load(clip) as AudioClip;
        source.Play();
    }

    public void DestroyObject(UnityEngine.Object obj) {
        GameObject.Destroy(obj);
    }

    public void playerDestroy() {
        GameObject.Destroy(Player.gameObject);
        Player.player = null;
        Player.gameObject = null;
    }

    public void loadSceneAfterDelay(string sceneName, int delay) {
        StartCoroutine(LoadSceneCoroutine(sceneName, delay));
    }

    IEnumerator LoadSceneCoroutine(string sceneName, int delay) {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }


}