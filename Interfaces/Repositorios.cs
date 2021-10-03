using System.Collections.Generic;

namespace CadastroSeries.Interfaces
{
    public interface Respositorios<T>
    {
        void Adicionar(T objeto);
        void Remover(int id);
        void Reativar(int id);
        void Atualizar(int id, T obejto);
        List<T> Listar();
        int ProximoId();
        bool ExisteNaLista(int id);
        T RetornaPorId(int id);
        void RegistraDataExcluido(int id, string d);
        void RegistraDataAtualizacao(int id, string d);
    }
}