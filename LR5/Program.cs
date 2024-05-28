using System;
using System.Collections.Generic;

class MetroGraph
{
    public Dictionary<string, Dictionary<string, int>> AdjacencyList;

    public MetroGraph()
    {
        AdjacencyList = new Dictionary<string, Dictionary<string, int>>();
    }

    public void AddEdge(string station1, string station2, int weight)
    {
        if (!AdjacencyList.ContainsKey(station1))
        {
            AdjacencyList[station1] = new Dictionary<string, int>();
        }
        if (!AdjacencyList.ContainsKey(station2))
        {
            AdjacencyList[station2] = new Dictionary<string, int>();
        }
        AdjacencyList[station1][station2] = weight;
        AdjacencyList[station2][station1] = weight;
    }

    public List<string> GetShortestPath(string start, string end, out int totalTime)
    {
        var previous = new Dictionary<string, string>();
        var distances = new Dictionary<string, int>();
        var nodes = new List<string>();

        List<string> path = null;
        totalTime = 0;

        foreach (var vertex in AdjacencyList)
        {
            if (vertex.Key == start)
            {
                distances[vertex.Key] = 0;
            }
            else
            {
                distances[vertex.Key] = int.MaxValue;
            }

            nodes.Add(vertex.Key);
        }

        while (nodes.Count != 0)
        {
            nodes.Sort((x, y) => distances[x] - distances[y]);

            var smallest = nodes[0];
            nodes.Remove(smallest);

            if (smallest == end)
            {
                path = new List<string>();
                while (previous.ContainsKey(smallest))
                {
                    path.Insert(0, smallest);
                    totalTime += AdjacencyList[previous[smallest]][smallest];
                    smallest = previous[smallest];
                }
                path.Insert(0, start);
                break;
            }

            if (distances[smallest] == int.MaxValue)
            {
                break;
            }

            foreach (var neighbor in AdjacencyList[smallest])
            {
                var alt = distances[smallest] + neighbor.Value;
                if (alt < distances[neighbor.Key])
                {
                    distances[neighbor.Key] = alt;
                    previous[neighbor.Key] = smallest;
                }
            }
        }

        return path;
    }
}

class Program
{
    static void Main(string[] args)
    {
        MetroGraph graph = new MetroGraph();

        // Добавляем станции и веса (время в пути)
        // Линия 1
        graph.AddEdge("Akademmistechko", "Zhytymirska", 4);
        graph.AddEdge("Zhytymirska", "Sviatoshyn", 4);
        graph.AddEdge("Sviatoshyn", "Nyvky", 4);
        graph.AddEdge("Nyvky", "Beresteiska", 4);
        graph.AddEdge("Beresteiska", "Shuliavska", 4);
        graph.AddEdge("Shuliavska", "Politekhnichnyi instytut", 4);
        graph.AddEdge("Politekhnichnyi instytut", "Vokzalna", 4);
        graph.AddEdge("Vokzalna", "Universytet", 4);
        graph.AddEdge("Universytet", "Teatralna", 4);
        graph.AddEdge("Teatralna", "Khreshchatyk", 4);
        graph.AddEdge("Khreshchatyk", "Arsenalna", 4);
        graph.AddEdge("Arsenalna", "Dnipro", 4);
        graph.AddEdge("Dnipro", "Hidropark", 4);
        graph.AddEdge("Hidropark", "Livoberezhna", 4);
        graph.AddEdge("Livoberezhna", "Darnytsia", 4);
        graph.AddEdge("Darnytsia", "Chernihivska", 4);
        graph.AddEdge("Chernihivska", "Lisova", 4);

        // Линия 2
        graph.AddEdge("Heroiv Dnipra", "Minska", 4);
        graph.AddEdge("Minska", "Obolon", 4);
        graph.AddEdge("Obolon", "Petrivka", 4);
        graph.AddEdge("Petrivka", "Tarasa Shevchenka", 4);
        graph.AddEdge("Tarasa Shevchenka", "Kontraktova ploshcha", 4);
        graph.AddEdge("Kontraktova ploshcha", "Poshtova ploshcha", 4);
        graph.AddEdge("Poshtova ploshcha", "Maidan Nezalezhnosti", 4);
        graph.AddEdge("Maidan Nezalezhnosti", "Ploshcha Lva Tolstoho", 4);
        graph.AddEdge("Ploshcha Lva Tolstoho", "Olimpiiska", 4);
        graph.AddEdge("Olimpiiska", "Palats Ukraina", 4);
        graph.AddEdge("Palats Ukraina", "Lybidska", 4);
        graph.AddEdge("Lybidska", "Demiivska", 4);
        graph.AddEdge("Demiivska", "Holosiivska", 4);
        graph.AddEdge("Holosiivska", "Vasylkivska", 4);
        graph.AddEdge("Vasylkivska", "Vystavkovyi tsentr", 4);
        graph.AddEdge("Vystavkovyi tsentr", "Ipodrom", 4);
        graph.AddEdge("Ipodrom", "Teremky", 4);

        // Линия 3
        graph.AddEdge("Syrets", "Dorohozhychi", 4);
        graph.AddEdge("Dorohozhychi", "Lukianivska", 4);
        graph.AddEdge("Lukianivska", "Zoloti vorota", 4);
        graph.AddEdge("Zoloti vorota", "Palats sportu", 4);
        graph.AddEdge("Palats sportu", "Klovska", 4);
        graph.AddEdge("Klovska", "Pecherska", 4);
        graph.AddEdge("Pecherska", "Druzhby narodiv", 4);
        graph.AddEdge("Druzhby narodiv", "Vydubychi", 4);
        graph.AddEdge("Vydubychi", "Slavutych", 4);
        graph.AddEdge("Slavutych", "Osokorky", 4);
        graph.AddEdge("Osokorky", "Pozniaky", 4);
        graph.AddEdge("Pozniaky", "Kharkivska", 4);
        graph.AddEdge("Kharkivska", "Vyrlytsia", 4);
        graph.AddEdge("Vyrlytsia", "Boryspilska", 4);
        graph.AddEdge("Boryspilska", "Chervonyi Khutir", 4);

        // Серая линия (городской поезд)
        graph.AddEdge("Darnytsia", "Livyi Bereh", 8);
        graph.AddEdge("Livyi Bereh", "Vydubychi Train", 8);
        graph.AddEdge("Vydubychi Train", "Vokzalna Train", 20);
        graph.AddEdge("Vokzalna Train", "Karavaievi dachi", 8);
        graph.AddEdge("Karavaievi dachi", "Kiev-Volynskyi", 8);
        graph.AddEdge("Kiev-Volynskyi", "Borshchahivka", 8);
        graph.AddEdge("Borshchahivka", "Rubezhivskyi", 8);
        graph.AddEdge("Rubezhivskyi", "Syrets Train", 15);
        graph.AddEdge("Syrets Train", "Vyshhoroska", 8);
        graph.AddEdge("Vyshhoroska", "Zenit", 8);
        graph.AddEdge("Zenit", "Petrivka Train", 8);
        graph.AddEdge("Petrivka Train", "Troieshchyna", 8);
        graph.AddEdge("Troieshchyna", "Livoberezhna Train", 8);
        graph.AddEdge("Livoberezhna Train", "Darnytsia", 8);

        // Пересадки
        graph.AddEdge("Maidan Nezalezhnosti", "Khreshchatyk", 3);
        graph.AddEdge("Teatralna", "Zoloti vorota", 3);
        graph.AddEdge("Palats sportu", "Ploshcha Lva Tolstoho", 3);
        graph.AddEdge("Vydubychi", "Vydubychi Train", 8);
        graph.AddEdge("Vokzalna Train", "Vokzalna", 8);
        graph.AddEdge("Rubezhivskyi", "Beresteiska", 8);
        graph.AddEdge("Syrets", "Syrets Train", 8);
        graph.AddEdge("Petrivka Train", "Petrivka", 8);
        graph.AddEdge("Livoberezhna Train", "Livoberezhna", 8);

        while (true)
        {
            Console.WriteLine("Enter the start station:");
            string start = Console.ReadLine();
            Console.WriteLine("Enter the end station:");
            string end = Console.ReadLine();

            int totalTime;
            List<string> path = graph.GetShortestPath(start, end, out totalTime);

            if (path != null)
            {
                Console.WriteLine("Shortest path from {0} to {1}:", start, end);
                foreach (string station in path)
                {
                    Console.WriteLine(station);
                }
                Console.WriteLine("Total time: {0} minutes", totalTime);
            }
            else
            {
                Console.WriteLine("No path found from {0} to {1}.", start, end);
            }

            Console.WriteLine("Do you want to find another path? (yes/no)");
            string response = Console.ReadLine();
            if (response.ToLower() != "yes")
            {
                break;
            }
        }
    }
}
