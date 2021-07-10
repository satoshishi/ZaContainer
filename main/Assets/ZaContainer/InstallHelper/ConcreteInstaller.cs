using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using System;

namespace ZaCo.Helper
{
    public class ConcreteInstaller : IInstaller,IDisposable
    {
        private InstallHelper.InstallHelperCallback OnInstalled;

        public ConcreteInstaller(InstallHelper.InstallHelperCallback onInstalled)
        {
            OnInstalled = onInstalled;
        }

        public void Handle(ZaContainer container)
        {
            var _readonly = container.Build();
            OnInstalled(_readonly);
        }

        public void Dispose()
        {
            OnInstalled = null;
        }
    }
}