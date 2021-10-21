using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using LivrariaComLog.Domain.Entidades;
using LivrariaComLog.Domain.Interfaces.Repositories;
using LivrariaComLog.Domain.QueryResults;
using LivrariaComLog.Infra.Data.DataContexts;
using LivrariaComLog.Infra.Data.Repositories.Queries;

namespace LivrariaComLog.Infra.Data.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly DynamicParameters _parameters = new();
        private readonly DataContext _dataContext;

        public LivroRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public long Inserir(Livro livro)
        {
            _parameters.Add("Nome", livro.Nome, DbType.String);
            _parameters.Add("Autor", livro.Autor, DbType.String);
            _parameters.Add("Edicao", livro.Edicao, DbType.Int32);
            _parameters.Add("Isbn", livro.Isbn, DbType.String);
            _parameters.Add("Imagem", livro.Imagem, DbType.String);

            return _dataContext.MySqlConnection.ExecuteScalar<long>(LivroQueries.Inserir, _parameters);
        }

        public void Atualizar(Livro livro)
        {
            _parameters.Add("Id", livro.Id, DbType.Int64);
            _parameters.Add("Nome", livro.Nome, DbType.String);
            _parameters.Add("Autor", livro.Autor, DbType.String);
            _parameters.Add("Edicao", livro.Edicao, DbType.Int32);
            _parameters.Add("Isbn", livro.Isbn, DbType.String);
            _parameters.Add("Imagem", livro.Imagem, DbType.String);

            _dataContext.MySqlConnection.Execute(LivroQueries.Atualizar, _parameters);
        }

        public void Excluir(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            _dataContext.MySqlConnection.Execute(LivroQueries.Excluir, _parameters);
        }

        public List<LivroQueryResult> Listar()
        {
            var livros = _dataContext.MySqlConnection.Query<LivroQueryResult>(LivroQueries.Listar).ToList();
            return livros;
        }

        public LivroQueryResult Obter(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            var livro = _dataContext.MySqlConnection.Query<LivroQueryResult>(LivroQueries.Obter, _parameters)
                .FirstOrDefault();
            return livro;
        }

        public bool CheckId(long id)
        {
            _parameters.Add("Id", id, DbType.Int64);

            return _dataContext.MySqlConnection.Query<bool>(LivroQueries.CheckId, _parameters).FirstOrDefault();
        }
    }
}