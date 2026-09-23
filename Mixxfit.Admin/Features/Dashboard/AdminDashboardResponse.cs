using Mixxfit.Admin.Common;
using Mixxfit.Admin.Common.Enums;

namespace Mixxfit.Admin.Features.Dashboard
{
    public record AdminDashboardResponse
    {
        public int TotalUsers { get; init; }
        public int TotalExercises { get; init; }
        public int TotalWorkouts { get; init; }
        public ExerciseType? MostCommonExerciseType { get; init; }
        public int TotalWeightEntries { get; init; }
        public double? AverageUserAge { get; init; }
    }
}
