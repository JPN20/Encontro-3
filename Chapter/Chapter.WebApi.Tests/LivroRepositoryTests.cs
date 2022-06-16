using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Chapter.WebApi.Tests
{
    public class LivroRepositoryTests
    {
        [Fact]
        public void Test2()
        {
            //var livros = new List<Livro>()
            //{
            //    new Livro { Id = 1, Titulo =  "Produto" },
            //    new Livro { Id = 2, Titulo =  "Produto2"}
            //};

            ////Setup DbContext and DbSet mock  
            //var dbContextMock = new Mock<ChapterContext>();
            //var dbSetMock = new Mock<DbSet<Livro>>();
            //dbSetMock.Setup(s => s.ToList()).Returns(livros);
            //dbContextMock.Setup(s => s.Set<Livro>()).Returns(dbSetMock.Object);

            ////Execute method of SUT (ProductsRepository)  
            //var productRepository = new LivroRepository(dbContextMock.Object);
            //var product = productRepository.Listar().Count;

            ////Assert  
            //Assert.Equal(product, livros.Count);
        }

    }
}
