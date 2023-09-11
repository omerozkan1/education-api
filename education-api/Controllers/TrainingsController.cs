using education_api.Base;
using education_application;
using Microsoft.AspNetCore.Mvc;

namespace education_api.Controllers
{
    [Route("api/trainings")]
    [ApiController]
    public class TrainingsController : BaseController
    {
        private readonly ITrainingService _trainingService;
        public TrainingsController(ITrainingService trainingService)
        {
            _trainingService = trainingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return CreateActionResultInstance(await _trainingService.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTrainingDto createTrainingDto)
        {
            return CreateActionResultInstance(await _trainingService.CreateAsync(createTrainingDto));
        }
    }
}
