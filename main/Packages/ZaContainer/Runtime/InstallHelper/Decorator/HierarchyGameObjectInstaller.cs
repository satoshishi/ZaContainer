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
            foreach (GameObject _gameObject in m_gameObject)
            {
                var attributes = Get_InstallGameObjectToZaContainerAttribute_FromGameObject(_gameObject);
                Regist_Container_FromGameObjectAndAttribute(_gameObject, attributes,container);
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