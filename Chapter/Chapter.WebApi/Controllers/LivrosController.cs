using Chapter.WebApi.Models;
using Chapter.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Chapter.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável pelos endpoints (URLs) referentes aos livros
    /// </summary>

    // Define que o tipo de resposta da API será no formato JSON
    [Produces("application/json")]

    // Define que a rota de uma requisição será no formato dominio/api/nomeController
    // ex: http://localhost:5000/api/livros
    [Route("api/[controller]")]

    // atributo para habilitar comportamentos especificos de API, como status, retorno
    [ApiController]
    // [ControllerBase] - requisicoes HTTP
    public class LivrosController : ControllerBase
    {
        private readonly LivroRepository _livroRepository;
        public LivrosController(LivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }
        // GET /api/livros
        [HttpGet]
        public IActionResult Listar()
        {
            // retorna no corpo da resposta, a lista de livros
            // retorna o status Ok - 200, sucesso
            return Ok(_livroRepository.Listar());
        }

        // POST /api/livros
        [HttpPost]
        // recebe a informacao do livro que deseja salvar do corpo da requisição
        public IActionResult Cadastrar(Livro livro)
        {
            _livroRepository.Cadastrar(livro);
            // o status code para um cadastro, pode ser utilizado o 201 ou 200 Ok
            return StatusCode(201);
        }

        // GET /api/livros/{id}
        [HttpGet("{id}")] // busca um livro a partir do id passado na requisição
        public IActionResult BuscarPorId(int id)
        {
            // busca o livro por id no banco
            Livro livro = _livroRepository.BuscarPorId(id);
            // se o livro não for encontrado, retorna uma status 
            // de não encontrado, 404 (NotFound).
            if (livro == null)
            {
                return NotFound();
            }
            // caso tenha sido encontrado, retorna o status 
            // de sucesso com a informação sobre o livro
            return Ok(livro);
        }

        // PUT /api/livros/{id}
        [AllowAnonymous]
        [HttpPut("{id}")] // o id passado no PUT /api/livros/1
        // recebe a informacao do livro que deseja 
        // atualizar do corpo da requisição
        public IActionResult Atualizar(int id, Livro livro)
        {
            // atualizar as informações de um livro 
            // no corpo da requisição, corresponde as novas informações do livro
            // na solicitação, o id do livro a ser atualizado
            _livroRepository.Atualizar(id, livro);
            return StatusCode(204);
        }

        // DELETE /api/livros/{id}
        [Authorize]
        [HttpDelete("{id}")] // o id passado no DELETE /api/livros/1
        public IActionResult Deletar(int id)
        {
            _livroRepository.Deletar(id);
            return StatusCode(204);
        }
    }
}
