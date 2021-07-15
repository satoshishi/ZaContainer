using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZaCo.Core
{

    public class ReadonlyZaContainer : IDisposable
    {
        private ZaContainer container;

        public ReadonlyZaContainer(ZaContainer container) => this.container = container;

        public T Get<T>() where T : class => container.Get<T>();

        public void Dispose() => container.Dispose();
    }
}