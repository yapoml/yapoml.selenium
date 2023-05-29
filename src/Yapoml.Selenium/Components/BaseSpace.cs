using OpenQA.Selenium;
using Yapoml.Framework.Options;

namespace Yapoml.Selenium.Components
{
    public abstract class BaseSpace<TRootSpace, TParentSpace> : BaseSpace
    {
        protected TRootSpace _rootSpace;

        protected TParentSpace _parentSpace;

        public BaseSpace(TRootSpace rootSpace, TParentSpace parentSpace, IWebDriver webDriver, ISpaceOptions spaceOptions)
            : base(webDriver, spaceOptions)
        {
            _rootSpace = rootSpace;
            _parentSpace = parentSpace;
            
        }
    }

    public abstract class BaseSpace
    {
        protected IWebDriver _webDriver;

        protected ISpaceOptions _spaceOptions;

        protected BaseSpace(IWebDriver webDriver, ISpaceOptions spaceOptions)
        {
            _webDriver = webDriver;
            _spaceOptions = spaceOptions;
        }
    }
}
