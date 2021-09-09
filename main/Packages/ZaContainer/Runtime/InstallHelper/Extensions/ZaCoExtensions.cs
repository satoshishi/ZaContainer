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
        public static ZaContainer RegistMonoBehaviour(this ZaContainer container, IEnumerable<InstallMonoBehaviourInfo> targets)
        {
            foreach (InstallMonoBehaviourInfo info in targets)
            {
                var targetScript = info.target.GetType();

                var attributes = targetScript.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).OfType<InstallGameObjectToZaContainerAttribute>();
                if(attributes == null)
                    continue;

                attributes.ToList().ForEach(
                    attribute =>
                    {
                        var targetType = attribute.type;
                        container.Register(targetType, info.target.GetComponent(targetScript), info.id);
                    });
            }

            return container;
        }        

        public static ZaContainer RegistGameObject(this ZaContainer container, IEnumerable<InstallGameObjectInfo> targets)
        {
            foreach (InstallGameObjectInfo info in targets)
            {
                var targetScripts = info.target.GetComponents<MonoBehaviour>().Select(mono => mono.GetType());

                var attributes = targetScripts
                    .Where(scirpt => scirpt.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).Any())
                    .Select(scirpt => (scirpt.GetCustomAttributes(typeof(InstallGameObjectToZaContainerAttribute), false).OfType<InstallGameObjectToZaContainerAttribute>().First()));

                attributes.ToList().ForEach(
                    attribute =>
                    {
                        var targetType = attribute.type;
                        container.Register(targetType, info.target.GetComponent(targetType), info.id);
                    });
            }

            return container;
        }

        public static ZaContainer RegistPrefab(this ZaContainer container, IEnumerable<InstallGameObjectInfo> targets, Transform prefabRoot)
        {
            List<InstallGameObjectInfo> instantiated = new List<InstallGameObjectInfo>();
            foreach(InstallGameObjectInfo info in targets)
            {
                var instance = GameObject.Instantiate(info.target,prefabRoot);
                instantiated.Add(new InstallGameObjectInfo()
                {
                    target = instance,
                    id = info.id
                });
            }

            return container.RegistGameObject(instantiated);
        }
    }
}