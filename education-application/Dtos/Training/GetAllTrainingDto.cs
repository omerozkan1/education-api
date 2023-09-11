namespace education_application
{
    public class GetAllTrainingDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public GetAllTrainingProgramDto TrainingProgram { get; set; }
    }
}
