using IIG_School.Models;
using IIG_School.Repository.UnitOfWork;
using IIG_School.Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace IIG_School.Services
{
    public class ProductService : IProductService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductService(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public async Task Add(Product data, IFormFile upload)
        {
            if (upload != null && upload.Length > 0)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(upload.FileName);
                string extension = Path.GetExtension(upload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                data.ImageName = fileName;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                data.ImagePath = path;
				using (var fileSrteam = new FileStream(path, FileMode.Create))
				{
					await upload.CopyToAsync(fileSrteam);
				}
			}
            _unitOfWork.ProductRepository.Add(data);
            await _unitOfWork.CommitAsync();
        }

        public Product? Get(int id)
            => _unitOfWork.ProductRepository.Get(x => x.Id == id);

        public async Task<Product?> GetAsync(int id)
            => await _unitOfWork.ProductRepository.GetAsync(x => x.Id == id);

        public async Task<ICollection<Product>> GetList()
            => await _unitOfWork.ProductRepository.GetAllAsync();

        public async Task Remove(int Id)
        {
            var exist = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == Id);
            if(exist != null)
            {
                _unitOfWork.ProductRepository.Remove(exist);
                await _unitOfWork.CommitAsync();    
            }    
        }

        private Boolean DeleteImage(string imageName)
        {
            try
            {
                if (!String.IsNullOrEmpty(imageName))
                {
                    var filepath = Path.Combine(_hostEnvironment.WebRootPath + "/Image/", imageName);
                    if (File.Exists(filepath))
                        File.Delete(filepath);
                }
                return true;
            }
            catch (IOException)
            {
                return false;
            }
        }
        public async Task Update(Product data, IFormFile upload)
        {
            var model = await _unitOfWork.ProductRepository.GetAsync(x => x.Id == data.Id);
            DeleteImage(model!.ImageName!);
            if (upload != null && upload.Length > 0)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(upload.FileName);
                string extension = Path.GetExtension(upload.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                model.ImageName = fileName;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                model.ImagePath = path;
                using (var fileSrteam = new FileStream(path, FileMode.Create))
                {
                    await upload.CopyToAsync(fileSrteam);
                }
            }
            model.Inventory = data.Inventory;
            model.ProductName = data.ProductName;
            model.ProductPrice = data.ProductPrice;
            model.ProductDescription = data.ProductDescription;
            model.ProductSale = data.ProductSale;
            _unitOfWork.ProductRepository.Update(model);
            await _unitOfWork.CommitAsync();
        }
    }
}
