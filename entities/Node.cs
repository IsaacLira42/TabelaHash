namespace TabelaHash;

public class Node<T>
{
    public int Key { get; set; }
    public T Dado { get; set; }

    public Node(int key, T dado)
    {
        Key = key;
        Dado = dado;
    }
}