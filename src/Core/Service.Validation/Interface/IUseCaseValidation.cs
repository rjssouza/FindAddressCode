using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Validation.Interface
{
    public interface IUseCaseValidation<TEntry> : IDisposable
        where TEntry : class
    {
        void Validade(TEntry? entry);
    }
}