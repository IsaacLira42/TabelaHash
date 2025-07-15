namespace TabelaHash.interfaces;

public interface ITabHash<T>
{
    // PRINCIPAIS
    T FindElement(int key);
    void InsertItem(int key, T item);
    T RemoveElement(int key);  // ME LEMBRAR: Se a chave não estiver na tabela hash retorna null, e mostrar
    int Size();
    bool IsEmpty();
    IEnumerable<int> Keys();
    IEnumerable<T> Elements();
    
    // METODOS PARA TRATAR COLISÕES
}