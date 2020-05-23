using System.Threading.Tasks;

namespace Demo.Core.Services
{
    public interface IValidatorService
    {
        Task ValidateAsync<T>(T obj);
    }
}