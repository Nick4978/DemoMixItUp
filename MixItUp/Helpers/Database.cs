using MixItUp.Interface;
using MixItUp.LocalDatabaseModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MixItUp.Helpers
{
    public class Database
    {
        //TODO : To Declare Class Level Variables..
        private SQLiteConnection _sqlconnection;

        public Database()
        {
            //Getting conection and Creating table  
            _sqlconnection = DependencyService.Get<ISQLite>().GetConnection();

            #region CREATE TABLES

            try
            {
                _sqlconnection.CreateTable<CategoryDataTable>();

            }
            catch (Exception ex)
            { }

            #endregion
        }

        #region Category Data category

        //Add Category Data
        public void AddCategoryData(CategoryDataTable categoryData)
        {
            _sqlconnection.Insert(categoryData);
        }

        //Update Manager Category Data to DB  
        public void UpdateCategoryDataTable(CategoryDataTable categoryData)
        {
            _sqlconnection.Update(categoryData);
        }

        //Delete all Category Data  
        public void DeleteAllCategoryData(CategoryDataTable categoryData)
        {
            _sqlconnection.Delete(categoryData);
        }

        //Delete specific Category Data  
        public void DeleteCategoryData(int id)
        {
            _sqlconnection.Delete<CategoryDataTable>(id);
        }

        //Get all Category Data  
        public IEnumerable<CategoryDataTable> GetAllCategoryData()
        {
            return (from t in _sqlconnection.Table<CategoryDataTable>() select t).ToList();
        }

        //Get specific User Category Data
        public CategoryDataTable GetSpecificCategoryData(int id)
        {
            return _sqlconnection.Table<CategoryDataTable>().FirstOrDefault(t => t.CategoryDataId == id);
        }

        #endregion

    }
}
