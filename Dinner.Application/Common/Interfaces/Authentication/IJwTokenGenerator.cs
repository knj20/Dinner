namespace Dinner.Application.Common.Interfaces.Authentication;

public interface IJwTokenGenerator
{
  string GenerateToken(Guid userId, string firstName, string lastName);
}