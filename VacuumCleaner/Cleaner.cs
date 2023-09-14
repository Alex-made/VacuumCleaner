namespace VacuumCleaner;

/// <summary>
/// Представляет базовый пылесос.
/// </summary>
public abstract class Cleaner : ICleaner
{
	#region Data
	#region Consts
	/// <summary>
	/// Возвращает максимально число шагов пылесоса.
	/// </summary>
	protected const int MaxPossibleIterations = 60;
	#endregion
	#endregion

	#region Overridable
	/// <inheritdoc />
	public abstract void Clean();

	/// <inheritdoc />
	public abstract bool HasFinished();
	#endregion
}
