using Api.DTOs;
using Api.Extensions;

namespace Api.Services
{
    public class MoonStoneService
    {
        private readonly HttpClient _cfApiHttpClient;

        private const string _inputPrefix = "Input:\n";
        private const string _inputSuffix = "\r\n\n\nOutput";

        private const string _outputPrefix = "";
        private const string _outputSuffix = "";

        public MoonStoneService(IConfiguration configuration)
        {
            _cfApiHttpClient = new();
            _cfApiHttpClient.BaseAddress = new(configuration["CodeforcesApiUrl"]);
            _cfApiHttpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task<IEnumerable<string>> FindHack(string problem, string input, string? output = null) 
        {
            var contestId = problem.GetContestId();
            var index = problem.GetIndex();

            try
            {
                HttpResponseMessage httpResponseMessage = await _cfApiHttpClient.GetAsync($"contest.hacks?contestId={contestId}");

                var hacks = (await httpResponseMessage.Content.ReadFromJsonAsync<ContestHacksDTO>()).result;

                return hacks
                    .Where(h =>
                        h.judgeProtocol.protocol.Contains(_inputPrefix + input + _inputSuffix) &&
                        (output == null || h.judgeProtocol.protocol.Contains(_outputPrefix + output + _outputSuffix)) &&
                        h.problem.index == index)
                    .Select(h =>
                        h.defender.members.FirstOrDefault().handle);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw new Exception("CF API is down");
            }
        }
    }
}
