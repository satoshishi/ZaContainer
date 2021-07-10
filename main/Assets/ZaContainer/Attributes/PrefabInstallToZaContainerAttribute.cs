using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class PrefabInstallToZaContainerAttribute : Attribute
{
    public Type type;

    public PrefabInstallToZaContainerAttribute(Type type)
    {
        this.type = type;
    }
}
