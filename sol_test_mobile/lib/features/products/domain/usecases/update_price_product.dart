import 'package:sol_test_mobile/features/products/domain/entities/product.dart';
import 'package:sol_test_mobile/features/products/domain/repositories/iproduct_repository.dart';

class UpdatePriceProduct {
  final IProductRepository repository;
  UpdatePriceProduct(this.repository);

  Future<Product> call({
    required String id,
    required double price
  }) {
    return repository.updatePrice(id, price);
  }
}