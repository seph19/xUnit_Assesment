using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace ProductsApp.Tests
{

    public class ProductsAppShould
    {
        [Fact]
        public void Products_Should_Throw_AnException_When_Product_IsNull()
        {
            //arrange
            var product = new Products();

            //act and assert
            Assert.Throws<ArgumentNullException>(() => product.AddNew(null));
        }

        [Fact]
        public void Products_Should_Add_New()
        {
            //arrange
            var count = 0;
            var products = new Products();
            var product = new Product() { Name = "Beer", IsSold = false };

            //act
            products.AddNew(product);

            //assert
            Assert.True(products.Items.Count() > count);
        }

        [Fact]
        public void Products_Should_Throw_AnException_When_ProdutName_IsNull()
        {
            // arrange
            var products = new Products();
            var product = new Product() { Name = null, IsSold = true };

            //act and assert
            Assert.Throws<NameRequiredException>(() => products.AddNew(product));
        }

    }

    internal class Products
    {
        private readonly List<Product> _products = new List<Product>();

        public IEnumerable<Product> Items => _products.Where(t => !t.IsSold);

        public void AddNew(Product product)
        {
            product = product ??
                throw new ArgumentNullException();
            product.Validate();
            _products.Add(product);
        }

        public void Sold(Product product)
        {
            product.IsSold = true;
        }

    }

    internal class Product
    {
        public bool IsSold { get; set; }
        public string Name { get; set; }

        internal void Validate()
        {
            Name = Name ??
                throw new NameRequiredException();
        }

    }

    [Serializable]
    internal class NameRequiredException : Exception
    {
        public NameRequiredException() { /* ... */ }

        public NameRequiredException(string message) : base(message) { /* ... */ }

        public NameRequiredException(string message, Exception innerException) : base(message, innerException) { /* ... */ }

        protected NameRequiredException(SerializationInfo info, StreamingContext context) : base(info, context) { /* ... */ }
    }
}