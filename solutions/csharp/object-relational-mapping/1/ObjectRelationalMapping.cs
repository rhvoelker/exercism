public class Orm : IDisposable
{
    private Database database;

    public Orm(Database database)
    {
        this.database = database;
    }

    public void Dispose() => database.Dispose();

    public void Begin()
    {
        if (database.DbState != Database.State.Closed)
        {
            throw new InvalidOperationException("Database must be closed before beginning.");
        }
        
        database.BeginTransaction();
    }

    public void Write(string data)
    {
        try
        {
            if (database.DbState != Database.State.TransactionStarted)
            {
                throw new InvalidOperationException("Transaction must be started before writing.");
            }
            
            database.Write(data);
        }
        catch
        {
            database.Dispose();
        }
    }

    public void Commit()
    {
        try
        {
            if (database.DbState != Database.State.DataWritten)
            {
                throw new InvalidOperationException("Data must be written before committing.");
            }
            
            database.EndTransaction();
        }
        catch
        {
            database.Dispose();
        }
    }
}
