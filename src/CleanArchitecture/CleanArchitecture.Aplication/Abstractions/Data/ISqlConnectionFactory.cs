using System.Data;

namespace CleanArchitecture.Aplication.Abstractions.Data;

public interface ISqlConnectionFactory
{ 
    IDbConnection CreateConnection();
}
