using Huida81.Core.Enums;
using Huida81.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Huida81.Core.Contracts.Services
{
    public interface IWin32InfoService
    {
        Task<ICollection<Win32Process>> GetWin32ProcessDataAsync();

        Task<ICollection<Win32Info>> GetWin32InfoDataAsync(Win32InfoKey key);
    }
}
