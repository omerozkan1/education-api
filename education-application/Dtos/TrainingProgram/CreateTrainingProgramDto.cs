using education_domain;

namespace education_application
{
    public class CreateTrainingProgramDto
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TrainingStatus Status { get; set; }
    }
}
