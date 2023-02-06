﻿namespace Yapoml.Selenium.Services.Locator
{
    public interface IElementHandlerRepository
    {
        bool TryGet(string key, out IElementHandler elementHandler);

        void Set(string key, IElementHandler elementHandler);

        IElementHandlerRepository ParentRepository { get; }

        IElementHandlerRepository NestedRepository { get; }

        IElementHandlerRepository CreateNestedRepository();
    }
}
