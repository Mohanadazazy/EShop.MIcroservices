

namespace Catalog.API.Products.GetProducts
{

    public record GetProductQuery
    (
        int? PageNumber = 1, int? Pagesize = 10
    ) : IQuery<GetProductsResult>;

    public record GetProductsResult
    (
        IEnumerable<Product> Products
    );

    public class GetProductsQueryHandler(IDocumentSession session) : IQueryHandler<GetProductQuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductQuery query, CancellationToken cancellationToken)
        {

            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.Pagesize ?? 10, cancellationToken);  

            return new GetProductsResult(products);
        }
    }
}
