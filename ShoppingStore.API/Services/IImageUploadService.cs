namespace ShoppingStore.API.Services
{

	public interface IImageUploadService
	{
		Task<List<string>> Upload(ICollection<IFormFile> files);
		Task<string> Upload(IFormFile file);
	}
}
