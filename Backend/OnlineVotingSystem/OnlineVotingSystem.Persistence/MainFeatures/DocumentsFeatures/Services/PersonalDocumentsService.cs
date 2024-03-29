using Microsoft.EntityFrameworkCore;
using OnlineVotingSystem.Domain.Entity;
using OnlineVotingSystem.Persistence.Context;
using OnlineVotingSystem.Persistence.MainFeatures.DocumentsFeatures.IServices;

namespace OnlineVotingSystem.Persistence.MainFeatures.DocumentsFeatures.Services;

public class PersonalDocumentsService : IPersonalDocumentsService
{
    private readonly DataContext context;
    public PersonalDocumentsService(DataContext _context)
    {
        context = _context;
    }

    public async Task<List<PersonalDocument>> GetAll()
        => await context.PersonalDocuments
                        .AsNoTracking()
                        .OrderByDescending(pd => pd.DateCreated)
                        .ToListAsync();
}
