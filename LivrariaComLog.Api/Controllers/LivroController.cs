using System;
using System.Collections.Generic;
using LivrariaComLog.Domain.Commands.Input;
using LivrariaComLog.Domain.Handlers;
using LivrariaComLog.Domain.Interfaces.Repositories;
using LivrariaComLog.Domain.QueryResults;
using LivrariaComLog.Infra.Interfaces.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaComLog.Controllers
{
    [Consumes("application/json")]
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _repository;
        private readonly LivroHandler _handler;

        public LivroController(ILivroRepository repository, LivroHandler handler)
        {
            _repository = repository;
            _handler = handler;
        }

        [HttpPost]
        [Route("livros")]
        public ICommandResult InserirLivro([FromBody] AdicionarLivroCommand command)
        {
            return _handler.Handle(command);
        }
        
        [HttpPut]
        [Route("livros/{id}")]
        public ICommandResult AtualizarLivro(long id, [FromBody] AtualizarLivroCommand command)
        {
            command.Id = id;
            return _handler.Handle(command);
        }  
        
        [HttpDelete]
        [Route("livros/{id}")]
        public ICommandResult ExcluirLivro(long id)
        {
            return _handler.Handle(new ExcluirLivroCommand() {Id = id});
        }   
        
        [HttpGet]
        [Route("livros")]
        public List<LivroQueryResult> ListarLivros()
        {
            return _repository.Listar();
        }       
        
        [HttpGet]
        [Route("livro/{id}")]
        public LivroQueryResult ObterLivro(long id)
        {
            return _repository.Obter(id);
        }
    }
}