using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ZaCo.Core
{
    public class ZaContainer : IDisposable
    {
        public sealed class DependenceInfo
        {
            public string id;

            public object instance;
        }

        private Dictionary<Type, IEnumerable<DependenceInfo>> instanceDict = new Dictionary<Type, IEnumerable<DependenceInfo>>();

        public static ZaContainer Create() => new ZaContainer();

        public ReadonlyZaContainer CreateReadony() => new ReadonlyZaContainer(this);

        public void Register(Type type, object instance, string id = "")
        {
            if (!instanceDict.ContainsKey(type))
                instanceDict.Add(
                    type,
                    new List<DependenceInfo>()
                    {
                        new DependenceInfo()
                        {
                            id = id.Equals("") ? string.Empty : id,
                            instance = instance
                        }
                    });
            else
            {
                var dependences = instanceDict[type].ToList();
                dependences.Add(
                    new DependenceInfo()
                    {
                        id = id.Equals("") ? string.Empty : id,
                        instance = instance
                    });
                    
                instanceDict[type] = dependences;
            }
        }

        public void Register<T>(object instance, string id = "")
        {
            Register(typeof(T), instance, id);
        }

        public T Get<T>(int index = 0) where T : class
        {
            T instance = default;

            Type type = typeof(T);

            if (instanceDict.ContainsKey(type))
            {
                var dependences = instanceDict[type].ToList();
                instance = dependences[index].instance as T;
                return instance;
            }

            if (instance == null) Debug.LogWarning($"Locator: {typeof(T).Name} not found.");

            return instance;
        }

        public T Get<T>(string id) where T : class
        {
            Type type = typeof(T);

            if (instanceDict.ContainsKey(type))
            {
                var dependences = instanceDict[type];
                int index = dependences.ToList().FindIndex(d => d.id.Equals(id));

                if (index >= 0)
                    return Get<T>(index);
            }

            Debug.LogWarning($"Locator: {typeof(T).Name} not found.");
            return default;
        }

        private class ReceiverMethods
        {
            public object services;

            public IEnumerable<MethodInfo> infos;
        }

        public ReadonlyZaContainer Build()
        {
            var readOnly = CreateReadony();
            var receiverMethods = GetTargetMethodFromInstanceDict();

            foreach (ReceiverMethods methods in receiverMethods)
                NotifyToReceiverMethods(methods, readOnly);

            return readOnly;
        }

        private void NotifyToReceiverMethods(ReceiverMethods methods, ReadonlyZaContainer container)
        {
            foreach (MethodInfo info in methods.infos)
                info.Invoke(methods.services, new object[] { container });
        }

        private List<ReceiverMethods> GetTargetMethodFromInstanceDict()
        {
            List<ReceiverMethods> methods = new List<ReceiverMethods>();

            foreach (IEnumerable<DependenceInfo> info in instanceDict.Values)
            {
                var services = info.ToList().Select(i => i.instance);
                var targetMethods = services.GetType().GetMethods().Where(scirpt => scirpt.GetCustomAttributes(typeof(ReceiveZaContainerAttribute), false).Any());

                if (targetMethods != null && targetMethods.Count() > 0)
                    methods.Add(new ReceiverMethods()
                    {
                        services = services,
                        infos = targetMethods
                    });
            }

            return methods;
        }

        public void Dispose()
        {
            instanceDict.Clear();
            instanceDict = new Dictionary<Type, IEnumerable<DependenceInfo>>();
        }
    }
}