using Dapper;
using Microsoft.Data.SqlClient;

namespace AllTheBeans.IntegrationTests;

public abstract class BeanTestBase : IntegrationTestBase
{
    protected void InsertBean(string id, double cost, string imageUrl, string name, string description, int countryId,
        int colourId)
    {
        using var connection = new SqlConnection(ConnectionString);
        connection.Open();

        var parameters = new DynamicParameters();
        parameters.Add("@Id", id);
        parameters.Add("@CostInGbp", cost);
        parameters.Add("@ImageUrl", imageUrl);
        parameters.Add("@Name", name);
        parameters.Add("@Description", description);
        parameters.Add("@CountryId", countryId);
        parameters.Add("@ColourId", colourId);


        connection.Execute("""
                           INSERT INTO [dbo].[Beans] (Id, IsBOTD, CostInGBP, ImageUrl, [Name], Description, CountryId, ColourId)
                           VALUES (@Id, 0, @CostInGbp, @ImageUrl, @Name, @Description, @CountryId, @ColourId)
                           """, parameters);
    }
}