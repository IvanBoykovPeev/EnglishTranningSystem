using System.Data.Entity;

namespace EnglishTranningSystem
{
    public class WordListContext : DbContext
    {
        public DbSet<WordList> WordList { get; set; }
    }
}
