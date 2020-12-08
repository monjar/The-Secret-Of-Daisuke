using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatorsHandler : MonoBehaviour
{

    

    private Dictionary<string, List<Activator>> _activators;

    private void Awake()
    {
        _activators = new Dictionary<string, List<Activator>>();
    }

    public void AddToActivators(Activator activator)
    {
        var activatorName = activator.ActivatorName;
        if (_activators.ContainsKey(activatorName))
        {
            _activators[activatorName].Add(activator);
        }
        else
        {
            _activators.Add(activatorName, new List<Activator>());
            _activators[activatorName].Add(activator);
        }
    }

    public bool IsAllActive(string activatorName)
    {
        foreach (var activator in _activators[activatorName])
        {
            if (!activator.IsActive)
                return false;
        }

        return true;
    }
}
