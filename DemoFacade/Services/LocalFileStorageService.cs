namespace DemoFacade.Services
{
    public class LocalFileStorageService
    {
        private readonly string _storagePath = "wwwroot/uploads";

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            if (!Directory.Exists(_storagePath))
                Directory.CreateDirectory(_storagePath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return $"/uploads/{fileName}";
        }
    }
}
