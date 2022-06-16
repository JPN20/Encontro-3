using Chapter.WebApi.Contexts;
using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Chapter.Tests
{
    [TestClass]
    public class LivroRepositoryTest
    {
        [TestMethod]
        public void QuandoCadastrarUmLivro_AdicionaComSucesso()
        {
            var mockSet = new Mock<DbSet<Livro>>();

            var mockContext = new Mock<ChapterContext>();
            mockContext.Setup(m => m.Livros).Returns(mockSet.Object);

            var service = new LivroRepository(mockContext.Object);
            service.Cadastrar(new Livro { Titulo = "Teste", Disponivel = true, QuantidadePaginas = 100 });

            mockSet.Verify(m => m.Add(It.IsAny<Livro>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}
