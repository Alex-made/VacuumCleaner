namespace VacuumCleaner;

public interface ICleaner
{
	#region Overridable
	/// <summary>
	/// Иницииирует начало уборки.
	/// </summary>
	public void Clean();

	/// <summary>
	/// Возвращает флаг окончания уборки.
	/// </summary>
	public bool HasFinished();
	#endregion
}