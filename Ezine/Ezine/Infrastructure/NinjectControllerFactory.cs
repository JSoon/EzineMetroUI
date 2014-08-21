using System;
using System.Linq;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Ezine.Abstract;
using Ezine.Repository;

namespace Ezine.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext,
        
            Type controllerType)
        {
            return controllerType == null
            ? null
            : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IAccount>().To<AccountRepository>();
            ninjectKernel.Bind<IEzine>().To<EzineRepository>();
            ninjectKernel.Bind<ISection>().To<SectionRepository>();
            ninjectKernel.Bind<IAttachment>().To<AttachmentRepository>();
   
        }
    }
}