using Domain.Interfaces;

namespace Application.Interfaces
{
    public interface IDistanceCalculator
    {
        decimal Calculate(IAirport source, IAirport destiation);
    }
}
