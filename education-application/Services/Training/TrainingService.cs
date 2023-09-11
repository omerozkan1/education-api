using education_domain;
using education_infrastructure.Dtos;
using education_infrastructure.Extensions;
using education_infrastructure.StaticValues;
using System.Data;
using System.Net;

namespace education_application
{
    public class TrainingService : ITrainingService
    {
        private readonly ITrainingEfRepository _trainingEfRepository;
        public TrainingService(ITrainingEfRepository trainingEfRepository)
        {
            _trainingEfRepository = trainingEfRepository;
        }
        public async Task<GenericResponse<NoContentDto>> CreateAsync(CreateTrainingDto createTrainingDto)
        {
            var isSuccess = await _trainingEfRepository.CreateAsync(createTrainingDto.ToEntity());
            if (isSuccess)
            {
                return GenericResponse<NoContentDto>.Success(HttpStatusCode.NoContent);
            }
            return GenericResponse<NoContentDto>.Fail(ApplicationConsts.DB_CREATE_ERROR, HttpStatusCode.InternalServerError);
        }

        public async Task<GenericResponse<List<GetAllTrainingDto>>> GetAllAsync()
        {
            var trainings = await _trainingEfRepository.GetAllAsync();
            var dto = trainings?.Select(x => x.ToDto());
            return GenericResponse<List<GetAllTrainingDto>>.Success(dto?.ToList(), HttpStatusCode.OK);
        }
    }
}
