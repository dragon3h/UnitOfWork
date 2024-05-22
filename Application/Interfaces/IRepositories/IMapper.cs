namespace Application.Interfaces.IRepositories;

public interface IMapper<TInput, TOutput> where TInput : class where TOutput : class
{
    TOutput Map(TInput source);
    TInput MapForCreation(TOutput playerDto);
    TInput MapForUpdating(TInput source, TOutput playerToUpdate);
    List<TOutput> MapList(List<TInput> source);
}