using OnlineVotingSystem.Domain.Entity;

namespace OnlineVotingSystem.Persistence.MainFeatures.DocumentsFeatures.IServices;

public interface IPersonalDocumentsService
{
    Task<List<PersonalDocument>> GetAll();
}
