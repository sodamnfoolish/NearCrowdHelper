namespace Api.Extensions
{
    public static class MoonStoneExtension
    {
        public static int GetContestId(this string problem)
            => Convert.ToInt32(problem.Substring(0, problem.Length - 1));

        public static string GetIndex(this string problem)
            => problem.Substring(problem.Length - 1, 1);
    }
}
