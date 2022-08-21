using Api.DTOs;

namespace Api.Services
{
    public class MoonStoneService
    {
        private readonly HttpClient _cfApiHttpClient;

        public MoonStoneService(IConfiguration configuration)
        {
            _cfApiHttpClient = new();
            _cfApiHttpClient.BaseAddress = new(configuration["CodeforcesApiUrl"]);
            _cfApiHttpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task<IEnumerable<string>> FindHack(int contest, string problem, string test) 
        {
            try
            {
                HttpResponseMessage httpResponseMessage = await _cfApiHttpClient.GetAsync($"contest.hacks?contestId={contest}");

                var hacks = (await httpResponseMessage.Content.ReadFromJsonAsync<ContestHacksDTO>()).result;

                return hacks
                    .Where(h => h.judgeProtocol.protocol.Contains(test) && h.problem.index == problem)
                    .Select(h => h.defender.members.FirstOrDefault().handle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                return Enumerable.Empty<string>();
            }
        }
    }
}
