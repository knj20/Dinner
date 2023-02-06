using Dinner.Domain.Entities;

namespace Dinner.Application.Common.Interfaces.Authentication;

public interface IJwTokenGenerator
{
  string GenerateToken(User user);
}