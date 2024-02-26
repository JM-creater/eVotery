using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;

public interface IPartyAffiliationService
{
    Task<ApiResponse> Create(CreatePartyAffiliationDto dto);
    Task<List<PartyAffiliation>> GetAll();
    Task<PartyAffiliation> GetById(Guid id);
}
