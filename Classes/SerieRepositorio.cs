using System.Collections.Generic;
using CadastroSeries.Interfaces;
using CadastroSeries;

public class SerieRepositorio : Respositorios<Series>
{
    List<Series> lista = new List<Series>();

    public void Adicionar(Series objeto)
    {
        lista.Add(objeto);
    }

    public void Atualizar(int id, Series obejto)
    {
        lista[id] = obejto;
    }

    public List<Series> Listar()
    {
        return lista;
    }

    public int ProximoId()
    {
        return lista.Count;
    }

    public void Remover(int id)
    {
        lista[id].excluir();
    }

    public void Reativar(int id)
    {
        lista[id].reativar();
    }

    public bool ExisteNaLista(int id)
    {
        for(int i = 0; i < lista.Count; i++)
        {
            if(lista[i].retornarId() == id)
                return true;
        }
        return false;
    }

    public Series RetornaPorId(int id)
    {
        return lista[id];
    }
    
    public void RegistraDataExcluido(int id, string d)
    {
        lista[id].dataExclusao(d);
    }
    public void RegistraDataAtualizacao(int id, string d)
    {
        lista[id].dataAtualizacao(d);
    }
}