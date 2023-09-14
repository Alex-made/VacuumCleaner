namespace VacuumCleaner;

/// <summary>
/// Представляет комнату.
/// </summary>
public class Room : IRoom
{
	#region .ctor
	/// <summary>
	/// Инициализирует экземпляр типа <see cref="Room" />.
	/// </summary>
	/// <param name="length">Длина комнаты.</param>
	/// <param name="width">Ширина комнаты.</param>
	public Room(int length, int width)
	{
		Space = new int[length, width];
		Length = length;
		Width = width;
	}
	#endregion

	#region Properties
	public int Length
	{
		get;
		private set;
	}

	public int[,] Space
	{
		get;
		private set;
	}

	public int Width
	{
		get;
		private set;
	}
	#endregion

	#region IRoom members
	/// <inheritdoc />
	public int CalculateCleanPercent()
	{
		var cellCount = Length * Width;
		var cleanCells = 0;

		for (var i = 0; i < Length; i++)
		{
			for (var j = 0; j < Width; j++)
			{
				if (Space[i, j] == 2)
				{
					cleanCells++;
				}
			}
		}

		return cleanCells * 100 / cellCount;
	}

	/// <inheritdoc />
	public void CleanArea(int xStart, int yStart, int xFinish, int yFinish)
	{
		for (var i = xStart; i <= xFinish; i++)
		{
			for (var j = yStart; j <= yFinish; j++)
			{
				Space[i, j] = 2;
			}
		}
	}

	/// <inheritdoc />
	public bool DoesCellExist(int xCoordinate, int yCoordinate)
	{
		try
		{
			return Space[xCoordinate, yCoordinate] != 1;
		}
		catch (IndexOutOfRangeException e)
		{
			return false;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	/// <inheritdoc />
	public void SetBarrier(int xCoordinate, int yCoordinate)
	{
		Space[xCoordinate, yCoordinate] = 1;
	}
	#endregion
}
