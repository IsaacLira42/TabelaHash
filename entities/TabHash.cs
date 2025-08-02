using TabelaHash.interfaces;
using TabelaHash.Entities.Enum;

namespace TabelaHash;

public class TabHash<T> : ITabHash<T>
{
    // ATRIBUTOS
    public Node<T>[] Tabela { get; set; }
    public int Tamanho { get; private set; }
    public int Capacidade { get; private set; }
    public TipoTratamentoColisoes TipoTratamentoColisoes { get; private set; }
    // CONSTRUTORES
    public TabHash(int tipoTatamentoColisoes, int capacidade = 6)
    {
        Tamanho = 0;
        Capacidade = capacidade;
        Tabela = new Node<T>[Capacidade];
        TipoTratamentoColisoes = (TipoTratamentoColisoes)tipoTatamentoColisoes;
    }
    
    
    // METODOS PRINCIPAIS
    // Inserir Item
    public void InsertItem(int key, T item)
    {
        if (Tamanho >= Capacidade/2)
        {
            ReHash();
        }
        
        int tentativa = 0;
        int index = 0;
        
        while (true)
        {
            index = (TipoTratamentoColisoes == TipoTratamentoColisoes.HashDuplo) 
                ? HashDuplo(key, tentativa)
                : LinearProbing(key, tentativa);
            
            if (Tabela[index] is null || Tabela[index].Removido)
            {
                Tabela[index] = new Node<T>(key, item);
                Tamanho++;
                break;
            }
            tentativa++;
        }
    }
    // Hssh Duplo
    public int HashDuplo(int key, int tentativa) => (Hash1(key) + tentativa * Hash2(key)) % Capacidade;
    // Linear Probing
    public int LinearProbing(int key, int tentativa) => (key % Capacidade + tentativa) % Capacidade;
    // Tamanho da Tabela
    public int Size() => Tamanho;
    // Retorna True se a tabale esta vazia
    public bool IsEmpty() => Size() == 0;
    // Encontra elemento
    public Node<T> FindElement(int key)
    {
        int tentativa = 0;

        while (tentativa < Capacidade)
        {
            int index = (TipoTratamentoColisoes == TipoTratamentoColisoes.HashDuplo) 
                ? HashDuplo(key, tentativa)
                : LinearProbing(key, tentativa);

            if (Tabela[index] is null)
            {
                return null;
            }

            if (Tabela[index].Key == key && !Tabela[index].Removido)
            {
                return Tabela[index];
            }

            tentativa++;
        }

        return null;
    }
    // Remover Item
    public Node<T> RemoveElement(int key)
    {
        int index = 0;
        int tentativa = 0;

        while (tentativa < Capacidade)
        {
            index = (TipoTratamentoColisoes == TipoTratamentoColisoes.HashDuplo) 
                ? HashDuplo(key, tentativa)
                : LinearProbing(key, tentativa);

            if (Tabela[index] is null)
            {
                return null;
            }

            if (!Tabela[index].Removido && Tabela[index].Key == key)
            {
                Node<T> itemRemovido = Tabela[index];
                Tabela[index].Removido = true;
                return itemRemovido;
            }

            tentativa++;
        }

        return null;
    }
    // Rehash
    public void ReHash()
    {
        var tabelaAntiga = Tabela;
        Tamanho = 0;
        Capacidade = Capacidade * 2;
        Tabela = new Node<T>[Capacidade];

        foreach (Node<T> node in tabelaAntiga)
        {
            if (node is not null && !node.Removido)
            {
                InsertItem(node.Key, node.Dado);
            }
        }
    }
    // Keis
    public IEnumerable<int> Keys()
    {
        foreach (Node<T> node in Tabela)
        {
            if (node is not null && !node.Removido)
            {
                yield return node.Key;
            }
        }
    }
    // Elements
    public IEnumerable<T> Elements()
    {
        foreach (Node<T> node in Tabela)
        {
            if (node is not null && !node.Removido)
            {
                yield return node.Dado;
            }
        }
    }



    // METODOS AUXILIARES
    private int Hash1(int key) => key % Capacidade;
    private int Hash2(int key)
    {
        int primo = MaiorPrimoAbaixo(Capacidade);
        return primo - (key % primo); 
    }
    private int MaiorPrimoAbaixo(int n)
    {
        for (int i = n - 1; i >= 2; i--)
        {
            if (EhPrimo(i)) return i;
        }
        return 2;
    }
    private bool EhPrimo(int n)
    {
        if (n < 2) return false;
        for (int i = 2; i * i <= n; i++)
        {
            if (n % i == 0) return false;
        }
        return true;
    }
}