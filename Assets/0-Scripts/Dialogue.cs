﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class Dialogue : MonoBehaviour {

    public SerializableEvent afterDialogueEvent;

    public string[] dialogues;

    private int index = 0;
    public string next {
        get {
            if (index < dialogues.Length) {
                return dialogues[index++];
            }
            afterDialogueEvent.Invoke();
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

}