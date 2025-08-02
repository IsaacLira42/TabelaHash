namespace TabelaHash.interfaces;

public interface ITabHash<T>
{
    // PRINCIPAIS
    Node<T> FindElement(int key); // Retorna nulo se não encontrar
    void InsertItem(int key, T item);
    Node<T> RemoveElement(int key);  // ME LEMBRAR: Se a chave não estiver na tabela hash retorna null, e mostrar
    int Size();
    bool IsEmpty();
    void ReHash();
    IEnumerable<int> Keys();
    IEnumerable<T> Elements();
    
    // METODOS PARA TRATAR COLISÕES
    int HashDuplo(int key, int tentativa);
    int LinearProbing(int key, int tentativa);
}