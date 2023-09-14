namespace VacuumCleaner;

public interface IRoom
{
	#region Overridable
	/// <summary>
	/// Возвращает значение чистоты комнаты в процентах.
	/// </summary>
	public int CalculateCleanPercent();

	/// <summary>
	/// Выполняет очистку области в виде квадрата.
	/// </summary>
	/// <param name="xStart">Начальная координата квадрата.</param>
	/// <param name="yStart">Начальная координата квадрата.</param>
	/// <param name="xFinish">Конечная координата квадрата.</param>
	/// <param name="yFinish">Конечная координата квадрата.</param>
	public void CleanArea(int xStart, int yStart, int xFinish, int yFinish);

	/// <summary>
	/// Проверяет, есть ли в комнате точка с такими координатами.
	/// </summary>
	/// <param name="xCoordinate">Координата X.</param>
	/// <param name="yCoordinate">Координата Y.</param>
	/// <returns></returns>
	public bool DoesCellExist(int xCoordinate, int yCoordinate);

	/// <summary>
	/// Устанавливает препятствие в комнате.
	/// </summary>
	/// <param name="xCoordinate">Координата X.</param>
	/// <param name="yCoordinate">Координата Y.</param>
	public void SetBarrier(int xCoordinate, int yCoordinate);
	#endregion
}
