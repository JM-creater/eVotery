using AutoMapper;
using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.VotesFeatures.Services;

public class VotesService : IVotesService
{
    private readonly DataContext context;
    private readonly IMapper mapper;
    public VotesService(DataContext _context, IMapper _mapper)
    {
        context = _context;
        mapper = _mapper;
    }

    public async Task<ApiResponse<Vote>> SubmitVote(SubmitVoteDto dto)
    {
        ApiResponse<Vote> response = new ApiResponse<Vote>();

        try
        {
            //var votes = await context.Votes
            //                         .Where(v => v.UserId == dto.UserId &&
            //                                     v.CandidateId == dto.CandidateId)
            //                         .FirstOrDefaultAsync();

            //if (votes == null)
            //{
            //    string errorMessage = "Vote already exists for this voter and candidate.";
            //    response.ErrorMessage = errorMessage;
            //    throw new KeyNotFoundException(errorMessage);
            //}

            var newVotes = new Vote
            {
                UserId = dto.UserId,
                CandidateId = dto.CandidateId
            };
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
}
