using Api.Extensions;
using Api.Entities;
using Api.DTOs;

namespace Api.Services
{
    public class MoonStoneService
    {
        private readonly HttpClient _cfApiHttpClient;

        private const string _inputPrefix = "Input:\n";
        private const string _inputSuffix = "\r\n\n\nOutput";

        private const string _outputPrefix = "";
        private const string _outputSuffix = "";

        private enum _verticts
        {
            OK = 0
        };

        private const string _programmingLanguagePrefix = "GNU C++";

        public MoonStoneService(IConfiguration configuration)
        {
            _cfApiHttpClient = new();
            _cfApiHttpClient.BaseAddress = new(configuration["CodeforcesApiUrl"]);
            _cfApiHttpClient.DefaultRequestHeaders.Accept.Add(new("application/json"));
        }

        public async Task<IEnumerable<string>> FindHandlesByHack(int contestId, string index, string input, string? output = null) 
        {
            var hacks = new List<Hack>();

            try
            {
                HttpResponseMessage httpResponseMessage = await _cfApiHttpClient.GetAsync($"contest.hacks?contestId={contestId}");

                hacks = (await httpResponseMessage.Content.ReadFromJsonAsync<CFApiResponseDTO<Hack>>()).result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw new Exception("There are some problems with CF API");
            }

            return hacks
                .Where(h =>
                    h.judgeProtocol.protocol.Contains(_inputPrefix + input + _inputSuffix) &&
                    (output == null || h.judgeProtocol.protocol.Contains(_outputPrefix + output + _outputSuffix)) &&
                    h.problem.index == index)
                .Select(h =>
                    h.defender.members.FirstOrDefault().handle);
        }

        public async Task<IEnumerable<int>> FindOkSubmissionsByHack(int contestId, string index, string input, string? output = null)
        {
            var handles = (await FindHandlesByHack(contestId, index, input, output)).ToList();

            var submissions = new List<Submission>();

            try
            {
                HttpResponseMessage httpResponseMessage = await _cfApiHttpClient.GetAsync($"contest.status?contestId={contestId}");

                submissions = (await httpResponseMessage.Content.ReadFromJsonAsync<CFApiResponseDTO<Submission>>()).result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                throw new Exception("There are some problems with CF API");
            }

            return submissions
                .Where(s =>
                    handles.Contains(s.author.members.FirstOrDefault().handle) &&
                    s.problem.index == index &&
                    s.verdict == _verticts.OK.ToString() &&
                    s.programmingLanguage.Contains(_programmingLanguagePrefix))
                .Select(s => s.id);
        }
    }
}
