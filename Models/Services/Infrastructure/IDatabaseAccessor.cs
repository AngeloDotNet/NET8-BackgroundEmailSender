namespace BackgroundEmailSenderSample.Models.Services.Infrastructure;

public interface IDatabaseAccessor
{
    Task<DataSet> QueryAsync(FormattableString formattableQuery, CancellationToken token = default);
    Task<T> QueryScalarAsync<T>(FormattableString formattableQuery, CancellationToken token = default);
    Task<int> CommandAsync(FormattableString formattableCommand, CancellationToken token = default);
}