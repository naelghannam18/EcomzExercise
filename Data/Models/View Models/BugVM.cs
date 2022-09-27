namespace EcomzExercise.Data.Models.View_Models
{
    public class BugVM
    {
    }

    public class BugListVM
    {
        public int Id { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStacktrace { get; set; }
        public string ErrorInnerException { get; set; }
        public string ErrorTargetSite { get; set; }
    }
}
