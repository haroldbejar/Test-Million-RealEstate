using OwnerService.Domain.Services;
using OwnerService.Domain.ValueObjects;
using System;
using System.Threading.Tasks;

namespace OwnerService.Infrastructure.Services
{
    public class OwnerCodeGenerator : IOwnerCodeGenerator
    {
        public Task<OwnerCode> GenerateAsync()
        {
            var random = new Random();
            var code = random.Next(1000, 999999);
            return Task.FromResult(new OwnerCode(code));
        }
    }
}
