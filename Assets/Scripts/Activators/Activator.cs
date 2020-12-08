using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private string _activatorName;
    protected bool isActive;
    protected ActivatorsHandler ActivatorsHandler;
    
    public string ActivatorName => _activatorName;
    public bool IsActive => isActive;

    private void Awake()
    {
        _activatorName = transform.parent.name;
    }

    private void Start()
    {
        ActivatorsHandler = FindObjectOfType<ActivatorsHandler>();
        ActivatorsHandler.AddToActivators( this);
    }


    protected virtual extern void Activate();
    
    protected virtual extern void DoAction();
    protected IEnumerator DeactivateAfter(float time, List<Renderer> renderers = null)
    {
        
        yield return new WaitForSeconds(time);
        if(renderers!=null)
            foreach (var rnder in renderers)
                rnder.material.color = Color.white;
            
        isActive = false;
    }
}
