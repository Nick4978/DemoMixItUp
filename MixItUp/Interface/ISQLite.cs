using SQLite;

namespace MixItUp.Interface
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
