// See https://aka.ms/new-console-template for more information

using System.ComponentModel;
using System.Reflection;
using VacuumCleaner;
using VacuumCleaner.RandomRidingCleaner;

//Инициализация комнаты для уборки - размер и препятствия.
//Препятствия - единички на схеме.
//x/y
//0 0 0 0 0 0 0
//0 0 0 0 0 0 0
//0 0 1 0 1 1 0
//0 0 0 0 1 1 0
//0 0 0 0 0 0 0
//0 1 0 0 0 0 1
//0 1 0 0 0 0 0
//1 1 0 0 0 0 0
var room = new Room(8, 7);
room.SetBarrier(2, 5);
room.SetBarrier(2, 4);
room.SetBarrier(3, 5);
room.SetBarrier(3, 4);
room.SetBarrier(2, 2);
room.SetBarrier(5, 1);
room.SetBarrier(5, 6);
room.SetBarrier(6, 1);
room.SetBarrier(7, 1);
room.SetBarrier(7, 0);

// room.SetBarrier(4, 4);
// room.SetBarrier(5, 4);
// room.SetBarrier(5, 3);
// room.SetBarrier(5, 2);
// room.SetBarrier(4, 2);
// room.SetBarrier(3, 2);

Console.WriteLine("Выберите тип пылесоса");

var cleanerTypes = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Cleaner))).ToArray();
var typeNumbers = new Dictionary<int, Type>();
for (var i = 0; i < cleanerTypes.Count(); i++)
{
	Console.WriteLine($"{i}: {cleanerTypes[i].Name}");
	typeNumbers.Add(i, cleanerTypes[i]);
}

var cleanerNumber = int.Parse(Console.ReadLine());
var cleaner = CreateCleaner(typeNumbers[cleanerNumber]);

//запускаем пылесос в отдельном потоке
var task = Task.Factory.StartNew(() => cleaner.Clean());

//Отобразить поле в процессе работы. Время обновления одинаково с временем задержки пылесоса.
while (!cleaner.HasFinished())
{
	for(var i=0; i<room.Length; i++)
	{
		for (int j = 0; j < room.Width; j++)
		{
			// необходимо для отображения положения пылесоса в данный момент времени. Удобно смотреть перемещение глазами.
			if (i == cleaner.CurrentX && j == cleaner.CurrentY)
			{
				Console.Write("X" + ' ');	
			}
			else
			{
				Console.Write(room.Space[i, j].ToString() + ' ');
			}
		}
		Console.WriteLine();
	}
	Console.WriteLine();
	Thread.Sleep(Cleaner.SleepMilliseconds);
}

// for(var i=0; i<room.Length; i++)
// {
// 	for (int j = 0; j < room.Width; j++)
// 	{
// 		Console.Write(room.Space[i, j].ToString() + ' ');
// 	}
// 	Console.WriteLine();
// }
Console.WriteLine($"Очистка завершена. Очищено {room.CalculateCleanPercent()}% комнаты.");

Cleaner CreateCleaner(Type cleanerType)
{
	if (cleanerType == typeof(RandomRidingCleaner))
	{
		return new RandomRidingCleaner(2, 3, room);
	}
	if (cleanerType == typeof(ClockArrowDirectionCleaner))
	{
		return new ClockArrowDirectionCleaner(2, 3, room);
	}
	if (cleanerType == typeof(AlphaRidingCleaner))
	{
		return new AlphaRidingCleaner(2, 3, room);
	}
	throw new InvalidEnumArgumentException("Не реализована фабрика для переданного типа пылесоса");
}

