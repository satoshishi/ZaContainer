using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using System;
using System.Linq;

namespace ZaCo.Helper
{
    public class HierarchyGameObjectInstaller : InstallDecorator
    {
        [SerializeField]
        private List<GameObject> m_gameObject;

        public override ZaContainer Install(ZaContainer container)
        {
            return container.RegistGameObject(m_gameObject);
        }
    }
}