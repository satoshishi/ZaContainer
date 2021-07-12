using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ZaCo.Core;

namespace ZaCo.Helper
{
    public abstract class InstallDecorator : MonoBehaviour,IInstaller
    {
        protected IInstaller childInstaller;

        public void Initialize(IInstaller installer)
        {
            childInstaller = installer;
        }

        public void Handle(ZaContainer container)
        {
            childInstaller.Handle(Install(container));
        }

        public abstract ZaContainer Install(ZaContainer container);
    }
}