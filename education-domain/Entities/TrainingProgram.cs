namespace education_domain
{
    public class TrainingProgram
    {
        public TrainingProgram()
        {
            Trainings = new List<Training>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TrainingStatus Status { get; set; }

        public virtual List<Training> Trainings { get; set; }
    }
}
