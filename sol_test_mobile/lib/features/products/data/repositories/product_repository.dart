import 'package:dio/dio.dart';
import 'package:sol_test_mobile/features/products/data/models/create_product_request.dart';
import 'package:sol_test_mobile/features/products/data/models/product_detail_response.dart';
import 'package:sol_test_mobile/features/products/domain/entities/product.dart';
import 'package:sol_test_mobile/features/products/domain/repositories/iproduct_repository.dart';

class ProductRepositoryImpl implements IProductRepository {
  final Dio dio;
  ProductRepositoryImpl(this.dio);

  @override
  Future<Product> getById(String id) async {
    try {
      final response = await dio.get('/products/$id');
      final Map<String, dynamic> responseBody = response.data;
      if (responseBody['isSuccess'] != true) {
        throw Exception(responseBody['error'] ?? "Unknown API error");
      }
      final model = ProductDetailResponse.fromJson(responseBody['value']);
      return model.toEntity();
    } on DioException catch (e) {
      throw Exception("Error in the API: ${e.response?.statusCode}");
    }
  }

  @override
  Future<List<Product>> getByFilters({
    int page = 1, 
    int limit = 15, 
    String? search, 
    String? sortBy, 
    String? sortOrder, 
    int? minPrice, 
    int? maxPrice
  }) async {
    try {
      final response = await dio.get(
        '/products',
        queryParameters: {
          'page': page,
          'pageSize': limit,
          'searchText': ?search,
          'sortBy': ?sortBy,
          'sortOrder': ?sortOrder,
          'minPrice': ?minPrice,
          'maxPrice': ?maxPrice,
        },
      );

      final Map<String, dynamic> responseBody = response.data;
      if (responseBody['isSuccess'] == true) {
        final List<dynamic> productsJson = responseBody['value']['data'];

        return productsJson.map((json) => ProductDetailResponse.fromJson(json).toEntity()).toList();
      } else {
        throw Exception(responseBody['error'] ?? "Unknown API error");
      }

    } on DioException catch (e) {
      throw Exception("Error in the API: ${e.response?.statusCode}");
    }
  }
  
  @override
  Future<Product> create(CreateProductRequest request) async {
    try {
      final response = await dio.post(
        '/products',
        data: request.toJson(),
      );

      final Map<String, dynamic> responseBody = response.data;
      if (responseBody['isSuccess'] != true) {
        throw Exception(responseBody['error'] ?? "Unknown API error");
      }

      final model = ProductDetailResponse.fromJson(responseBody['value']).toEntity();
      return model;
    } on DioException catch (e) {
      throw Exception("Error in the API: ${e.response?.statusCode}");
    }
  }
  
  @override
  Future<Product> updatePrice(String id, double newPrice) async {
    try {
      final response = await dio.patch(
        '/products/$id/price',
        data: {
          'price': newPrice,
        },
      );

      final Map<String, dynamic> responseBody = response.data;
      if (responseBody['isSuccess'] != true) {
        throw Exception(responseBody['error'] ?? "Unknown API error");
      }

      final model = ProductDetailResponse.fromJson(responseBody['value']).toEntity();
      return model;
    } on DioException catch (e) {
      throw Exception("Error in the API: ${e.response?.statusCode}");
    }
  }
}