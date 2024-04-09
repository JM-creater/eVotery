using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.Helpers.GenerateTokens;
using OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.Services;

public class VotesService : IVotesService
{
    private readonly DataContext context;
    private readonly IConfiguration configuration;
    public VotesService(DataContext _context, IConfiguration _configuration)
    {
        context = _context;
        configuration = _configuration;
    }

    public async Task<SubmitVoteResponse<Vote>> SubmitVote(SubmitVoteDto dto)
    {
        SubmitVoteResponse<Vote> response = new SubmitVoteResponse<Vote>();

        try
        {
            await GetUserVoted(dto.UserId);
            await GetSaveResponse(dto.CandidateId);

            var newVotes = new Vote
            {
                UserId = dto.UserId,
                CandidateId = dto.CandidateId
            };

            var token = Tokens.GenerateTokenSubmitVote(dto, configuration);

            if (!string.IsNullOrEmpty(token))
            {
                response.Token = token;
            }
            else
            {
                string errorMessage = "Failed to generate authentication token.";
                response.ErrorMessage = errorMessage;
                response.ResponseCode = 500;
                throw new InvalidOperationException(errorMessage);
            }

            newVotes.VotedAt = DateTime.Now;
            newVotes.DateCreated = DateTime.Now;

            context.Votes.Add(newVotes);
            await context.SaveChangesAsync();

            response.ResponseCode = 200;
        }
        catch (Exception e)
        {
            response.ResponseCode = 500;
            response.ErrorMessage = e.Message;
        }

        return response;
    }

    public async Task<Candidate> GetSaveResponse(Guid id)
    {
        try
        {
            var candidate = await context.Candidates
                                     .Include(c => c.Ballot)
                                     .Where(c => c.Id == id)
                                     .FirstOrDefaultAsync();

            if (candidate == null)
            {
                string errorMessage = "Candidate's ID not found.";
                throw new KeyNotFoundException(errorMessage);
            }

            if (candidate.Ballot != null)
            {
                candidate.Ballot.SaveResponse = true;
                await context.SaveChangesAsync();
            }
            else
            {
                throw new InvalidOperationException($"Ballot for Candidate ID {id} not found.");
            }

            return candidate;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<User> GetUserVoted(Guid userId)
    {
        try
        {
            var user = await context.Users
                                .Where(u => u.Id == userId)
                                .FirstOrDefaultAsync();

            if (user == null)
            {
                string errorMessage = "User's ID not found.";
                throw new KeyNotFoundException(errorMessage);
            }

            user.isVoted = true;

            await context.SaveChangesAsync();

            return user;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException(e.Message);
        }
    }

    public async Task<List<Vote>> GetSubmitVoteList()
        => await context.Votes
                        .AsNoTracking()
                        .ToListAsync();
}
