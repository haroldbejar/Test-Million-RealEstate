using OwnerService.Domain.ValueObjects;
using System.Threading.Tasks;

namespace OwnerService.Domain.Services
{
    public interface IOwnerCodeGenerator
    {
        Task<OwnerCode> GenerateAsync();
    }
}
