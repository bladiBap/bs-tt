import 'package:sol_test_mobile/features/products/data/models/create_product_request.dart';
import 'package:sol_test_mobile/features/products/domain/entities/product.dart';
import 'package:sol_test_mobile/features/products/domain/repositories/iproduct_repository.dart';

class CreateProduct {
  final IProductRepository repository;
  CreateProduct(this.repository);

  Future<Product> call({
    required String name,
    required double price,
    required String currencyId,
  }) {
    return repository.create(CreateProductRequest(
      name: name,
      price: price,
      currencyId: currencyId,
    ));
  }
}