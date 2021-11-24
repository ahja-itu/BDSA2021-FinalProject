using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

using WebService.Core.Server;
using System.Collections;

namespace WebService.Core.Server.Tests
{
    public class SearchAlgorithmTests
    {
        /*    private readonly MaterialRepository _repository;
         public SearchAlgorithmTests()
         {
             var connection = new SqliteConnection("Filename=:memory_");
             connection.Open();
             var builder = new DBContextOptionsBuilder<DatabaseContext>();
             builder.UseSqlite(connection);
             var context = new DatabaseContext(builder.Options);
             context.Database.ensureCreated();

             _repository = new MaterialRepository(context);
         }
    */

        [Fact]
        public void Search_given_nothing_returns_empty_list()
        {
            //Arrange
            List<string> inputWords = new List<string>();
            List<int> tagIDs = new List<int>();
            List<List<int>> filterIDs = new List<List<int>>();
            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            List<int> result = s.Search(inputWords, tagIDs, filterIDs);

            //Assert
            Assert.Equal<List<int>>(new List<int>(), result);
        }

        public static IEnumerable<object[]> Search_given_nothing_returns_empty_list_data => new List<object[]>{
            new object[]{new List<string>(){"search1"}, new List<int>(){1,2}, new List<List<int>>(){new List<int>{3,4}, new List<int>{5,6}}},
            new object[]{new List<string>(){"search2"}, new List<int>(){1,2}, new List<List<int>>(){new List<int>{3,4}, new List<int>{5,6}}},
        };

        [Theory]
        [MemberData(nameof(Search_given_nothing_returns_empty_list_data))]
        public void Search_given_certain_inputs_returns_relevant_list_of_materialIDs(List<string> inputWords, List<int> tagIDs, List<List<int>> filterIDs)
        {
            //Arrange
            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            List<int> result = s.Search(inputWords, tagIDs, filterIDs);

            //Assert
            Assert.Equal<List<int>>(new List<int>(), result);
        }

    }
}
