using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using System;

namespace ZaCo.Helper
{
    public class PrefabInstaller : InstallDecorator
    {
        [SerializeField]
        private List<GameObject> m_prefabs;

        public override ZaContainer Install(ZaContainer container)
        {
            return container;
        }
    }
}