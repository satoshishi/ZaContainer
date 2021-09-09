using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZaCo.Core;
using System;
using System.Linq;

namespace ZaCo.Helper
{
    public class HierarchyMonoBehaviourInstaller : InstallDecorator
    {
        [SerializeField]
        private List<InstallMonoBehaviourInfo> targets;

        public override ZaContainer Install(ZaContainer container)
        {
            return container.RegistMonoBehaviour(targets);
        }
    }
}