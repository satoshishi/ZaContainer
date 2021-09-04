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

        public T Get<T>(int index = 0) where T : class => container.Get<T>(index);

        public T Get<T>(string id) where T : class => container.Get<T>(id);

        public void Dispose() => container.Dispose();
    }
}