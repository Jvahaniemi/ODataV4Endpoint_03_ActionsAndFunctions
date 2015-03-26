using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using ODataV4Endpoint.Models;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;

namespace ODataV4Endpoint
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Creates an Entity Data Model (EDM) and add a route:
            ODataModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Product>("Products");
            builder.EntitySet<Supplier>("Suppliers");

            builder.Function("GetSalesTaxRate")
                .Returns<double>()
                .Parameter<int>("PostalCode");

            builder.Namespace = "ProductService";
            builder.EntityType<Product>()
                .Action("Rate")
                .Parameter<int>("Rating");

            builder.EntityType<Product>().Collection
                .Function("MostExpensive")
                .Returns<double>();

            config.MapODataServiceRoute(
                routeName: "ODataRoute",
                routePrefix: null,
                model: builder.GetEdmModel());

            
        }
    }
}
