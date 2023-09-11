using education_infrastructure.Dtos;
using education_infrastructure.Extensions;

namespace education_application
{
    public interface ITrainingService
    {
        Task<GenericResponse<List<GetAllTrainingDto>>> GetAllAsync();
        Task<GenericResponse<NoContentDto>> CreateAsync(CreateTrainingDto createTrainingDto);
    }
}
