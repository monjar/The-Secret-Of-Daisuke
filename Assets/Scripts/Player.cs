using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public ParticleSystem particleSystem;
    [SerializeField] private readonly List<string> _treasures = new List<string>();
    private GameManager _gm;
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            particleSystem.Play();
            AudioManager.instance.Play("Field");
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            particleSystem.Stop();
            AudioManager.instance.Stop("Field");
        }
    }

    private void Start()
    {
        _gm = GameManager.Instance;
    }


    public void AddTreasure(string treasureName)
    {
        _treasures.Add(treasureName);
        if (_gm.DidCollectAllTreasures(_treasures))
        {
            AudioManager.instance.Play("Win");
            print("WON");
            GameManager.Instance.PlayerWin();
        }
        else
        {
            AudioManager.instance.Play("Jingle1");
        }
    }

    private void OnCollisionStay(Collision other)
    {
        AudioManager.instance.StartRoll();
    }

    private void OnCollisionEnter(Collision other)
    {
        AudioManager.instance.StartRoll();
    }

    private void OnCollisionExit(Collision other)
    {
        AudioManager.instance.StopRoll();
    }
}