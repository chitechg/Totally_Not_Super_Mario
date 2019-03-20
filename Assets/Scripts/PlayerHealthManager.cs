﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{

    public enum PlayerState { little, big, fire, invincible }
    public PlayerState state;

    public GameObject player;
    public int playerHealth;

    [Header("Animation")]
    public RuntimeAnimatorController littleAnim;
    public RuntimeAnimatorController bigAnim;
    private Animator anim;

    [Header("Audio")]
    public AudioClip growSound;
    public AudioClip shrinkSound;
    public AudioClip deathSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        state = PlayerState.little;
        playerHealth = 1;
        anim = player.GetComponent<Animator>();
        audioSource = GameObject.Find("Main Camera").GetComponent<AudioSource>();
    }

    public void DamagePlayer()
    {
        //playerHealth = playerHealth -1;
        playerHealth--;

        if(playerHealth <= 0)
        {
            KillPlayer();
        }
        else
        {
            TransitionLittle();
        }

    }
    public void GiveHealth()
    {
        playerHealth = 2;
        TransitionBig();
    }

    private void TransitionBig()
    {
        state = PlayerState.big;
        audioSource.PlayOneShot(growSound);

        anim.runtimeAnimatorController = bigAnim as RuntimeAnimatorController;
    }
    private void TransitionLittle()
    {
        state = PlayerState.little;
        audioSource.PlayOneShot(shrinkSound);

        anim.runtimeAnimatorController = littleAnim as RuntimeAnimatorController;
    }
    public void KillPlayer()
    {
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        Destroy(player);
    }
}