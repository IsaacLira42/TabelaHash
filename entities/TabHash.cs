using TabelaHash.interfaces;

namespace TabelaHash;

public class TabHash<T> : ITabHash<T>
{
    // ATRIBUTOS
    public Node<T>[] Tabela { get; set; }
    public int Tamanho { get; set; }
    public int Capacidade { get; set; }

    
    // CONSTRUTOR
    public TabHash(int capacidade = 2) // Capacidade padrão inicial = 2
    {
        Tamanho = 0;
        Capacidade = capacidade;
        Tabela = new Node<T>[Capacidade];
    }
    
    
    // METODOS PRINCIPAIS
}