import 'package:sol_test_mobile/features/products/domain/entities/currency.dart';

class Product {
  final String id;
  final String name;
  final String? sku;
  final double price;
  final int? stock;
  final Currency? currency;

  const Product({
    required this.id,
    required this.name,
    required this.sku,
    required this.price,
    required this.stock,
    this.currency,
  });

  bool get isOutOfStock => stock == 0;
  bool get priceIsValid => price >= 0;
  String get formattedPrice => "${currency?.symbol ?? ''} ${price.toStringAsFixed(2)}";
}