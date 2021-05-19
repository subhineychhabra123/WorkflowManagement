using REQFINFO.Business;
using REQFINFO.Business.Infrastructure;
using REQFINFO.Business.Interfaces;
using REQFINFO.Repository;
using StructureMap;
using StructureMap.Configuration.DSL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace REQFINFO.Website.Infrastructure
{
    public class StructureMapControllerFactory : DefaultControllerFactory
    {
        protected override IController
            GetControllerInstance(RequestContext requestContext,
            Type controllerType)
        {
            try
            {
                if ((requestContext == null) || (controllerType == null))
                    return null;

                return (Controller)ObjectFactory.Container.GetInstance(controllerType);
            }
            catch (StructureMapException)
            {
                System.Diagnostics.Debug.WriteLine(ObjectFactory.Container.WhatDoIHave());
                throw new Exception(ObjectFactory.Container.WhatDoIHave());
            }
        }
    }

    public static class StructureMapper
    {
        public static void Run()
        {
            ControllerBuilder.Current
                .SetControllerFactory(new StructureMapControllerFactory());

            ObjectFactory.Initialize(action =>
            {
                action.AddRegistry(new RepositoryRegistry());
                action.AddRegistry(new BusinessRegistry());

            });
        }
    }
    //public static class StructureMapper
    //{
    //    private static readonly Lazy<Container> _containerBuilder =
    //            new Lazy<Container>(defaultContainer, LazyThreadSafetyMode.ExecutionAndPublication);

    //    public static IContainer Container
    //    {
    //        get { return _containerBuilder.Value; }
    //    }

    //    private static Container defaultContainer()
    //    {
    //        return new Container(x =>
    //        {
    //            x.AddRegistry(new RepositoryRegistry());
    //            x.AddRegistry(new BusinessRegistry());
    //        });
    //    }
    //}
    public class RepositoryRegistry : Registry
    {
        public RepositoryRegistry()
        {
            For<IUserBusiness>().Use<UserBusiness>();
            For<IWorkflowBusiness>().Use<WorkflowBusiness>();
            For<IProjectBusiness>().Use<ProjectBusiness>();
            For<IGIGBusiness>().Use<GIGBusiness>();
            For<IFunctionBusiness>().Use<FunctionBusiness>();
            For<IStatusBusiness>().Use<StatusBusiness>();
            For<IUserXProjectXWorkFlowUserGroupBusiness>().Use<UserXProjectXWorkFlowUserGroupBusiness>();
            For<ICreateGIGBusiness>().Use<CreateGIGBusiness>();
            For<ICustomLookupBusiness>().Use<CustomLookupBusiness>();
            For<IUDFValidationBusiness>().Use<UDFValidationBusiness>();
            For<IUDFValidationDefinitionBusiness>().Use<UDFValidationDefinitionBusiness>();
            For<IUDFInstanceBusiness>().Use<UDFInstanceBusiness>();
            For<ILifeCycleBusiness>().Use<LifeCycleBusiness>();
            For<IAttachmentBusiness>().Use<AttachmentBusiness>();
            For<IWorkFlowUserGroupBusiness>().Use<WorkFlowUserGroupBusiness>();
            For<IContractorBusiness>().Use<ContractorBusiness>();
            For<ILevelBusiness>().Use<LevelBusiness>();
        }
    }
}