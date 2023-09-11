using education_domain;

namespace education_application
{
    public class GetAllTrainingProgramDto
    {
        public GetAllTrainingProgramDto()
        {
            Trainings = new List<GetAllTrainingDto>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TrainingStatus Status { get; set; }
        public List<GetAllTrainingDto> Trainings { get; set; }
    }
}
