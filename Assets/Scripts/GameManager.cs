using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance => _instance;

    private List<string> _treasures = new List<string>();

    private bool _didWin = false;
    private float _timePast = 0f;

    public bool DidWin => _didWin;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        var treasureObjects = new List<Treasure>(Resources.FindObjectsOfTypeAll(typeof(Treasure)) as Treasure[]);
        treasureObjects.ForEach(t => _treasures.Add(t.treasureName));
    }

    private void Update()
    {
        _timePast += Time.deltaTime;
    }

    public bool DidCollectAllTreasures(List<string> tNames)
    {
        foreach (var tName in _treasures)
        {
            if (!tNames.Contains(tName))
                return false;
        }
        return true;
    }

    public void PlayerWin()
    {
        _didWin = true;
        FindObjectOfType<HUD>().Win(_timePast/60f);
    }
}