using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcActivator : Activator
{

    public float deactivateTime = 5f;
    public Color activeColor;
    public GameObject spawnObject;

    public List<Renderer> renderers;
    protected override void Activate()
    {
        base.Activate();
    }
    private void OnTriggerEnter(Collider other)
    {
        isActive = true;
        
        StartCoroutine(DeactivateAfter(deactivateTime, renderers));
        if (ActivatorsHandler.IsAllActive(ActivatorName))
        {
            DoAction();
        }
    }

    protected override void DoAction()
    {
        spawnObject.SetActive(true);
    }

    private void Update()
    {
        if (isActive)
        {
            foreach (var rnder in renderers)
                rnder.material.color = Color.Lerp(Color.white, activeColor, Mathf.PingPong(Time.time, 2));
        }

    }
}
