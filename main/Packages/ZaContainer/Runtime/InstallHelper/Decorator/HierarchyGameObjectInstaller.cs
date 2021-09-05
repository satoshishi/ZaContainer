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
        private List<InstallGameObjectInfo> targets;

        public override ZaContainer Install(ZaContainer container)
        {
            return container.RegistGameObject(targets);
        }
    }
}