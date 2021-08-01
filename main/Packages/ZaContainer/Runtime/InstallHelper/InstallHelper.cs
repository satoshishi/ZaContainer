using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using System.Linq;

namespace ZaCo.Helper
{
    public class InstallHelper : MonoBehaviour
    {
        public delegate void InstallHelperCallback(ReadonlyZaContainer container);

        [SerializeField]
        private List<InstallDecorator> m_installers;

        public void AddInstaller(InstallDecorator installer) => m_installers.Add(installer);

        public void Handle(InstallHelperCallback onInstalled)
        {
            var concrete = new ConcreteInstaller(onInstalled);
            InstallDecorator currentInstaller = null;

            m_installers.Reverse();

            foreach (InstallDecorator installer in m_installers)
            {
                if (currentInstaller == null)
                    installer.Initialize(concrete);
                else installer.Initialize(currentInstaller);
                
                currentInstaller = null;
                currentInstaller = installer;
            }

            currentInstaller.Handle(ZaContainer.Create());
        }
    }
}