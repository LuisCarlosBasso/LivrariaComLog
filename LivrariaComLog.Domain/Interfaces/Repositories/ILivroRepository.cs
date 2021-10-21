using System.Collections.Generic;
using LivrariaComLog.Domain.Entidades;
using LivrariaComLog.Domain.QueryResults;

namespace LivrariaComLog.Domain.Interfaces.Repositories
{
    public interface ILivroRepository
    {
        long Inserir(Livro livro);
        void Atualizar(Livro livro);
        void Excluir(long id);
        List<LivroQueryResult> Listar();
        LivroQueryResult Obter(long id);
        bool CheckId(long id);
    }
}