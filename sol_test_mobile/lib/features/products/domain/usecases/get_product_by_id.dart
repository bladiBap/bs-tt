import 'package:sol_test_mobile/features/products/domain/entities/product.dart';
import 'package:sol_test_mobile/features/products/domain/repositories/iproduct_repository.dart';

class GetProductById {
  final IProductRepository repository;
  GetProductById(this.repository);

  Future<Product> call(String id) {
    return repository.getById(id);
  }
}