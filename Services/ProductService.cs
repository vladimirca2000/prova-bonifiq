using ProvaPub.Interfaces.Repositories;
using ProvaPub.Interfaces.Services;
using ProvaPub.Models;
using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;        
        /// <summary>
        /// Quantidade de itens por página poderia ser passada por parametro ou ser variavel de ambiente ou appsettings
        /// </summary>
        private readonly int pageSize = 10;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> ListProducts(int page)
        {
            if(_productRepository.HasNext(page, pageSize).Result == false)
                ///Poderia ter objeto de retorno padrão com uma mensagem amigavel ao usuario
                throw new Exception("Não há mais produtos disponíveis para exibir");

            var products = await _productRepository.ListProducts(page, pageSize);
            return products ?? Enumerable.Empty<Product>();
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            var products = await _productRepository.SelectAllAsync();
            return products;
        }

        public int CountProduct()
        {
            var products = GetAllProduct().Result;
            return products.Count();
        }
    }
}
