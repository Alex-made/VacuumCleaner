// See https://aka.ms/new-console-template for more information
using VacuumCleaner;
using VacuumCleaner.RandomRidingCleaner;

//Инициализация комнаты для уборки - размер и препятствия.
//Препятствия - единички на схеме.
//0 0 0 0 0 0 0
//0 0 0 0 0 0 0
//0 0 1 0 1 1 0
//0 0 0 0 1 1 0
//0 0 0 0 0 0 0
//0 1 0 0 0 0 0
//0 1 0 0 0 0 0
//1 1 0 0 0 0 0
var room = new Room(8, 7);
room.SetBarrier(2, 5);
room.SetBarrier(2, 4);
room.SetBarrier(3, 5);
room.SetBarrier(3, 4);
room.SetBarrier(2, 2);
room.SetBarrier(5, 1);
room.SetBarrier(6, 1);
room.SetBarrier(7, 1);
room.SetBarrier(7, 0);
//room.CleanArea(1,0, 2, 1); 

var cleaner = new RandomRidingCleaner(2, 3, room);
cleaner.Clean();

//Отобразить поле в процессе работы
// while (!cleaner.HasFinished())
// {
// 	for(var i=0; i<room.Length; i++)
// 	{
// 		for (int j = 0; j < room.Width; j++)
// 		{
// 			Console.Write(room.Space[i, j].ToString() + ' ');
// 		}
// 		Console.WriteLine();
// 	}
// 	Console.WriteLine();
// 	Thread.Sleep(500);  //TODO тормозит все выполнение, но пока для примера пойдет. сделать выполнение пылесоса в отдельном потоке.
// }

for(var i=0; i<room.Length; i++)
{
	for (int j = 0; j < room.Width; j++)
	{
		Console.Write(room.Space[i, j].ToString() + ' ');
	}
	Console.WriteLine();
}
Console.WriteLine($"Очистка завершена. Очищено {room.CalculateCleanPercent()}% комнаты.");



