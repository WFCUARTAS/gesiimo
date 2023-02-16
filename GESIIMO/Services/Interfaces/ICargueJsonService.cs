using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GESIIMO.Model;

namespace GESIIMO.Services
{
    public interface ICargueJsonService
    {
        Task<int> SetCargueJson(CargueJson carguejson);
    }
}
