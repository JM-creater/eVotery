using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.ResultFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.ResultFeatures.Services;

public class ResultService : IResultService
{
    private readonly DataContext context;

    public ResultService(DataContext _context)
    {
        context = _context;
    }

    public async Task<ApiResponse<ElectionResultDto>> GetElectionResultsByPosition(Guid positionId)
    {
        ApiResponse<ElectionResultDto> response = new ApiResponse<ElectionResultDto>();

        try
        {
            var currentDate = DateTime.Now;

            var position = await context.Positions
                                        .AsNoTracking()
                                        .Include(p => p.Candidates)
                                            .ThenInclude(e => e.Votes)
                                        .Include(b => b.Candidates)
                                            .ThenInclude(c => c.Ballot)
                                                .ThenInclude(b => b.Election)
                                        .FirstOrDefaultAsync(p => p.Id == positionId);

            if (position == null)
            {
                throw new InvalidOperationException("Specified position does not exist or is not active.");
            }

            var candidate = position.Candidates
                                    .Where(c => c.Ballot.Election.EndDate <= currentDate)
                                    .Select(c => new ElectionResultDto
                                    {
                                        CandidateId = c.Id,
                                        CandidateName = $"{c.FirstName} {c.LastName}",
                                        VoteCount = c.Votes.Count(),
                                        CandidateImage = c.Image,
                                        Position = c.Position.Name,
                                    })
                                    .OrderByDescending(c => c.VoteCount)
                                    .FirstOrDefault();

            if (candidate == null)
            {
                throw new InvalidOperationException("Election results are not available yet or no votes have been cast.");
            }

            response.Result = candidate;
            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

}
