using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using ZaCo.Core;

namespace ZaCo.Helper
{
    public static class ZaCoExtensions
    {
        public static ZaContainer RegistGameObject(this ZaContainer container, IEnumerable<GameObject> targets)
        {
            foreach (GameObject target in targets)
            {
                var targetScripts = target.GetComponents<MonoBehaviour>().Select(mono => mono.GetType());

                var attributes = targetScripts
                    .Where(scirpt => scirpt.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).Any())
                    .Select(scirpt => (scirpt.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).OfType<InstallGameObjectToZaContainerAttribute>().First()));

                attributes.ToList().ForEach(
                    attribute =>
                    {
                        var targetType = attribute.type;
                        container.Register(targetType, target.GetComponent(targetType));
                    });
            }

            return container;
        }

        public static ZaContainer RegistPrefab(this ZaContainer container, IEnumerable<GameObject> targets, Transform prefabRoot)
        {
            List<GameObject> instantiated = new List<GameObject>();
            foreach(GameObject target in targets)
            {
                var instance = GameObject.Instantiate(target,prefabRoot);
                instantiated.Add(instance);
            }

            return container.RegistGameObject(instantiated);
        }
    }
}