namespace education_domain
{
    public class Training
    {
        public int Id { get; set; }
        public int TrainingProgramId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }

        public virtual TrainingProgram TrainingProgram { get; set; }
    }
}
