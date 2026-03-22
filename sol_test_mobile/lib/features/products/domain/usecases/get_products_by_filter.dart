import 'package:sol_test_mobile/features/products/domain/entities/product.dart';
import 'package:sol_test_mobile/features/products/domain/repositories/iproduct_repository.dart';

class GetProductsByFilter {
  final IProductRepository repository;
  GetProductsByFilter(this.repository);

  Future<List<Product>> call({
    int page = 1,
    int limit = 15,
    String? search,
    String? sortBy,
    String? sortOrder,
    int? minPrice,
    int? maxPrice
  }) {
    return repository.getByFilters(
      page: page,
      limit: limit,
      search: search,
      sortBy: sortBy,
      sortOrder: sortOrder,
      minPrice: minPrice,
      maxPrice: maxPrice
    );
  }
}