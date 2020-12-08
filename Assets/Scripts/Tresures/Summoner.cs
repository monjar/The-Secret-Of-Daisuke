using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : MonoBehaviour
{
    private bool _isPlayerIn = false;
    public SummonerType type = SummonerType.Stay;
    private bool _isFound = false;
    private float _playerTimeIn = 0;
    public int hitTimes = 1;
    public float playerMinStayTime;
    public ParticleSystem particleSystem;
    public GameObject treasure;
    private int _playerHitted = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (_isFound)
            return;
        if (type == SummonerType.Stay)
            _isPlayerIn = true;
        else if (type == SummonerType.Hit)
        {
            if (Input.GetKey(KeyCode.E))
            {
                _playerHitted++;
                if (_playerHitted == hitTimes)
                {
                    FindTreasure();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _isPlayerIn = false;
        _playerTimeIn = 0;
    }

    private void Update()
    {
        if (_isPlayerIn && !_isFound)
        {
            if (type == SummonerType.Stay && Input.GetKey(KeyCode.E))
            {
                _playerTimeIn += Time.deltaTime;
                if (_playerTimeIn >= playerMinStayTime)
                {
                    FindTreasure();
                }
            }
        }
    }

    private void FindTreasure()
    {
        print("FoundIt");
        AudioManager.instance.Play("Found");
        treasure.SetActive(true);
        particleSystem.Play();
        StartCoroutine(DestroyParticleAfter(3));
        _isFound = true;
    }

    private IEnumerator DestroyParticleAfter(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(particleSystem.gameObject);
    }
}

public enum SummonerType
{
    Hit,
    Stay
}