using System;
using System.Collections.Generic;

#nullable disable

namespace EcomzExercise.Data.Models
{
    public partial class Bug
    {
        public int Id { get; set; }
        public string ErrorSource { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStacktrace { get; set; }
        public string ErrorInnerException { get; set; }
        public string ErrorTargetSite { get; set; }
    }
}
