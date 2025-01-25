namespace poc.aws.services.api.Repository.UnitOfWork;

public interface IUnitOfWork
{
    IProfileRepository Profiles { get; }
    int Commit();
}