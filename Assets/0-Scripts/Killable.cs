﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Killable : MonoBehaviour
{
    [Range(1,10)]
    public int hp = 1;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.transform.parent == transform) return;
        checkHit(collision.collider);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.transform.parent == transform) return;
        checkHit(collision);
    }

    protected virtual bool checkHit(Collider2D collision) {
        bool output = false;
        if (collision.tag == "Damage" && gameObject.layer != collision.gameObject.layer) {
            hp--;
            output = true;
            Debug.Log(gameObject.name + " has been hit by "+ collision.name);
            //collision.enabled = false;
            if (hp == 0) {
                Debug.Log(gameObject + " dead!");

                //triggering death animation
                Animator animator = GetComponent<Animator>();
                if (animator) {
                    //checking if animator has die animation
                    if (Game.AnimatorHasParameter(animator, "die")) {
                        animator.SetTrigger("die");
                        Destroy(gameObject, 1.5f);

                        //disabling all scripts
                        MonoBehaviour[] scripts = gameObject.GetComponents<MonoBehaviour>();
                        foreach (MonoBehaviour script in scripts) {
                            script.enabled = false;
                        }

                        return output;
                    }
                }

                //destroying instantly if it wasn't possible to trigger death animation
                Destroy(gameObject);
            }
        }

        return output;
    }

}
