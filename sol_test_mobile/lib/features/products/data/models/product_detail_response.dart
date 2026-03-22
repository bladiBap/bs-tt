import 'package:sol_test_mobile/features/products/data/models/currency_response.dart';
import 'package:sol_test_mobile/features/products/domain/entities/product.dart';

class ProductDetailResponse {
  final String id;
  final String name;
  final String sku;
  final double price;
  final int stock;
  final CurrencyResponse? currency;

  ProductDetailResponse({
    required this.id,
    required this.name,
    required this.sku,
    required this.price,
    required this.stock,
    this.currency,
  });

  factory ProductDetailResponse.fromJson(Map<String, dynamic> json) => 
    ProductDetailResponse(
      id: json['id'],
      name: json['name'],
      sku: json['sku'],
      price: (json['price'] as num).toDouble(),
      stock: json['stock'],
      currency: json['currency'] != null 
        ? CurrencyResponse.fromJson(json['currency']) 
        : null,
    );
    
  Product toEntity() {
    return Product(
      id: id,
      name: name,
      sku: sku,
      price: price,
      stock: stock,
      currency: currency?.toEntity(),
    );
  }
}