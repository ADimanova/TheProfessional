using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Professional.Web.Infrastructure.Mappings
{
    using AutoMapper;
    public interface IHaveCustomMappings
    {
        void CreateMappings(IConfiguration configuration);
    }
}
