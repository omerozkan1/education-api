using education_infrastructure.Dtos;
using education_infrastructure.Extensions;

namespace education_application
{
    public interface ITrainingProgramService
    {
        Task<GenericResponse<List<GetAllTrainingProgramDto>>> GetAllAsync();
        Task<GenericResponse<NoContentDto>> CreateAsync(CreateTrainingProgramDto createTrainingDto);
    }
}
