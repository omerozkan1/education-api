using education_domain;
using education_infrastructure.Dtos;
using education_infrastructure.Extensions;
using education_infrastructure.StaticValues;
using System.Net;

namespace education_application
{
    public class TrainingProgramService : ITrainingProgramService
    {
        private readonly ITrainingProgramEfRepository _trainingProgramEfRepository;
        public TrainingProgramService(ITrainingProgramEfRepository trainingProgramEfRepository)
        {
            _trainingProgramEfRepository = trainingProgramEfRepository;
        }
        public async Task<GenericResponse<NoContentDto>> CreateAsync(CreateTrainingProgramDto createTrainingProgramDto)
        {
            var isSuccess = await _trainingProgramEfRepository.CreateAsync(createTrainingProgramDto.ToEntity());
            if (isSuccess)
            {
                return GenericResponse<NoContentDto>.Success(HttpStatusCode.NoContent);
            }
            return GenericResponse<NoContentDto>.Fail(ApplicationConsts.DB_CREATE_ERROR, HttpStatusCode.InternalServerError);
        }

        public async Task<GenericResponse<List<GetAllTrainingProgramDto>>> GetAllAsync()
        {
            var trainingPrograms = await _trainingProgramEfRepository.GetAllAsync();
            var dto = trainingPrograms?.Select(x => x.ToDto());

            return GenericResponse<List<GetAllTrainingProgramDto>>.Success(dto?.ToList(), HttpStatusCode.OK);
        }
    }
}
