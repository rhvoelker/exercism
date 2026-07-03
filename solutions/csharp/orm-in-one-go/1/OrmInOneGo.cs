public class Orm
{
    private Database database;

    public Orm(Database database)
    {
        this.database = database;
    }

    public void Write(string data)
    {
        using var db2 = database;
        db2.BeginTransaction();
        db2.Write(data);
        db2.EndTransaction();
    }

    public bool WriteSafely(string data)
    {
        try
        {
            using var db2 = database;
            db2.BeginTransaction();
            db2.Write(data);
            db2.EndTransaction();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
