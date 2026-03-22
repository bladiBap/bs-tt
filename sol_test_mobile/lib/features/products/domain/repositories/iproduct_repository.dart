import 'package:sol_test_mobile/features/products/data/models/create_product_request.dart';
import 'package:sol_test_mobile/features/products/domain/entities/product.dart';

abstract class IProductRepository {
  
  Future<Product> getById(String id);
  
  Future<List<Product>> getByFilters({
    int page = 1, 
    int limit = 15, 
    String? search, 
    String? sortBy, 
    String? sortOrder, 
    int? minPrice, 
    int? maxPrice
  });

  Future<Product> create(CreateProductRequest request);

  Future<Product> updatePrice(String id, double newPrice);
}