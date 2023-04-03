using System;
using UnityEngine;

// DO NOT USE THIS

[Obsolete]
public class HealthChange
{
    public HealthChange type;
    public float amount;
    enum HealthChangeType {
        HEALING,
        HEALING_CRITICAL,
        DAMAGE,
        DAMAGE_CRITICAL,
        NONE
    }
}