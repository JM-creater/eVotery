using Microsoft.Extensions.Options;

namespace OnlineVotingSystem.Application.ImageDirectory;

public class ImageDirectoryPath
{
    private readonly ImagePathOptions _imagePathOptions;
    public ImageDirectoryPath(IOptions<ImagePathOptions> imagePathOptions)
    {
        _imagePathOptions = imagePathOptions.Value;
    }

    public string GetMainFolderPath()
    {
        return _imagePathOptions.PathImages;
    }

    public string GetVoterPath()
    {
        return Path.Combine(_imagePathOptions.PathImages, _imagePathOptions.VoterImages);
    }

    public string GetCandidatePath()
    {
        return Path.Combine(_imagePathOptions.PathImages, _imagePathOptions.CandidateImages);
    }

    public string GetLogoImagePath()
    {
        return Path.Combine(_imagePathOptions.PathImages, _imagePathOptions.LogoImage);
    }
}
