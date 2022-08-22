using Api.Entities;
using Api.DTOs;
using Api.Contstants;

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

        public async Task<IEnumerable<string>> FindHandlesByHack(int contestId, string index, string input, string? output = null) 
        {
            var httpResponseMessage = await _cfApiHttpClient.GetAsync($"contest.hacks?contestId={contestId}");

            var hacks = (await httpResponseMessage.Content.ReadFromJsonAsync<CFApiResponseDTO<Hack>>())!.result;

            return hacks
                .Where(h =>
                    h.judgeProtocol.protocol.Contains(MoonStoneConstants.InputPrefix + input + MoonStoneConstants.InputSuffix) &&
                    (output == null || h.judgeProtocol.protocol.Contains(MoonStoneConstants.OutputPrefix + output + MoonStoneConstants.OutputSuffix)) &&
                    h.problem.index == index)
                .Select(h =>
                    h.defender.members.FirstOrDefault()!.handle);
        }

        public async Task<IEnumerable<int>> FindOkSubmissionsByHack(int contestId, string index, string input, string? output = null)
        {
            var handles = (await FindHandlesByHack(contestId, index, input, output)).ToList();

            var httpResponseMessage = await _cfApiHttpClient.GetAsync($"contest.status?contestId={contestId}");

            var submissions = (await httpResponseMessage.Content.ReadFromJsonAsync<CFApiResponseDTO<Submission>>())!.result;

            return submissions
                .Where(s =>
                    handles.Contains(s.author.members.FirstOrDefault()!.handle) &&
                    s.problem.index == index &&
                    s.verdict == MoonStoneConstants.Verticts.OK.ToString() &&
                    s.author.participantType == MoonStoneConstants.ParticipantTypes.CONTESTANT.ToString() &&
                    s.programmingLanguage.Contains(MoonStoneConstants.ProgrammingLanguagePrefix))
                .Select(s => s.id);
        }
    }
}
