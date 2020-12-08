using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    public string treasureName;
    private Player _player;
    private HUD _hud;
    private void Start()
    {
        _player = FindObjectOfType<Player>();
        _hud = FindObjectOfType<HUD>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _hud.OpenToast("You found\n\""+ treasureName+"\"");
        _player.AddTreasure(treasureName);
        Destroy(gameObject);
    }
}
