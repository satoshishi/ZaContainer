using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ZaCo.Core;

namespace ZaCo.Helper
{
    public class ScriptableObjectInstaller : InstallDecorator
    {
        [SerializeField]
        private ZaCoInstallPrefabObjects m_scriptable;

        [SerializeField]
        private Transform m_prefabRoot;

        public override ZaContainer Install(ZaContainer container)
        {
            return container.RegistPrefab(m_scriptable.prefabs,m_prefabRoot);
        }
    }
}