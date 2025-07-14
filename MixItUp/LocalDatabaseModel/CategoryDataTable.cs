using SQLite;

namespace MixItUp.LocalDatabaseModel
{
    public class CategoryDataTable
    {
        [PrimaryKey, AutoIncrement]

        public int CategoryDataId { get; set; }

        public string id { get; set; }
        public string RecipeName { get; set; }
        public string RecipeDescription { get; set; }
        public string RecipeType { get; set; }
        public string RecipeGlass { get; set; }
        public string RecipeGarnishes { get; set; }
    }
}
