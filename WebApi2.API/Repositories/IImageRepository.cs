using System.Net;
using WebApi2.API.Models.Domain;

namespace WebApi2.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
