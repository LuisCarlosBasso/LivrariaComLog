namespace LivrariaComLog.Infra.Data.Repositories.Queries
{
    public static class LivroQueries
    {
        public static string Inserir =
            @"Insert Into Livros(Nome, Autor, Edicao, Isbn, Imagem) Values (@Nome, @Autor, @Edicao, @Isbn, @Imagem);
                                         Select LAST_INSERT_ID();";

        public static string Atualizar =
            @"Update Livros Set Nome=@Nome, Autor=@Autor, Edicao=@Edicao, Isbn=@Isbn, Imagem=@Imagem Where Id=@Id";

        public static string Excluir = @"Delete From Livros Where Id=@Id";

        public static string Listar = @"Select 
                                            Id as Id,
                                            Nome as Nome,
                                            Autor as Autor, 
                                            Edicao as Edicao,
                                            Isbn as Isbn,
                                            Imagem as Imagem
                                        From Livros
                                        Order by Nome";

        public static string Obter = @"Select 
                                            Id as Id,
                                            Nome as Nome,
                                            Autor as Autor, 
                                            Edicao as Edicao,
                                            Isbn as Isbn,
                                            Imagem as Imagem
                                        From Livros
                                        Where Id=@Id";

        public static string CheckId = @"Select Id From Livros Where Id=@Id";
    }
}