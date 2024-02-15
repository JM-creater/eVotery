using Microsoft.AspNetCore.Http;

namespace OnlineVotingSystem.Application.ImageDirectory;

public class ImagePathConfig
{
    public async Task<string?> SaveVoterImages(IFormFile? imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return null;

        string mainFolder = Path.Combine(Directory.GetCurrentDirectory(), "PathImages");
        string subFolder = Path.Combine(mainFolder, "VoterImages");

        if (!Directory.Exists(mainFolder))
        {
            Directory.CreateDirectory(mainFolder);
        }
        if (!Directory.Exists(subFolder))
        {
            Directory.CreateDirectory(subFolder);
        }

        var fileName = Path.GetFileName(imageFile.FileName);
        var filePath = Path.Combine(subFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return Path.Combine("FolderImages", "VoterImages", fileName);
    }

    public async Task<string?> SaveCandidateImages(IFormFile? imageFile)
    {
        if (imageFile == null || imageFile.Length == 0)
            return null;

        string mainFolder = Path.Combine(Directory.GetCurrentDirectory(), "PathImages");
        string subFolder = Path.Combine(mainFolder, "CandidateImage");

        if (!Directory.Exists(mainFolder))
        {
            Directory.CreateDirectory(mainFolder);
        }
        if (!Directory.Exists(subFolder))
        {
            Directory.CreateDirectory(subFolder);
        }

        var fileName = Path.GetFileName(imageFile.FileName);
        var filePath = Path.Combine(subFolder, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(stream);
        }

        return Path.Combine("FolderImages", "CandidateImage", fileName);
    }
}
