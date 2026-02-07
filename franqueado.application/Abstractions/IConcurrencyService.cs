namespace Franqueado.Application.Abstractions;

public interface IConcurrencyService
{
    void SetOriginalRowVersion<T>(T entity, byte[] rowVersion) where T : class;
}
