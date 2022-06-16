using Chapter.WebApi.Controllers;
using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Moq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Xunit;

namespace Chapter.WebApi.Tests
{
    public class LivrosControllerTests
    {
        private readonly HttpClient _client;

        public LivrosControllerTests()
        {
            var server = new TestServer(
                new WebHostBuilder()
                .UseEnvironment("Development")
                .UseStartup<Startup>()
            );
            _client = server.CreateClient();
        }

        [Fact]
        public void Test1()
        {
            //var mock = new Mock<LivroRepository>();
            //var livros = new List<Livro>()
            //{
            //    new Livro { Id = 1, Titulo =  "Produto" },
            //    new Livro { Id = 2, Titulo =  "Produto2"}
            //};
            //mock.Setup(repo => repo.Listar()).Returns(livros);
            //var controller = new LivrosController(mock.Object);

            Assert.Equal(1, 1);
            
        }
    }
}
