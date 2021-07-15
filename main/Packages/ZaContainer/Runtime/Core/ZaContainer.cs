using System;
using System.Reflection;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ZaCo.Core
{
    public class ZaContainer
    {
        public static ZaContainer Create() => new ZaContainer();

        public ReadonlyZaContainer CreateReadony() => new ReadonlyZaContainer(this);

        private Dictionary<Type, object> instanceDict = new Dictionary<Type, object>();

        public void Register(Type type, object instance)
        {
            instanceDict[type] = instance;
        }

        public void Register<T>(object instance)
        {
            instanceDict[typeof(T)] = instance;
        }

        public T Get<T>() where T : class
        {
            T instance = default;

            Type type = typeof(T);

            if (instanceDict.ContainsKey(type))
            {
                instance = instanceDict[type] as T;
                return instance;
            }

            if (instance == null) Debug.LogWarning($"Locator: {typeof(T).Name} not found.");
 
            return instance;
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

            foreach(ReceiverMethods methods in receiverMethods)
                NotifyToReceiverMethods(methods);

            return readOnly;
        }

        private void NotifyToReceiverMethods(ReceiverMethods methods)
        {
            foreach(MethodInfo info in methods.infos)
                info.Invoke(methods.services,new object[]{CreateReadony()});
        }

        private List<ReceiverMethods> GetTargetMethodFromInstanceDict()
        {
            List<ReceiverMethods> methods = new List<ReceiverMethods>();

            foreach(object services in instanceDict.Values)
            {
                 var targetMethods = services.GetType().GetMethods().Where(scirpt => scirpt.GetCustomAttributes(typeof(ReceiveZaContainerAttribute), false).Any());

                 if(targetMethods != null && targetMethods.Count() > 0)
                    methods.Add(new ReceiverMethods()
                    {
                        services = services,
                        infos = targetMethods
                    });
            }

            return methods;
        }
    }
}