using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using WebService.Core.Server;

namespace WebService.Core.Server.Tests
{
        public class SearchAlgorithmTests
    {
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


        [Fact]
        public void Search_given_certain_inputs_returns_relevant_list_of_materialIDs()
        {
            //Arrange
            List<string> inputWords = new List<string>(/*input values*/);
            List<int> tagIDs = new List<int>(/*input values*/);
            List<List<int>> filterIDs = new List<List<int>>(/*input values*/);
            SearchAlgorithm s = new SearchAlgorithm();

            //Act
            List<int> result = s.Search(inputWords, tagIDs, filterIDs);

            //Assert
            Assert.Equal<List<int>>(new List<int>(/*input values*/), result);
        }

    }
}
