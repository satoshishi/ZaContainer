using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using ZaCo.Core;

namespace ZaCo.Helper
{
    public abstract class InstallDecorator : MonoBehaviour,IInstaller,IDisposable
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

        public void Dispose()
        {
            Destroy(this.gameObject);
        }        

        public abstract ZaContainer Install(ZaContainer container);
    }
}