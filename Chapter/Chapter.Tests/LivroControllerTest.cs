using Chapter.WebApi.Contexts;
using Chapter.WebApi.Controllers;
using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Chapter.Tests
{
    [TestClass]
    public class LivroControllerTest
    {

        List<Livro> livros = new List<Livro>
        {
            new Livro{ Titulo = "Sylvester", QuantidadePaginas=8 },
            new Livro{ Titulo = "Whiskers", QuantidadePaginas=2 },
            new Livro{ Titulo = "Sasha", QuantidadePaginas=14 }
        };
        
        [TestMethod]
        public void Get_QuandoChamado_RetornaResultadoDeOK()
        {
            var repository = new Mock<LivroRepository>();
            repository.Setup(x => x.Listar()).Returns(livros);
            var _controller = new LivrosController(repository.Object);
            // Act
            var okResult = _controller.Listar();

            // Assert
            Assert.AreEqual(200, okResult);
        }
    }
}
