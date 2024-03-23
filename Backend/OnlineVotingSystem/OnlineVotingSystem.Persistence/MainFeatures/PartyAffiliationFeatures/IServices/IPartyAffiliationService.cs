using OnlineVotingSystem.Domain.Dtos;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Domain.Responses;

namespace OnlineVotingSystem.Persistence.MainFeatures.PartyAffiliationFeatures.IServices;

public interface IPartyAffiliationService
{
    Task<ApiResponse<PartyAffiliation>> Create(CreatePartyAffiliationDto dto);
    Task<List<PartyAffiliation>> GetAll();
    Task<PartyAffiliation> GetById(Guid id);
    Task<ApiResponse<PartyAffiliation>> Update(Guid id, UpdatePartyAffiliationDto dto);
    Task<ApiResponse<PartyAffiliation>> Delete(Guid id);
    Task<int> GetCountPartyMembers(Guid id);   
}
