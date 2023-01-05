using FluentMigrator;
using System;

namespace DbMigrator.Migrations
{
    [Migration(202207011231)]
    public class Migration202207011231InsertCountriesAndCities : AutoReversingMigration
    {
        public override void Up()
        {
            var romaniaCountryId = Guid.NewGuid();
            Insert.IntoTable("Country").Row(new { Id = romaniaCountryId, Name = "Romania" });
            
            var iasiCityId = Guid.NewGuid();
            Insert.IntoTable("City").Row(new { Id = iasiCityId, CountryId = romaniaCountryId, Name = "Iasi" });

            var bacauCityId = Guid.NewGuid();
            Insert.IntoTable("City").Row(new { Id = bacauCityId, CountryId = romaniaCountryId, Name = "Bacau" });

            var bacauCityId = Guid.NewGuid();
            Insert.IntoTable("City").Row(new { Id = bacauCityId, CountryId = romaniaCountryId, Name = "Bacau" });
        }
    }
}
