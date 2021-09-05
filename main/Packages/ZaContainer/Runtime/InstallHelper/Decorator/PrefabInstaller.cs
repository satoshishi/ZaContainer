using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using System;
using System.Linq;

namespace ZaCo.Helper
{
    public class PrefabInstaller : InstallDecorator
    {
        [SerializeField]
        private List<InstallGameObjectInfo> m_prefabs;

        [SerializeField]
        private Transform m_prefabRoot;

        public override ZaContainer Install(ZaContainer container)
        {
            return container.RegistPrefab(m_prefabs,m_prefabRoot);;
        }
    }
}