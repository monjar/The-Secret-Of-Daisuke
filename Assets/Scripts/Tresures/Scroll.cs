using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public ParticleSystem particleSystem;

    [SerializeField] private string hintText;
    void Start()
    {
        print("HIIIIIIII");
        particleSystem.Play();
        
        AudioManager.instance.Play("Jingle2");
        SeperateTextLines();
    }

    private void SeperateTextLines()
    {
        var finalString = "";
        var hintStringSplit = hintText.Split('_');
        foreach (var str in hintStringSplit)
        {
            finalString += (str + "\n");
        }

        hintText = finalString;
    }

    private void OnTriggerEnter(Collider other)
    {
        
        print(hintText);
        FindObjectOfType<HUD>().AddClue(hintText);
        AudioManager.instance.Play("Pickup1");
        StartCoroutine(DestroyAfter(1));
        
    }

    private IEnumerator DestroyAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }
}
