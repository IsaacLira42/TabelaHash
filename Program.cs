using System;
using TabelaHash;
namespace TabelaHash;


class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=== Tabela Hash Genérica ===");
        Console.WriteLine("Escolha o tratamento de colisões:");
        Console.WriteLine("1 - Linear Probing");
        Console.WriteLine("2 - Hash Duplo");
        Console.Write("Opção: ");
        int tipo = int.Parse(Console.ReadLine() ?? "1");

        var tabela = new TabHash<string>(tipo);

        while (true)
        {
            Console.WriteLine("\n=== MENU ===");
            Console.WriteLine("1 - Inserir item");
            Console.WriteLine("2 - Buscar item");
            Console.WriteLine("3 - Remover item");
            Console.WriteLine("4 - Exibir chaves (Keys)");
            Console.WriteLine("5 - Exibir elementos (Values)");
            Console.WriteLine("6 - Ver tamanho da tabela");
            Console.WriteLine("7 - Verificar se está vazia");
            Console.WriteLine("8 - Mostrar tabela (debug)");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine();
            Console.WriteLine();

            switch (opcao)
            {
                case "1":
                    Console.Write("Informe a chave (int): ");
                    int chave = int.Parse(Console.ReadLine());
                    Console.Write("Informe o valor (string): ");
                    string valor = Console.ReadLine();
                    tabela.InsertItem(chave, valor);
                    Console.WriteLine("Item inserido com sucesso!");
                    break;

                case "2":
                    Console.Write("Informe a chave (int): ");
                    int chaveBusca = int.Parse(Console.ReadLine());
                    var encontrado = tabela.FindElement(chaveBusca);
                    if (encontrado is not null)
                        Console.WriteLine($"Valor encontrado: {encontrado.Dado}");
                    else
                        Console.WriteLine("Chave não encontrada.");
                    break;

                case "3":
                    Console.Write("Informe a chave (int): ");
                    int chaveRemover = int.Parse(Console.ReadLine());
                    var removido = tabela.RemoveElement(chaveRemover);
                    if (removido is not null)
                        Console.WriteLine($"Item removido: {removido.Dado}");
                    else
                        Console.WriteLine("Item não encontrado ou já removido.");
                    break;

                case "4":
                    Console.WriteLine("Chaves armazenadas:");
                    foreach (var key in tabela.Keys())
                        Console.WriteLine($"- {key}");
                    break;

                case "5":
                    Console.WriteLine("Elementos armazenados:");
                    foreach (var elem in tabela.Elements())
                        Console.WriteLine($"- {elem}");
                    break;

                case "6":
                    Console.WriteLine($"Tamanho atual da tabela: {tabela.Size()}");
                    break;

                case "7":
                    Console.WriteLine(tabela.IsEmpty()
                        ? "Tabela está vazia."
                        : "Tabela contém elementos.");
                    break;

                case "8":
                    MostrarTabela(tabela);
                    break;

                case "0":
                    Console.WriteLine("Encerrando programa...");
                    return;

                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;
            }
        }
    }

    static void MostrarTabela(TabHash<string> tabela)
    {
        Console.WriteLine("=== TABELA HASH (DEBUG) ===");
        for (int i = 0; i < tabela.Tabela.Length; i++)
        {
            var node = tabela.Tabela[i];
            if (node is not null && !node.Removido)
                Console.WriteLine($"[{i}] Key: {node.Key}, Valor: {node.Dado}");
            else if (node is not null && node.Removido)
                Console.WriteLine($"[{i}] REMOVIDO");
            else
                Console.WriteLine($"[{i}] VAZIO");
        }
    }
}
