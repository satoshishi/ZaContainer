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
        private List<GameObject> m_prefabs;

        [SerializeField]
        private Transform m_prefabRoot;

        public override ZaContainer Install(ZaContainer container)
        {
            foreach (GameObject prefab in m_prefabs)
            {
                var target = Instantiate(prefab, m_prefabRoot);
                var attributes = Get_InstallGameObjectToZaContainerAttribute_FromGameObject(target);
                Regist_Container_FromGameObjectAndAttribute(target, attributes,container);
            }

            return container;
        }

        protected IEnumerable<InstallGameObjectToZaContainerAttribute> Get_InstallGameObjectToZaContainerAttribute_FromGameObject(GameObject target)
        {
            var targetScripts = target.GetComponents<MonoBehaviour>().Select(mono => mono.GetType());
            var attributes = targetScripts
                    .Where(scirpt => scirpt.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).Any())
                    .Select(scirpt => (scirpt.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).OfType<InstallGameObjectToZaContainerAttribute>().First()));

            return attributes;
        }

        protected void Regist_Container_FromGameObjectAndAttribute(GameObject target, IEnumerable<InstallGameObjectToZaContainerAttribute> attributes,ZaContainer container)
        {
            attributes.ToList().ForEach(
                attribute =>
                {
                    var targetType = attribute.type;
                    container.Register(targetType, target.GetComponent(targetType));
                });
        }
    }
}