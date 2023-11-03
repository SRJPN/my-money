namespace MyMoney.handlers;

public interface ICommandHandler
{
    string Execute(params string[] args);
}