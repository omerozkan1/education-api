using education_api.Base;
using education_application;
using Microsoft.AspNetCore.Mvc;

namespace education_api.Controllers
{
    [Route("api/trainingPrograms")]
    [ApiController]
    public class TrainingProgramsController : BaseController
    {
        private readonly ITrainingProgramService _trainingProgramService;
        public TrainingProgramsController(ITrainingProgramService trainingProgramService)
        {
            _trainingProgramService = trainingProgramService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _trainingProgramService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingProgramDto createTrainingProgramDto)
        {
            return CreateActionResultInstance(await _trainingProgramService.CreateAsync(createTrainingProgramDto));
        }
    }
}
