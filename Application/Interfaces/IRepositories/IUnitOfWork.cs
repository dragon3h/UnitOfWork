namespace Application.Interfaces.IRepositories;

public interface IUnitOfWork
{
    IPlayerRepository Players { get; } // we have only get because we don't want to set the repository. setting the repository will be done in the UnitOfWork class

    Task CompleteAsync(); // this method will save all the changes made to the database
}