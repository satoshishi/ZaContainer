using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZaCo.Core
{

    public class ReadonlyZaContainer
    {
        private ZaContainer container;

        public ReadonlyZaContainer(ZaContainer container) => this.container = container;

        public T Get<T>() where T : class
        {
            return container.Get<T>();
        }            
    }
}