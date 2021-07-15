using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class InstallGameObjectToZaContainerAttribute : Attribute
{
    public Type type;

    public InstallGameObjectToZaContainerAttribute(Type type)
    {
        this.type = type;
    }
}
