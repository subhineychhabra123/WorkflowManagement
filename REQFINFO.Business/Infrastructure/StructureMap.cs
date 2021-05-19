using System;
using StructureMap;
using StructureMap.Configuration.DSL;
using REQFINFO.Business;
using REQFINFO.Business.Interfaces;
using REQFINFO.Repository.Infrastructure;
using REQFINFO.Repository.Infrastructure.Contract;

namespace REQFINFO.Business.Infrastructure
{
    public class BusinessRegistry : Registry
    {
        public BusinessRegistry()
        {
            For<IUnitOfWork>().Use<UnitOfWork>();
            //For<IUserBusiness>().Use<UserBusiness>();
        }

    }
}
